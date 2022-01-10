// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
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

namespace BootstrapBlazor.Components;

/// <summary>
/// 缓存操作类
/// </summary>
internal class CacheManager : ICacheManager
{
    private IMemoryCache Cache { get; set; }

    private IStringLocalizerFactory Localizer { get; set; }

    private IServiceProvider Provider { get; set; }

    [NotNull]
    private static CacheManager? Instance { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="memoryCache"></param>
    /// <param name="localizerFactory"></param>
    public CacheManager(
        IServiceProvider provider,
        IMemoryCache memoryCache,
        IStringLocalizerFactory localizerFactory)
    {
        Provider = provider;
        Cache = memoryCache;
        Localizer = localizerFactory;
        Instance = this;
    }

    /// <summary>
    /// 获得或者创建指定 Key 缓存项
    /// </summary>
    public T GetOrCreate<T>(object key, Func<ICacheEntry, T> factory) => Cache.GetOrCreate(key, entry =>
    {
#if DEBUG
        entry.SlidingExpiration = TimeSpan.FromSeconds(5);
#endif

        if (key is not string)
        {
            entry.SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }
        return factory(entry);
    });

    /// <summary>
    /// 获得或者创建指定 Key 缓存项 异步重载方法
    /// </summary>
    public Task<T> GetOrCreateAsync<T>(object key, Func<ICacheEntry, Task<T>> factory) => Cache.GetOrCreateAsync(key, async entry =>
    {
#if DEBUG
        entry.SlidingExpiration = TimeSpan.FromSeconds(5);
#endif

        if (key is not string)
        {
            entry.SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }
        return await factory(entry);
    });

    /// <summary>
    /// 设置 App 开始时间
    /// </summary>
    public void SetStartTime()
    {
        GetOrCreate("BootstrapBlazor_StartTime", entry => DateTimeOffset.Now);
    }

    /// <summary>
    /// 获取 App 开始时间
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset GetStartTime()
    {
        var ret = DateTimeOffset.MinValue;
        if (Cache.TryGetValue("BootstrapBlazor_StartTime", out var v) && v is DateTimeOffset d)
        {
            ret = d;
        }
        return ret;
    }

    #region Localizer
    /// <summary>
    /// 通过指定类型创建 IStringLocalizer 实例
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    public static IStringLocalizer? CreateLocalizer<TType>() => CreateLocalizerByType(typeof(TType));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resourceSource"></param>
    /// <returns></returns>
    public static IStringLocalizer? CreateLocalizerByType(Type resourceSource) => resourceSource.Assembly.IsDynamic
        ? null
        : Instance.Localizer.Create(resourceSource);
    #endregion

    #region DisplayName
    public static string? GetEnumDisplayName(Type type, string fieldName)
    {
        var t = Nullable.GetUnderlyingType(type) ?? type;
        var cacheKey = $"EnumDisplayName-{CultureInfo.CurrentUICulture.Name}-{t.FullName}-{fieldName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            var dn = "";
            // search in Localization
            var localizer = CreateLocalizerByType(t);
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

            entry.SetDynamicAssemblyPolicy(type);

            return dn;
        });
    }

    public static string GetDisplayName(Type modelType, string fieldName)
    {
        var cacheKey = $"DisplayName-{CultureInfo.CurrentUICulture.Name}-{modelType.FullName}-{fieldName}";
        var displayName = Instance.GetOrCreate(cacheKey, entry =>
        {
            string? dn = null;
            // 显示名称为空时通过资源文件查找 FieldName 项
            var localizer = modelType.Assembly.IsDynamic ? null : CreateLocalizerByType(modelType);
            var stringLocalizer = localizer?[fieldName];
            if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
            {
                dn = stringLocalizer.Value;
            }
            else if (TryGetProperty(modelType, fieldName, out var propertyInfo))
            {
                dn = FindDisplayAttribute(propertyInfo);
            }

            entry.SetDynamicAssemblyPolicy(modelType);

            return dn;
        });

        return displayName ?? fieldName;

        string? FindDisplayAttribute(PropertyInfo propertyInfo)
        {
            // 回退查找 Display 标签
            var dn = propertyInfo.GetCustomAttribute<DisplayAttribute>(true)?.Name
                ?? propertyInfo.GetCustomAttribute<DisplayNameAttribute>(true)?.DisplayName;

            // 回退查找资源文件通过 dn 查找匹配项 用于支持 Validation
            if (!modelType.Assembly.IsDynamic && !string.IsNullOrEmpty(dn))
            {
                dn = GetLocalizerValueByKey(dn);
            }
            return dn;
        }
    }

    /// <summary>
    /// 通过指定 Key 获取资源文件中的键值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static string? GetLocalizerValueByKey(string key)
    {
        string? dn = null;
        var resxType = Instance.Provider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
        if (resxType.Value.ResourceManagerStringLocalizerType != null)
        {
            var localizer = CreateLocalizerByType(resxType.Value.ResourceManagerStringLocalizerType);
            if (localizer != null)
            {
                var stringLocalizer = localizer[key];
                if (!stringLocalizer.ResourceNotFound)
                {
                    dn = stringLocalizer.Value;
                }
            }
        }
        return dn ?? key;
    }
    #endregion

    #region Placeholder
    public static string? GetPlaceholder(Type modelType, string fieldName)
    {
        var cacheKey = $"Placeholder-{CultureInfo.CurrentUICulture.Name}-{modelType.FullName}-{fieldName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            string? ret = null;
            // 通过资源文件查找 FieldName 项
            var localizer = CreateLocalizerByType(modelType);
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

            entry.SetDynamicAssemblyPolicy(modelType);

            return ret;
        });
    }
    #endregion

    #region Lambda Property
    public static bool TryGetProperty(Type modelType, string fieldName, [NotNullWhen(true)] out PropertyInfo? propertyInfo)
    {
        var cacheKey = $"GetProperty-{modelType.FullName}-{fieldName}";
        propertyInfo = Instance.GetOrCreate(cacheKey, entry =>
        {
            var props = modelType.GetRuntimeProperties().AsEnumerable();

            // 支持 MetadataType
            var metadataType = modelType.GetCustomAttribute<MetadataTypeAttribute>(false);
            if (metadataType != null)
            {
                props = props.Concat(metadataType.MetadataClassType.GetRuntimeProperties());
            }

            var pi = props.FirstOrDefault(p => p.Name == fieldName);

            entry.SetDynamicAssemblyPolicy(modelType);

            return pi;
        });
        return propertyInfo != null;
    }

    public static TResult GetPropertyValue<TModel, TResult>(TModel model, string fieldName)
    {
        var type = model is object o ? o.GetType() : typeof(TModel);
        var cacheKey = ($"Lambda-Get-{type.FullName}", typeof(TModel), fieldName, typeof(TResult));
        var invoker = Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(type);
            return LambdaExtensions.GetPropertyValueLambda<TModel, TResult>(model, fieldName).Compile();
        });
        return invoker(model);
    }

    public static void SetPropertyValue<TModel, TValue>(TModel model, string fieldName, TValue value)
    {
        var type = model is object o ? o.GetType() : typeof(TModel);
        var cacheKey = ($"Lambda-Set-{type.FullName}", typeof(TModel), fieldName, typeof(TValue));
        var invoker = Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(type);
            return LambdaExtensions.SetPropertyValueLambda<TModel, TValue>(model, fieldName).Compile();
        });
        invoker(model, value);
    }
    #endregion

    #region Lambda Sort
    public static Func<IEnumerable<T>, string, SortOrder, IEnumerable<T>> GetSortFunc<T>()
    {
        var cacheKey = $"Lambda-{nameof(LambdaExtensions.GetSortLambda)}-{typeof(T).FullName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(typeof(T));
            return LambdaExtensions.GetSortLambda<T>().Compile();
        });
    }
    public static Func<IEnumerable<T>, List<string>, IEnumerable<T>> GetSortListFunc<T>()
    {
        var cacheKey = $"Lambda-{nameof(LambdaExtensions.GetSortListLambda)}-{typeof(T).FullName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(typeof(T));
            return LambdaExtensions.GetSortListLambda<T>().Compile();
        });
    }
    #endregion

    #region Lambda ConvertTo
    public static Func<object, IEnumerable<string?>> CreateConverterInvoker(Type type)
    {
        var cacheKey = $"Lambda-{nameof(CreateConverterInvoker)}-{type.FullName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            var method = typeof(CacheManager)
                .GetMethod(nameof(ConvertToString), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(type);

            var para_exp = Expression.Parameter(typeof(object));
            var convert = Expression.Convert(para_exp, typeof(List<>).MakeGenericType(type));
            var body = Expression.Call(method, convert);

            entry.SetDynamicAssemblyPolicy(type);
            return Expression.Lambda<Func<object, IEnumerable<string?>>>(body, para_exp).Compile();

        });
    }

    private static IEnumerable<string?> ConvertToString<TSource>(List<TSource> source) => source is List<SelectedItem> list
        ? list.Select(i => i.Value)
        : source.Select(i => i?.ToString());
    #endregion

    #region Format
    public static Func<object, string, IFormatProvider?, string> GetFormatInvoker(Type type)
    {
        var cacheKey = $"Lambda-{nameof(GetFormatLambda)}-{type.FullName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(type);
            return GetFormatLambda(type).Compile();
        });

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

    public static Func<object, IFormatProvider?, string> GetFormatProviderInvoker(Type type)
    {
        var cacheKey = $"Lambda-{nameof(GetFormatProviderLambda)}-{type.FullName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(type);
            return GetFormatProviderLambda(type).Compile();
        });

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
}

/// <summary>
/// ICacheManager 扩展操作类
/// </summary>
public static class ICacheManagerExtensions
{
    /// <summary>
    /// 通过 JsonLocalizationOptions 配置项实例获取资源文件配置集合
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="assembly"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    public static IEnumerable<IConfigurationSection> GetJsonStringConfig(this ICacheManager cache, Assembly assembly, JsonLocalizationOptions option)
    {
        var cacheKey = $"Localizer-Sections-{CultureInfo.CurrentUICulture.Name}-{assembly.GetName().Name}-{nameof(GetJsonStringConfig)}";
        return cache.GetOrCreate(cacheKey, entry => option.GetJsonStringConfig(assembly));
    }

    /// <summary>
    /// 获取指定文化本地化资源集合
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="cultureName"></param>
    /// <param name="option"></param>
    /// <param name="assembly"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static IEnumerable<KeyValuePair<string, string>> GetJsonStringByCulture(this ICacheManager cache, string cultureName, JsonLocalizationOptions option, Assembly assembly, string typeName)
    {
        var cacheKey = $"Localizer-{cultureName}-{assembly.GetName().Name}-{typeName}";
        return cache.GetOrCreate(cacheKey, entry =>
        {
            // 获得程序集中的资源文件 stream
            var sections = option.GetJsonStringConfig(assembly);
            var v = sections
                .FirstOrDefault(kv => typeName.Equals(kv.Key, StringComparison.OrdinalIgnoreCase))?
                .GetChildren()
                .SelectMany(c => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(c.Key, c.Value) });

            return v ?? Enumerable.Empty<KeyValuePair<string, string>>();
        });
    }
}
