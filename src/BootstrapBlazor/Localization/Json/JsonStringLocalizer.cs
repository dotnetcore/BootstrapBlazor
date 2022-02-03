// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// JsonStringLocalizer 实现类
/// </summary>
internal class JsonStringLocalizer : ResourceManagerStringLocalizer
{
    private readonly Assembly _assembly;
    private readonly string _typeName;
    private readonly ILogger _logger;
    private readonly JsonLocalizationOptions _options;
    private readonly IServiceProvider _provider;

    private readonly string _searchedLocation = "";

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="assembly"></param>
    /// <param name="typeName"></param>
    /// <param name="baseName"></param>
    /// <param name="logger"></param>
    /// <param name="options"></param>
    /// <param name="provider"></param>
    public JsonStringLocalizer(
        JsonStringLocalizerFactory factory,
        Assembly assembly,
        string typeName,
        string baseName,
        ILogger logger,
        JsonLocalizationOptions options,
        IServiceProvider provider) : base(new ResourceManager(baseName, assembly), assembly, baseName, factory.GetCache(), logger)
    {
        _assembly = assembly;
        _typeName = typeName;
        _logger = logger;
        _options = options;
        _provider = provider;
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
            var value = GetStringFromInject(name)
                ?? GetStringSafely(name, CultureInfo.CurrentUICulture)
                ?? GetJsonStringSafely(name);

            return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
        }
    }

    private string? GetStringFromInject(string name)
    {
        string? ret = null;
        var factorys = _provider.GetService<IEnumerable<IStringLocalizerFactory>>();
        if (factorys != null)
        {
            var factory = factorys.LastOrDefault(a => a is not JsonStringLocalizerFactory);
            if (factory != null)
            {
                var type = _assembly.GetType(_typeName);
                if (type != null)
                {
                    var localizer = factory.Create(type);
                    var l = localizer[name];
                    ret = l.ResourceNotFound ? null : l.Value;
                }
            }
        }
        return ret;
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
            var format = GetStringFromInject(name)
                ?? GetStringSafely(name, CultureInfo.CurrentUICulture)
                ?? GetJsonStringSafely(name);
            var value = !string.IsNullOrEmpty(format) ? string.Format(format, arguments) : name;
            return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: _searchedLocation);
        }
    }

    /// <summary>
    /// 获取当前语言的所有资源信息
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    public override IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var ret = GetAllStringFromInject(includeParentCultures);
        if (ret == null)
        {
            try
            {
                ret = base.GetAllStrings(includeParentCultures).ToList();
            }
            catch (MissingManifestResourceException)
            {
                ret = Enumerable.Empty<LocalizedString>();
            }
        }
        if (!ret.Any())
        {
            ret = GetAllJsonStrings(includeParentCultures);
        }
        return ret;
    }

    private IEnumerable<LocalizedString>? GetAllStringFromInject(bool includeParentCultures)
    {
        IEnumerable<LocalizedString>? ret = null;
        var factorys = _provider.GetService<IEnumerable<IStringLocalizerFactory>>();
        if (factorys != null)
        {
            var factory = factorys.LastOrDefault(a => a is not JsonStringLocalizerFactory);
            if (factory != null)
            {
                var type = _assembly.GetType(_typeName);
                if (type != null)
                {
                    var localizer = factory.Create(type);
                    ret = localizer.GetAllStrings(includeParentCultures);
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    protected virtual IEnumerable<LocalizedString> GetAllJsonStrings(bool includeParentCultures)
    {
        var cultureInfoName = GetCultureName(CultureInfo.CurrentUICulture);
        var resourceNames = includeParentCultures
            ? GetAllStringsFromCultureHierarchy(CultureInfo.CurrentUICulture)
            : GetAllResourceStrings(cultureInfoName);

        foreach (var name in resourceNames)
        {
            var value = GetJsonStringSafely(name);
            yield return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
        }
    }

    private string GetCultureName(CultureInfo culture)
    {
        var cultureInfoName = culture.Name;
        if (string.IsNullOrEmpty(cultureInfoName) && _options.FallBackToParentUICultures)
        {
            cultureInfoName = _options.FallbackCulture;
        }
        return cultureInfoName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    protected virtual string? GetJsonStringSafely(string name) => GetStringByCulture(CultureInfo.CurrentUICulture, name);

    private string? GetStringByCulture(CultureInfo culture, string name)
    {
        var cultureName = GetCultureName(culture);
        var resources = GetJsonStringByCulture(cultureName);
        var resource = resources.FirstOrDefault(s => s.Key == name);
        _logger.LogDebug($"{nameof(JsonStringLocalizer)} searched for '{name}' in '{_searchedLocation}' with culture '{culture}'.");
        return resource.Value;
    }

    private IEnumerable<string> GetAllStringsFromCultureHierarchy(CultureInfo culture)
    {
        var currentCulture = culture;
        var resourceNames = new HashSet<string>();
        while (currentCulture != currentCulture.Parent)
        {
            var cultureResourceNames = GetAllResourceStrings(GetCultureName(currentCulture));
            foreach (var resourceName in cultureResourceNames)
            {
                resourceNames.Add(resourceName);
            }
            currentCulture = currentCulture.Parent;
        }
        return resourceNames;
    }

    private IEnumerable<string> GetAllResourceStrings(string cultureName)
    {
        var resources = GetJsonStringByCulture(cultureName);
        return resources.Select(r => r.Key);
    }

    private IEnumerable<KeyValuePair<string, string>> GetJsonStringByCulture(string cultureName) =>
        _provider.GetRequiredService<ICacheManager>()
        .GetJsonStringByCulture(cultureName, _options, _assembly, _typeName);
}
