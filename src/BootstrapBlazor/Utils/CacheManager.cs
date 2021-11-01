// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 缓存操作类
    /// </summary>
    public static class CacheManager
    {
        private static Lazy<IMemoryCache> Cache { get; } = new(() => ServiceProviderHelper.ServiceProvider?.GetRequiredService<IMemoryCache>() ?? new MemoryCache(new MemoryCacheOptions()));

        /// <summary>
        /// 获得或者创建指定 Key 缓存项
        /// </summary>
        public static T GetOrCreate<T>(object key, Func<ICacheEntry, T> factory) => Cache.Value.GetOrCreate(key, entry =>
        {
#if DEBUG
            entry.SlidingExpiration = TimeSpan.FromSeconds(30);
#else
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
#endif
            return factory(entry);
        });

        /// <summary>
        /// 获得或者创建指定 Key 缓存项 异步重载方法
        /// </summary>
        public static Task<T> GetOrCreateAsync<T>(object key, Func<ICacheEntry, Task<T>> factory) => Cache.Value.GetOrCreateAsync(key, async entry =>
        {
#if DEBUG
            entry.SlidingExpiration = TimeSpan.FromSeconds(30);
#else
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
#endif
            return await factory(entry);
        });

        internal static IEnumerable<KeyValuePair<string, string>> GetJsonStringByCulture(string cultureName, JsonLocalizationOptions option, Assembly assembly, string typeName) => CacheManager.GetOrCreate($"Localizer-{cultureName}-{assembly.GetName().Name}-{typeName}", key =>
        {
            // 获得程序集中的资源文件 stream
            var v = JsonStringConfigHelper.GetJsonStringConfig(option)
                .FirstOrDefault(kv => typeName.Equals(kv.Key, StringComparison.OrdinalIgnoreCase))?
                .GetChildren()
                .SelectMany(c => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(c.Key, c.Value) });

            return v ?? Enumerable.Empty<KeyValuePair<string, string>>();
        });

        internal static string? GetEnumDisplayName(Type type, string fieldName)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            var cacheKey = $"EnumDisplayName-{CultureInfo.CurrentCulture.Name}-{t.Name}-{fieldName}";
            return CacheManager.GetOrCreate(cacheKey, entry =>
            {
                var dn = "";
                // search in Localization
                var localizer = JsonStringLocalizerFactory.CreateLocalizer(t);
                var stringLocalizer = localizer?[fieldName];
                if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
                {
                    dn = stringLocalizer.Value;
                }
                else
                {
                    var field = t.GetField(fieldName);
                    dn = field?.GetCustomAttribute<DisplayAttribute>(true)?.Name
                    ?? field?.GetCustomAttribute<DescriptionAttribute>(true)?.Description;

                    // search in Localization again
                    if (!string.IsNullOrEmpty(dn))
                    {
                        dn = GetLocalizerValueByKey(dn);
                    }
                }

                // add display name into cache
                if (type.Assembly.IsDynamic)
                {
                    entry.SetSlidingExpirationForDynamicAssembly();
                }

                return dn;
            });
        }

        internal static string GetDisplayName(Type modelType, string fieldName)
        {
            var cacheKey = $"DisplayName-{CultureInfo.CurrentUICulture.Name}-{modelType.Name}-{fieldName}";
            var displayName = CacheManager.GetOrCreate(cacheKey, entry =>
            {
                string? dn = null;
                // 显示名称为空时通过资源文件查找 FieldName 项
                var localizer = modelType.Assembly.IsDynamic ? null : JsonStringLocalizerFactory.CreateLocalizer(modelType);
                var stringLocalizer = localizer?[fieldName];
                if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
                {
                    dn = stringLocalizer.Value;
                }
                else if (!modelType.Assembly.IsDynamic && TryGetProperty(modelType, fieldName, out var propertyInfo))
                {
                    dn = FindDisplayAttribute(propertyInfo);
                }
                return dn;
            });
            return displayName ?? fieldName;

            static string? FindDisplayAttribute(PropertyInfo propertyInfo)
            {
                // 回退查找 Display 标签
                var dn = propertyInfo.GetCustomAttribute<DisplayAttribute>(true)?.Name
                    ?? propertyInfo.GetCustomAttribute<DisplayNameAttribute>(true)?.DisplayName;

                // 回退查找资源文件通过 dn 查找匹配项 用于支持 Validation
                if (!string.IsNullOrEmpty(dn))
                {
                    dn = GetLocalizerValueByKey(dn);
                }
                return dn;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="fieldName"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        internal static bool TryGetProperty(Type modelType, string fieldName, [NotNullWhen(true)] out PropertyInfo? propertyInfo)
        {
            var cacheKey = $"GetProperty-{modelType.Name}-{fieldName}";
            propertyInfo = CacheManager.GetOrCreate(cacheKey, entry =>
            {
                var props = modelType.GetProperties().AsEnumerable();

                // 支持 MetadataType
                var metadataType = modelType.GetCustomAttribute<MetadataTypeAttribute>(false);
                if (metadataType != null)
                {
                    props = props.Concat(metadataType.MetadataClassType.GetProperties());
                }

                var pi = props.Where(p => p.Name == fieldName).FirstOrDefault();
                if (modelType.Assembly.IsDynamic)
                {
                    entry.SetSlidingExpirationForDynamicAssembly();
                }
                return pi;
            });
            return propertyInfo != null;
        }

        internal static string? GetPlaceholder(Type modelType, string fieldName)
        {
            var cacheKey = $"Placeholder-{modelType.Name}-{fieldName}";
            return CacheManager.GetOrCreate(cacheKey, entry =>
            {
                string? ret = null;
                // 通过资源文件查找 FieldName 项
                var localizer = JsonStringLocalizerFactory.CreateLocalizer(modelType);
                var stringLocalizer = localizer?[$"{fieldName}.PlaceHolder"];
                if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
                {
                    ret = stringLocalizer.Value;
                }
                else if (Utility.TryGetProperty(modelType, fieldName, out var propertyInfo))
                {
                    var placeHolderAttribute = propertyInfo.GetCustomAttribute<PlaceHolderAttribute>(true);
                    if (placeHolderAttribute != null)
                    {
                        ret = placeHolderAttribute.Text;
                    }
                }
                return ret;
            });
        }

        /// <summary>
        /// 通过指定 Key 获取资源文件中的键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string? GetLocalizerValueByKey(string key)
        {
            string? dn = null;
            var resxType = ServiceProviderHelper.ServiceProvider?.GetRequiredService<IOptions<JsonLocalizationOptions>>();
            if (resxType?.Value.ResourceManagerStringLocalizerType != null)
            {
                var localizer = JsonStringLocalizerFactory.CreateLocalizer(resxType.Value.ResourceManagerStringLocalizerType);
                if (localizer != null)
                {
                    var stringLocalizer = localizer[key];
                    if (!stringLocalizer.ResourceNotFound)
                    {
                        dn = stringLocalizer.Value;
                    }
                }
            }
            return dn;
        }

        internal static TResult GetPropertyValue<TModel, TResult>(TModel model, string fieldName)
        {
            var cacheKey = $"Lambda-{nameof(LambdaExtensions.GetPropertyValueLambda)}-{typeof(TModel).Name}-{fieldName}";
            var invoker = CacheManager.GetOrCreate(cacheKey, entry => LambdaExtensions.GetPropertyValueLambda<TModel, TResult>(model, fieldName).Compile());
            return invoker(model);
        }

        internal static void SetPropertyValue<TModel, TValue>(TModel model, string fieldName, TValue value)
        {
            var cacheKey = $"Lambda-{nameof(LambdaExtensions.SetPropertyValueLambda)}-{typeof(TModel).Name}-{fieldName}";
            var invoker = CacheManager.GetOrCreate(cacheKey, entry => LambdaExtensions.SetPropertyValueLambda<TModel, TValue>(model, fieldName).Compile());
            invoker(model, value);
        }

        internal static Func<IEnumerable<T>, string, SortOrder, IEnumerable<T>> GetSortFunc<T>()
        {
            var key = $"Lambda-{nameof(LambdaExtensions.GetSortLambda)}-{typeof(T).Name}";
            return CacheManager.GetOrCreate(key, entry => LambdaExtensions.GetSortLambda<T>().Compile());
        }

        internal static Func<object, IEnumerable<string?>> CreateConverterInvoker(Type type) => CacheManager.GetOrCreate($"Lambda-{nameof(CreateConverterInvoker)}-{type.Name}", entry =>
        {
            var method = typeof(Utility)
                .GetMethod(nameof(ConvertToString), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(type);

            var para_exp = Expression.Parameter(typeof(object));
            var convert = Expression.Convert(para_exp, typeof(List<>).MakeGenericType(type));
            var body = Expression.Call(method, convert);
            return Expression.Lambda<Func<object, IEnumerable<string?>>>(body, para_exp).Compile();

            static IEnumerable<string?> ConvertToString<TSource>(List<TSource> source) => source is List<SelectedItem> list
                ? list.Select(i => i.Value)
                : source.Select(i => i?.ToString());
        });

        private static EventCallback<TType> CreateCallback<TType>(ComponentBase component, object model, string fieldName) => EventCallback.Factory.Create<TType>(component, t =>
        {
            var key = $"Lambda-{nameof(CreateCallback)}-{model.GetType().Name}-{fieldName}";
            var invoker = CacheManager.GetOrCreate(key, entry => LambdaExtensions.SetPropertyValueLambda<object, object?>(model, fieldName).Compile());
            invoker.Invoke(model, t);
        });

        #region Format
        internal static Func<object, string, IFormatProvider?, string> GetFormatInvoker(Type type)
        {
            var cacheKey = $"Lambda-{nameof(GetFormatLambda)}-{type.Name}";
            return CacheManager.GetOrCreate(cacheKey, entry => GetFormatLambda(type).Compile());

            static Expression<Func<object, string, IFormatProvider?, string>> GetFormatLambda(Type type)
            {
                var exp_p1 = Expression.Parameter(typeof(object));
                var exp_p2 = Expression.Parameter(typeof(string));
                var exp_p3 = Expression.Parameter(typeof(IFormatProvider));
                Expression? body = null;
                if (type.IsSubclassOf(typeof(IFormattable)))
                {
                    // 通过 IFormattable 接口格式化
                    var mi = type.GetMethod("ToString", new Type[] { typeof(string), typeof(IFormatProvider) });
                    if (mi != null)
                    {
                        body = Expression.Call(Expression.Convert(exp_p1, type), mi, exp_p2, exp_p3);
                    }
                }
                else
                {
                    // 通过 ToString(string format) 方法格式化
                    var mi = type.GetMethod("ToString", new Type[] { typeof(string) });
                    if (mi != null)
                    {
                        body = Expression.Call(Expression.Convert(exp_p1, type), mi, exp_p2);
                    }
                }
                return body == null
                    ? (s, f, provider) => s.ToString() ?? ""
                    : Expression.Lambda<Func<object, string, IFormatProvider?, string>>(body, exp_p1, exp_p2, exp_p3);
            }
        }

        internal static Func<object, IFormatProvider?, string> GetFormatProviderInvoker(Type type)
        {
            var cacheKey = $"Lambda-{nameof(GetFormatProviderLambda)}-{type.Name}";
            return CacheManager.GetOrCreate(cacheKey, entry => GetFormatProviderLambda(type).Compile());

            static Expression<Func<object, IFormatProvider?, string>> GetFormatProviderLambda(Type type)
            {
                var exp_p1 = Expression.Parameter(typeof(object));
                var exp_p2 = Expression.Parameter(typeof(IFormatProvider));
                Expression? body;

                var mi = type.GetMethod("ToString", new Type[] { typeof(IFormatProvider) });
                if (mi != null)
                {
                    // 通过 ToString(IFormatProvider? provider) 接口格式化
                    body = Expression.Call(Expression.Convert(exp_p1, type), mi, exp_p2);
                }
                else
                {
                    // 通过 ToString() 方法格式化
                    mi = type.GetMethod("ToString", new Type[] { typeof(string) });
                    body = Expression.Call(Expression.Convert(exp_p1, type), mi!);
                }
                return Expression.Lambda<Func<object, IFormatProvider?, string>>(body, exp_p1, exp_p2);
            }
        }
        #endregion

        internal static object? GenerateValueChanged(ComponentBase component, object model, string fieldName, Type fieldType)
        {
            var cacheKey = $"Lambda-{nameof(GenerateValueChanged)}-{model.GetType().Name}-{fieldName}";
            var valueChangedInvoker = CacheManager.GetOrCreate(cacheKey, entry => CreateLambda(fieldType).Compile());
            return valueChangedInvoker(component, model, fieldName);

            static Expression<Func<ComponentBase, object, string, object>> CreateLambda(Type fieldType)
            {
                var exp_p1 = Expression.Parameter(typeof(ComponentBase));
                var exp_p2 = Expression.Parameter(typeof(object));
                var exp_p3 = Expression.Parameter(typeof(string));
                var method = typeof(CacheManager).GetMethod(nameof(CreateCallback), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(fieldType);
                var body = Expression.Call(null, method, exp_p1, exp_p2, exp_p3);

                return Expression.Lambda<Func<ComponentBase, object, string, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2, exp_p3);
            }
        }
    }
}
