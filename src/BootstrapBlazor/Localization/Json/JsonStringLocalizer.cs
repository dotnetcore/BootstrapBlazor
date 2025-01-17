// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BootstrapBlazor.Components;

/// <summary>
/// JsonStringLocalizer 实现类
/// </summary>
/// <param name="assembly"></param>
/// <param name="typeName"></param>
/// <param name="baseName"></param>
/// <param name="ignoreLocalizerMissing"></param>
/// <param name="logger"></param>
/// <param name="resourceNamesCache"></param>
/// <param name="localizationMissingItemHandler"></param>
internal class JsonStringLocalizer(Assembly assembly, string typeName, string baseName, bool ignoreLocalizerMissing, ILogger logger, IResourceNamesCache resourceNamesCache, ILocalizationMissingItemHandler localizationMissingItemHandler) : ResourceManagerStringLocalizer(new ResourceManager(baseName, assembly), assembly, baseName, resourceNamesCache, logger)
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
            if (localizer != null && localizer is not JsonStringLocalizer)
            {
                ret = GetLocalizerValueFromCache(localizer, name);
            }
            return ret;
        }

        // get string from json localization file
        string? GetStringSafelyFromJson(string name)
        {
            var localizerStrings = MegerResolveLocalizers(CacheManager.GetAllStringsByTypeName(Assembly, typeName));
            return GetValueFromCache(localizerStrings, name);
        }
    }

    private List<LocalizedString> MegerResolveLocalizers(IEnumerable<LocalizedString>? localizerStrings)
    {
        var localizers = new List<LocalizedString>();
        var resolveLocalizers = CacheManager.GetTypeStringsFromResolve(typeName);
        localizers.AddRange(resolveLocalizers);

        if (localizerStrings != null)
        {
            localizers.AddRange(localizerStrings);
        }
        return localizers;
    }

    private readonly HashSet<string> _missingLocalizerCache = [];

    private string? GetValueFromCache(List<LocalizedString> localizerStrings, string name)
    {
        string? ret = null;
        if (!_missingLocalizerCache.Contains(name))
        {
            var l = localizerStrings.Find(i => i.Name == name);
            if (l is { ResourceNotFound: false })
            {
                ret = l.Value;
            }
            else
            {
                HandleMissingResourceItem(name);
                _missingLocalizerCache.Add(name);
            }
        }
        return ret;
    }

    private string? GetLocalizerValueFromCache(IStringLocalizer localizer, string name)
    {
        string? ret = null;
        if (!_missingLocalizerCache.Contains(name))
        {
            var l = localizer[name];
            if (!l.ResourceNotFound)
            {
                ret = l.Value;
            }
            else
            {
                HandleMissingResourceItem(name);
                _missingLocalizerCache.Add(name);
            }
        }
        return ret;
    }

    private void HandleMissingResourceItem(string name)
    {
        localizationMissingItemHandler.HandleMissingItem(name, typeName, CultureInfo.CurrentUICulture.Name);
        if (!ignoreLocalizerMissing)
        {
            Logger.LogInformation("{JsonStringLocalizerName} searched for '{Name}' in '{TypeName}' with culture '{CultureName}' not found.", nameof(JsonStringLocalizer), name, typeName, CultureInfo.CurrentUICulture.Name);
        }
    }

    private List<LocalizedString>? _allLocalizerdStrings;

    /// <summary>
    /// 获取当前语言的所有资源信息
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public override IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        if (_allLocalizerdStrings == null)
        {
            var items = GetAllStringsFromService()
                ?? GetAllStringsFromBase()
                ?? GetAllStringsFromJson();

            _allLocalizerdStrings = MegerResolveLocalizers(items);
        }
        return _allLocalizerdStrings;

        // 1. 从注入服务中获取所有资源信息
        // get all strings from the other inject service
        IEnumerable<LocalizedString>? GetAllStringsFromService()
        {
            IEnumerable<LocalizedString>? ret = null;
            var localizer = Utility.GetStringLocalizerFromService(Assembly, typeName);
            if (localizer != null && localizer is not JsonStringLocalizer)
            {
                ret = localizer.GetAllStrings(includeParentCultures);
            }
            return ret;
        }

        // 2. 从父类 ResourceManagerStringLocalizer 中获取微软格式资源信息
        // get all strings from base json localization factory
        IEnumerable<LocalizedString>? GetAllStringsFromBase()
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
        IEnumerable<LocalizedString>? GetAllStringsFromJson() => CacheManager.GetAllStringsByTypeName(Assembly, typeName);
    }
}
