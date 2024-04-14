// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// JsonStringLocalizer 实现类
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="assembly"></param>
/// <param name="typeName"></param>
/// <param name="baseName"></param>
/// <param name="ignoreLocalizerMissing"></param>
/// <param name="logger"></param>
/// <param name="resourceNamesCache"></param>
internal class JsonStringLocalizer(Assembly assembly, string typeName, string baseName, bool ignoreLocalizerMissing, ILogger logger, IResourceNamesCache resourceNamesCache) : ResourceManagerStringLocalizer(new ResourceManager(baseName, assembly), assembly, baseName, resourceNamesCache, logger)
{
    private Assembly Assembly { get; } = assembly;

    private ILogger Logger { get; } = logger;

    /// <summary>
    /// 通过指定键值获取多语言值信息索引
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override LocalizedString this[string name]
    {
        get
        {
            var value = GetStringSafely(name);
            return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: typeName);
        }
    }

    /// <summary>
    /// 带格式化参数的通过指定键值获取多语言值信息索引
    /// </summary>
    /// <param name="name"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public override LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = SafeFormat();
            return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: typeName);

            string? SafeFormat()
            {
                string? ret = null;
                try
                {
                    var format = GetStringSafely(name);
                    ret = string.Format(CultureInfo.CurrentCulture, format ?? name, arguments);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "{JsonStringLocalizerName} searched for '{Name}' in '{typeName}' with culture '{CultureName}' throw exception.", nameof(JsonStringLocalizer), name, typeName, CultureInfo.CurrentUICulture.Name);
                }
                return ret;
            }
        }
    }

    private string? GetStringSafely(string name)
    {
        return GetStringFromService(name)
            ?? GetStringSafely(name, null)
            ?? GetStringSafelyFromJson(name);

        // get string from inject service
        string? GetStringFromService(string name)
        {
            string? ret = null;
            var localizer = Utility.GetStringLocalizerFromService(Assembly, typeName);
            if (localizer != null)
            {
                ret = GetLocalizerValueFromCache(localizer, name);
            }
            return ret;
        }

        // get string from json localization file
        string? GetStringSafelyFromJson(string name)
        {
            var localizerStrings = CacheManager.GetAllStringsByTypeName(Assembly, typeName);
            return GetValueFromCache(localizerStrings, name);
        }
    }

    private string? GetValueFromCache(IEnumerable<LocalizedString>? localizerStrings, string name)
    {
        string? ret = null;
        var cultureName = CultureInfo.CurrentUICulture.Name;
        var cacheKey = $"{nameof(GetValueFromCache)}&name={name}&{Assembly.GetUniqueName()}&type={typeName}&culture={cultureName}";
        if (!CacheManager.GetMissingLocalizerByKey(cacheKey))
        {
            var l = GetLocalizedString();
            if (l is { ResourceNotFound: false })
            {
                ret = l.Value;
            }
            else
            {
                LogSearchedLocation(name);
                CacheManager.AddMissingLocalizerByKey(cacheKey, name);
            }
        }
        return ret;

        LocalizedString? GetLocalizedString()
        {
            LocalizedString? localizer = null;
            if (localizerStrings != null)
            {
                localizer = localizerStrings.FirstOrDefault(i => i.Name == name);
            }
            return localizer ?? CacheManager.GetAllStringsFromResolve().FirstOrDefault(i => i.Name == name);
        }
    }

    private string? GetLocalizerValueFromCache(IStringLocalizer localizer, string name)
    {
        string? ret = null;
        var cultureName = CultureInfo.CurrentUICulture.Name;
        var cacheKey = $"{nameof(GetLocalizerValueFromCache)}&name={name}&{Assembly.GetUniqueName()}&type={typeName}&culture={cultureName}";
        if (!CacheManager.GetMissingLocalizerByKey(cacheKey))
        {
            var l = localizer[name];
            if (!l.ResourceNotFound)
            {
                ret = l.Value;
            }
            else
            {
                LogSearchedLocation(name);
                CacheManager.AddMissingLocalizerByKey(cacheKey, name);
            }
        }
        return ret;
    }

    private void LogSearchedLocation(string name)
    {
        if (!ignoreLocalizerMissing)
        {
            Logger.LogInformation("{JsonStringLocalizerName} searched for '{Name}' in '{TypeName}' with culture '{CultureName}' not found.", nameof(JsonStringLocalizer), name, typeName, CultureInfo.CurrentUICulture.Name);
        }
    }

    /// <summary>
    /// 获取当前语言的所有资源信息
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public override IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var ret = GetAllStringsFromService(includeParentCultures)
            ?? GetAllStringsFromBase(includeParentCultures)
            ?? GetAllStringsFromJson(includeParentCultures);

        return ret;

        // 1. 从注入服务中获取所有资源信息
        // get all strings from the other inject service
        IEnumerable<LocalizedString>? GetAllStringsFromService(bool includeParentCultures)
        {
            IEnumerable<LocalizedString>? ret = null;
            var localizer = Utility.GetStringLocalizerFromService(Assembly, typeName);
            if (localizer != null)
            {
                ret = localizer.GetAllStrings(includeParentCultures);
            }
            return ret;
        }

        // 2. 从父类 ResourceManagerStringLocalizer 中获取微软格式资源信息
        // get all strings from base json localization factory
        IEnumerable<LocalizedString>? GetAllStringsFromBase(bool includeParentCultures)
        {
            IEnumerable<LocalizedString>? ret = base.GetAllStrings(includeParentCultures);
            try
            {
                CheckMissing();
            }
            catch (MissingManifestResourceException)
            {
                ret = null;
            }
            return ret;

            [ExcludeFromCodeCoverage]
            void CheckMissing() => _ = ret.Any();
        }

        // 3. 从 Json 文件中获取资源信息
        // get all strings from json localization file
        IEnumerable<LocalizedString> GetAllStringsFromJson(bool includeParentCultures) => CacheManager.GetAllStringsByTypeName(Assembly, typeName)
            ?? CacheManager.GetAllStringsFromResolve(includeParentCultures);
    }
}
