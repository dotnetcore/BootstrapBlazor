// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">JsonStringLocalizer 实现类</para>
/// <para lang="en">JsonStringLocalizer implementation class</para>
/// </summary>
/// <param name="assembly"></param>
/// <param name="typeName"></param>
/// <param name="baseName"></param>
/// <param name="jsonLocalizationOptions"></param>
/// <param name="logger"></param>
/// <param name="resourceNamesCache"></param>
/// <param name="localizationMissingItemHandler"></param>
internal class JsonStringLocalizer(Assembly assembly, string typeName, string baseName, JsonLocalizationOptions jsonLocalizationOptions, ILogger logger, IResourceNamesCache resourceNamesCache, ILocalizationMissingItemHandler localizationMissingItemHandler) : ResourceManagerStringLocalizer(new ResourceManager(baseName, assembly), assembly, baseName, resourceNamesCache, logger)
{
    private Assembly Assembly { get; } = assembly;

    private ILogger Logger { get; } = logger;

    /// <summary>
    /// <para lang="zh">通过指定键值获取多语言值信息索引</para>
    /// <para lang="en">Get multi-language value info index by specified key</para>
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
    /// <para lang="zh">带格式化参数的通过指定键值获取多语言值信息索引</para>
    /// <para lang="en">Get multi-language value info index by specified key with format arguments</para>
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

    private string? GetStringSafely(string name) => GetStringFromService(name) ?? GetStringFromResourceManager(name) ?? GetStringFromJson(name);

    private string? GetStringFromService(string name)
    {
        // <para lang="zh">get string from inject service</para>
        // <para lang="en">get string from inject service</para>
        string? ret = null;
        if (jsonLocalizationOptions.DisableGetLocalizerFromService == false)
        {
            var localizer = Utility.GetStringLocalizerFromService(Assembly, typeName);
            if (localizer != null && localizer is not JsonStringLocalizer)
            {
                var l = localizer[name];
                if (!l.ResourceNotFound)
                {
                    ret = l.Value;
                }
            }
        }
        return ret;
    }

    private string? GetStringFromResourceManager(string name)
    {
        string? ret = null;
        if (jsonLocalizationOptions.DisableGetLocalizerFromResourceManager == false)
        {
            ret = GetStringSafely(name, CultureInfo.CurrentUICulture);
        }
        return ret;
    }

    private readonly ConcurrentDictionary<string, object?> _missingManifestCache = [];
    private string? GetStringFromJson(string name)
    {
        // <para lang="zh">从 json 本地化文件中获取字符串</para>
        // <para lang="en">get string from json localization file</para>
        var localizerStrings = MergeResolveLocalizers(CacheManager.GetAllStringsByTypeName(Assembly, typeName));
        var cacheKey = $"name={name}&culture={CultureInfo.CurrentUICulture.Name}";
        string? ret = null;
        if (!_missingManifestCache.ContainsKey(cacheKey))
        {
            var l = localizerStrings.Find(i => i.Name == name);
            if (l is { ResourceNotFound: false })
            {
                ret = l.Value;
            }
            else
            {
                // <para lang="zh">如果没有找到资源信息则尝试从父类中查找</para>
                // <para lang="en">If resource info not found, try to find from base class</para>
                ret ??= GetStringFromBaseType(name);

                if (ret is null)
                {
                    // <para lang="zh">加入缺失资源信息缓存中</para>
                    // <para lang="en">Add to missing resource info cache</para>
                    HandleMissingResourceItem(name);
                }
            }
        }
        return ret;
    }

    private string? GetStringFromBaseType(string name)
    {
        string? ret = null;
        var type = Assembly.GetType(typeName);
        var propertyInfo = type?.GetPropertyByName(name);
        if (propertyInfo is { DeclaringType: not null })
        {
            var baseType = propertyInfo.DeclaringType;
            if (baseType != type)
            {
                var baseAssembly = baseType.Assembly;
                var localizerStrings = MergeResolveLocalizers(CacheManager.GetAllStringsByTypeName(baseAssembly, baseType.FullName!));
                var l = localizerStrings.Find(i => i.Name == name);
                if (l is { ResourceNotFound: false })
                {
                    ret = l.Value;
                }
            }
        }
        return ret;
    }

    private List<LocalizedString> MergeResolveLocalizers(IEnumerable<LocalizedString>? localizerStrings)
    {
        var localizers = new List<LocalizedString>(CacheManager.GetTypeStringsFromResolve(typeName));
        if (localizerStrings != null)
        {
            localizers.AddRange(localizerStrings);
        }
        return localizers;
    }

    private void HandleMissingResourceItem(string name)
    {
        localizationMissingItemHandler.HandleMissingItem(name, typeName, CultureInfo.CurrentUICulture.Name);
        if (jsonLocalizationOptions.IgnoreLocalizerMissing == false)
        {
            Logger.LogInformation("{JsonStringLocalizerName} searched for '{Name}' in '{TypeName}' with culture '{CultureName}' not found.", nameof(JsonStringLocalizer), name, typeName, CultureInfo.CurrentUICulture.Name);
        }
        _missingManifestCache.TryAdd($"name={name}&culture={CultureInfo.CurrentUICulture.Name}", null);
    }

    private List<LocalizedString>? _allLocalizedStrings;

    /// <summary>
    /// <para lang="zh">获取当前语言的所有资源信息</para>
    /// <para lang="en">Get all resource info of current culture</para>
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public override IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        if (_allLocalizedStrings == null)
        {
            var items = GetAllStringsFromService()
                ?? GetAllStringsFromBase()
                ?? GetAllStringsFromJson();

            _allLocalizedStrings = MergeResolveLocalizers(items);
        }
        return _allLocalizedStrings;

        // <para lang="zh">1. 从注入服务中获取所有资源信息</para>
        // <para lang="en">1. Get all resource info from injected service</para>
        // get all strings from the other inject service
        IEnumerable<LocalizedString>? GetAllStringsFromService()
        {
            IEnumerable<LocalizedString>? ret = null;
            if (jsonLocalizationOptions.DisableGetLocalizerFromService == false)
            {
                var localizer = Utility.GetStringLocalizerFromService(Assembly, typeName);
                if (localizer != null && localizer is not JsonStringLocalizer)
                {
                    ret = localizer.GetAllStrings(includeParentCultures);
                }
            }
            return ret;
        }

        // <para lang="zh">2. 从父类 ResourceManagerStringLocalizer 中获取微软格式资源信息</para>
        // <para lang="en">2. Get Microsoft format resource info from base class ResourceManagerStringLocalizer</para>
        // get all strings from base json localization factory
        IEnumerable<LocalizedString>? GetAllStringsFromBase()
        {
            IEnumerable<LocalizedString>? ret = null;
            if (jsonLocalizationOptions.DisableGetLocalizerFromResourceManager == false)
            {
                ret = base.GetAllStrings(includeParentCultures);
                try
                {
                    CheckMissing();
                }
                catch (MissingManifestResourceException)
                {
                    ret = null;
                }
            }
            return ret;

            [ExcludeFromCodeCoverage]
            void CheckMissing() => _ = ret.Any();
        }

        // <para lang="zh">3. 从 Json 文件中获取资源信息</para>
        // <para lang="en">3. Get resource info from Json file</para>
        // get all strings from json localization file
        IEnumerable<LocalizedString>? GetAllStringsFromJson() => CacheManager.GetAllStringsByTypeName(Assembly, typeName);
    }
}
