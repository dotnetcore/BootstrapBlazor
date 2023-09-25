// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// JsonStringLocalizer 实现类
/// </summary>
internal class JsonStringLocalizer : ResourceManagerStringLocalizer
{
    private Assembly Assembly { get; set; }

    private string TypeName { get; set; }

    private bool IgnoreLocalizerMissing { get; set; }

    private ILogger Logger { get; set; }

    private ConcurrentDictionary<string, object?> MissingLocalizerCache { get; } = new();
    
    private Func<string, string?>[] ResouceSearchFuncArray;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="typeName"></param>
    /// <param name="baseName"></param>
    /// <param name="ignoreLocalizerMissing"></param>
    /// <param name="logger"></param>
    /// <param name="resourceNamesCache"></param>
    public JsonStringLocalizer(
        Assembly assembly,
        string typeName,
        string baseName,
        bool ignoreLocalizerMissing,
        ILogger logger,
        IResourceNamesCache resourceNamesCache,
        string resources) : base(new ResourceManager(baseName, assembly), assembly, baseName, resourceNamesCache, logger)
    {
        Assembly = assembly;
        TypeName = typeName;
        IgnoreLocalizerMissing = ignoreLocalizerMissing;
        Logger = logger;
        ResouceSearchFuncArray = InitResourceSearchFuncDict(resources);
    }
    /// <summary>
    /// 初始化查找Func数组
    /// </summary>
    /// <param name="resources">用户配置的资源名及顺序字符串</param>
    /// <returns></returns>
    private Func<string, string?>[] InitResourceSearchFuncDict(string resources)
    {
        ConcurrentDictionary<string, Func<string, string?>> ResouceSearchFuncDict = GetInitResouceSearchFuncDict();
        string[] configResources = ensureIncludeBB(resources);
        ResouceSearchFuncArray = configResources.Join(ResouceSearchFuncDict, s => s, t => t.Key, (s, t) => t.Value).ToArray(); 
        return ResouceSearchFuncArray;

        ConcurrentDictionary<string, Func<string, string?>> GetInitResouceSearchFuncDict()
        {
            ConcurrentDictionary<string, Func<string, string?>> dict = new();
            dict.TryAdd("abp", GetStringFromService);
            dict.TryAdd("wtm", name => GetStringSafely(name, null));
            dict.TryAdd("bb", GetStringSafelyFromJson);
            return dict;
        }
        string? GetStringFromService(string name)
        {
            string? ret = null;
            var localizer = Utility.GetStringLocalizerFromService(Assembly, TypeName);
            if (localizer != null)
            {
                ret = GetLocalizerValueFromCache(localizer, name);
            }
            return ret;
        }
        // get string from json localization file
        string? GetStringSafelyFromJson(string name)
        {
            var localizerStrings = CacheManager.GetAllStringsByTypeName(Assembly, TypeName);
            return GetValueFromCache(localizerStrings, name);
        }
        string[] ensureIncludeBB(string searchString)
        {
            string[] keys = searchString.ToLower().Split(",");
            if (!keys.Contains("bb"))
            {
                keys = keys.Append("bb").ToArray();
            }
            return keys;
        }
    }
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
            return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: TypeName);
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
            return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: TypeName);

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
                    Logger.LogError(ex, "{JsonStringLocalizerName} searched for '{Name}' in '{TypeName}' with culture '{CultureName}' throw exception.", nameof(JsonStringLocalizer), name, TypeName, CultureInfo.CurrentUICulture.Name);
                }
                return ret;
            }
        }
    }

    private string? GetStringSafely(string name)
    {
        string? result = null;
        for (int i = 0; i < ResouceSearchFuncArray.Length; i++)

        // get string from inject service
        {
            result = ResouceSearchFuncArray[i](name);
            if (result != null)
            {
                break;
        }

        // get string from json localization file
        }
        return result;
    }

    private string? GetValueFromCache(IEnumerable<LocalizedString>? localizerStrings, string name)
    {
        string? ret = null;
        var cultureName = CultureInfo.CurrentUICulture.Name;
        var cacheKey = $"{nameof(GetValueFromCache)}&name={name}&{Assembly.GetName().Name}&type={TypeName}&culture={cultureName}";
        if (!MissingLocalizerCache.ContainsKey(cacheKey))
        {
            var l = GetLocalizedString();
            if (l is { ResourceNotFound: false })
            {
                ret = l.Value;
            }
            else
            {
                LogSearchedLocation(name);
                MissingLocalizerCache.TryAdd(cacheKey, null);
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
        var cacheKey = $"{nameof(GetLocalizerValueFromCache)}&name={name}&{Assembly.GetName().Name}&type={TypeName}&culture={cultureName}";
        if (!MissingLocalizerCache.ContainsKey(cacheKey))
        {
            var l = localizer[name];
            if (l.ResourceNotFound)
            {
                LogSearchedLocation(name);
                MissingLocalizerCache.TryAdd(cacheKey, null);
            }
            else
            {
                ret = l.Value;
            }
        }
        return ret;
    }

    private void LogSearchedLocation(string name)
    {
        if (!IgnoreLocalizerMissing)
        {
            Logger.LogInformation("{JsonStringLocalizerName} searched for '{Name}' in '{TypeName}' with culture '{CultureName}' not found.", nameof(JsonStringLocalizer), name, TypeName, CultureInfo.CurrentUICulture.Name);
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
            var localizer = Utility.GetStringLocalizerFromService(Assembly, TypeName);
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
        IEnumerable<LocalizedString> GetAllStringsFromJson(bool includeParentCultures) => CacheManager.GetAllStringsByTypeName(Assembly, TypeName)
            ?? CacheManager.GetAllStringsFromResolve(includeParentCultures);
    }
}
