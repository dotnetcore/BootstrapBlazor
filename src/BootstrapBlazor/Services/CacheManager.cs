// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization;
using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 缓存操作类
/// </summary>
internal class CacheManager : ICacheManager
{
    private IMemoryCache Cache { get; set; }

    private IServiceProvider Provider { get; set; }

    [NotNull]
    private static CacheManager? Instance { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="memoryCache"></param>
    public CacheManager(
        IServiceProvider provider,
        IMemoryCache memoryCache)
    {
        // 为了避免依赖导致的报错，构造函数避免使用其他服务
        Provider = provider;
        Cache = memoryCache;
        Instance = this;
    }

    /// <summary>
    /// 获得或者创建指定 Key 缓存项
    /// </summary>
    public T GetOrCreate<T>(object key, Func<ICacheEntry, T> factory) => Cache.GetOrCreate(key, entry =>
    {
#if DEBUG
        entry.SlidingExpiration = TimeSpan.FromSeconds(500000);
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

    #region Count
    public static int ElementCount(object? value)
    {
        var ret = 0;
        if (value != null)
        {
            var type = value.GetType();
            var cacheKey = $"Lambda-Count-{type.FullName}";
            var invoker = Instance.GetOrCreate(cacheKey, entry =>
            {
                entry.SetDynamicAssemblyPolicy(type);
                return LambdaExtensions.CountLambda(type).Compile();
            });
            ret = invoker(value);
        }
        return ret;
    }
    #endregion

    #region Localizer
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resourceSource"></param>
    /// <returns></returns>
    public static IStringLocalizer? CreateLocalizerByType(Type resourceSource) => resourceSource.Assembly.IsDynamic
        ? null
        : Instance.Provider.GetRequiredService<IStringLocalizerFactory>().Create(resourceSource);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static JsonLocalizationOptions GetJsonLocalizationOption()
    {
        var localizationOptions = Instance.Provider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
        return localizationOptions.Value;
    }

    /// <summary>
    /// 通过 程序集与类型获得 IStringLocalizer 实例
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static IStringLocalizer? GetStringLocalizerFromService(Assembly assembly, string typeName) => assembly.IsDynamic
        ? null
        : Instance.GetOrCreate($"{nameof(GetStringLocalizerFromService)}-{CultureInfo.CurrentUICulture.Name}-{assembly.GetName().Name}-{typeName}", entry =>
    {
        IStringLocalizer? ret = null;
        var factories = Instance.Provider.GetServices<IStringLocalizerFactory>();
        if (factories != null)
        {
            var factory = factories.LastOrDefault(a => a is not JsonStringLocalizerFactory);
            if (factory != null)
            {
                var type = assembly.GetType(typeName);
                if (type != null)
                {
                    ret = factory.Create(type);
                }
            }
        }
        return ret;
    });

    /// <summary>
    /// 获取指定文化本地化资源集合
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static IEnumerable<LocalizedString>? GetAllStringsByTypeName(Assembly assembly, string typeName) => GetJsonStringByTypeName(GetJsonLocalizationOption(), assembly, typeName, CultureInfo.CurrentUICulture.Name);

    /// <summary>
    /// 通过指定程序集获取所有本地化信息键值集合
    /// </summary>
    /// <param name="option">JsonLocalizationOptions 实例</param>
    /// <param name="assembly">Assembly 程序集实例</param>
    /// <param name="typeName">类型名称</param>
    /// <param name="cultureName">cultureName 未空时使用 CultureInfo.CurrentUICulture.Name</param>
    /// <param name="forceLoad">默认 false 使用缓存值 设置 true 时内部强制重新加载</param>
    /// <returns></returns>
    public static IEnumerable<LocalizedString>? GetJsonStringByTypeName(JsonLocalizationOptions option, Assembly assembly, string typeName, string? cultureName = null, bool forceLoad = false)
    {
        return assembly.IsDynamic ? null : GetJsonStringByTypeName();

        IEnumerable<LocalizedString>? GetJsonStringByTypeName()
        {
            cultureName ??= CultureInfo.CurrentUICulture.Name;
            var key = $"{nameof(GetJsonStringByTypeName)}-{assembly.GetName().Name}-{cultureName}";
            var typeKey = $"{key}-{typeName}";
            if (forceLoad)
            {
                Instance.Cache.Remove(key);
                Instance.Cache.Remove(typeKey);
            }
            return Instance.GetOrCreate(typeKey, entry =>
            {
                var sections = Instance.GetOrCreate(key, entry => option.GetJsonStringFromAssembly(assembly, cultureName));
                return sections.FirstOrDefault(kv => typeName.Equals(kv.Key, StringComparison.OrdinalIgnoreCase))?
                    .GetChildren()
                    .SelectMany(kv => new[] { new LocalizedString(kv.Key, kv.Value) });
            });
        }
    }

    /// <summary>
    /// 通过 ILocalizationResolve 接口实现类获得本地化键值集合
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public static IEnumerable<LocalizedString> GetAllStringsFromResolve(bool includeParentCultures = true) => Instance.GetOrCreate($"{nameof(GetAllStringsFromResolve)}-{CultureInfo.CurrentUICulture.Name}", entry => Instance.Provider.GetRequiredService<ILocalizationResolve>().GetAllStringsByCulture(includeParentCultures));
    #endregion

    #region DisplayName
    /// <summary>
    /// 获得类型属性的描述信息
    /// </summary>
    /// <param name="modelType"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static string GetDisplayName(Type modelType, string fieldName)
    {
        var cacheKey = $"{nameof(GetDisplayName)}-{CultureInfo.CurrentUICulture.Name}-{modelType.FullName}-{fieldName}";
        var displayName = Instance.GetOrCreate(cacheKey, entry =>
        {
            string? dn = null;
            // 显示名称为空时通过资源文件查找 FieldName 项
            var localizer = modelType.Assembly.IsDynamic ? null : CreateLocalizerByType(modelType);
            var stringLocalizer = localizer?[fieldName];
            if (stringLocalizer is { ResourceNotFound: false })
            {
                dn = stringLocalizer.Value;
            }
            else if (modelType.IsEnum)
            {
                var info = modelType.GetFieldByName(fieldName);
                if (info != null)
                {
                    dn = FindDisplayAttribute(info);
                }
            }
            else if (TryGetProperty(modelType, fieldName, out var propertyInfo))
            {
                dn = FindDisplayAttribute(propertyInfo);
            }

            entry.SetDynamicAssemblyPolicy(modelType);

            return dn;
        });

        return displayName ?? fieldName;

        string? FindDisplayAttribute(MemberInfo memberInfo)
        {
            // 回退查找 Display 标签
            var dn = memberInfo.GetCustomAttribute<DisplayAttribute>(true)?.Name
                ?? memberInfo.GetCustomAttribute<DisplayNameAttribute>(true)?.DisplayName
                ?? memberInfo.GetCustomAttribute<DescriptionAttribute>(true)?.Description;

            // 回退查找资源文件通过 dn 查找匹配项 用于支持 Validation
            if (!modelType.Assembly.IsDynamic && !string.IsNullOrEmpty(dn))
            {
                dn = GetLocalizerValueFromResourceManager(dn);
            }
            return dn;
        }
    }

    public static List<SelectedItem> GetNullableBoolItems(Type modelType, string fieldName)
    {
        var cacheKey = $"{nameof(GetNullableBoolItems)}-{CultureInfo.CurrentUICulture.Name}-{modelType.FullName}-{fieldName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            var items = new List<SelectedItem>();
            var localizer = modelType.Assembly.IsDynamic ? null : CreateLocalizerByType(modelType);
            IStringLocalizer? localizerAttr = null;
            items.Add(new SelectedItem("", FindDisplayText(nameof(NullableBoolItemsAttribute.NullValueDisplayText), attr => attr.NullValueDisplayText)));
            items.Add(new SelectedItem("True", FindDisplayText(nameof(NullableBoolItemsAttribute.TrueValueDisplayText), attr => attr.TrueValueDisplayText)));
            items.Add(new SelectedItem("False", FindDisplayText(nameof(NullableBoolItemsAttribute.FalseValueDisplayText), attr => attr.FalseValueDisplayText)));
            return items;

            string FindDisplayText(string itemName, Func<NullableBoolItemsAttribute, string?> callback)
            {
                string? dn = null;

                // 优先读取资源文件配置
                var stringLocalizer = localizer?[$"{fieldName}-{itemName}"];
                if (stringLocalizer is { ResourceNotFound: false })
                {
                    dn = stringLocalizer.Value;
                }
                else if (TryGetProperty(modelType, fieldName, out var propertyInfo))
                {
                    // 类资源文件未找到 回落查找标签
                    var attr = propertyInfo.GetCustomAttribute<NullableBoolItemsAttribute>(true);
                    if (attr != null && !modelType.Assembly.IsDynamic)
                    {
                        dn = callback(attr);
                    }
                }

                // 回落读取 NullableBoolItemsAttribute 资源文件配置
                return dn ?? FindDisplayTextByItemName(itemName);
            }

            string FindDisplayTextByItemName(string itemName)
            {
                localizerAttr ??= CreateLocalizerByType(typeof(NullableBoolItemsAttribute));
                var stringLocalizer = localizerAttr![itemName];
                return stringLocalizer.Value;
            }
        });
    }

    /// <summary>
    /// 通过指定 Key 获取资源文件中的键值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static string? GetLocalizerValueFromResourceManager(string key)
    {
        string? dn = null;
        var options = GetJsonLocalizationOption();
        if (options.ResourceManagerStringLocalizerType != null)
        {
            var localizer = CreateLocalizerByType(options.ResourceManagerStringLocalizerType);
            dn = GetValueByKey(localizer);
        }
        return dn ?? key;

        [ExcludeFromCodeCoverage]
        string? GetValueByKey(IStringLocalizer? l)
        {
            string? val = null;
            var stringLocalizer = l?[key];
            if (stringLocalizer is { ResourceNotFound: false })
            {
                val = stringLocalizer.Value;
            }
            return val;
        }
    }
    #endregion

    #region Placeholder
    public static string? GetPlaceholder(Type modelType, string fieldName)
    {
        var cacheKey = $"{nameof(GetPlaceholder)}-{CultureInfo.CurrentUICulture.Name}-{modelType.FullName}-{fieldName}";
        return Instance.GetOrCreate(cacheKey, entry =>
        {
            // 通过资源文件查找 FieldName 项
            string? ret = null;
            var localizer = CreateLocalizerByType(modelType);
            if (localizer != null)
            {
                var stringLocalizer = localizer[$"{fieldName}.PlaceHolder"];
                if (stringLocalizer is { ResourceNotFound: false })
                {
                    ret = stringLocalizer.Value;
                }
                else if (TryGetProperty(modelType, fieldName, out var propertyInfo))
                {
                    var placeHolderAttribute = propertyInfo.GetCustomAttribute<PlaceHolderAttribute>(true);
                    if (placeHolderAttribute != null)
                    {
                        ret = placeHolderAttribute.Text;
                    }
                }

                entry.SetDynamicAssemblyPolicy(modelType);
            }
            return ret;
        });
    }
    #endregion

    #region Lambda Property
    public static bool TryGetProperty(Type modelType, string fieldName, [NotNullWhen(true)] out PropertyInfo? propertyInfo)
    {
        var cacheKey = $"{nameof(TryGetProperty)}-{modelType.FullName}-{fieldName}";
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
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        var type = model.GetType();
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
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        var type = model.GetType();
        var cacheKey = ($"Lambda-Set-{type.FullName}", typeof(TModel), fieldName, typeof(TValue));
        var invoker = Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(type);
            return LambdaExtensions.SetPropertyValueLambda<TModel, TValue>(model, fieldName).Compile();
        });
        invoker(model, value);
    }

    /// <summary>
    /// 获得 指定模型标记 <see cref="KeyAttribute"/> 的属性值
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="model"></param>
    /// <param name="customAttribute"></param>
    /// <returns></returns>
    public static TValue GetKeyValue<TModel, TValue>(TModel model, Type? customAttribute = null)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        var type = model.GetType();
        var cacheKey = ($"Lambda-GetKeyValue-{type.FullName}-{customAttribute?.FullName}", typeof(TModel));
        var invoker = Instance.GetOrCreate(cacheKey, entry =>
        {
            entry.SetDynamicAssemblyPolicy(type);

            return LambdaExtensions.GetKeyValue<TModel, TValue>(customAttribute).Compile();
        });
        return invoker(model);
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
        var cacheKey = $"{nameof(GetFormatInvoker)}-{nameof(GetFormatLambda)}-{type.FullName}";
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
            if (type.IsAssignableTo(typeof(IFormattable)))
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
                // 通过 ToString() 方法格式化
                var mi = type.GetMethod("ToString", Array.Empty<Type>());
                if (mi != null)
                {
                    body = Expression.Call(Expression.Convert(exp_p1, type), mi);
                }
            }
            return body == null
                ? (s, f, provider) => s.ToString() ?? ""
                : Expression.Lambda<Func<object, string, IFormatProvider?, string>>(body, exp_p1, exp_p2, exp_p3);
        }
    }

    public static Func<object, IFormatProvider?, string> GetFormatProviderInvoker(Type type)
    {
        var cacheKey = $"{nameof(GetFormatProviderInvoker)}-{nameof(GetFormatProviderLambda)}-{type.FullName}";
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
                mi = type.GetMethod("ToString");
                body = Expression.Call(Expression.Convert(exp_p1, type), mi!);
            }
            return Expression.Lambda<Func<object, IFormatProvider?, string>>(body, exp_p1, exp_p2);
        }
    }
    #endregion
}
