// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    private Assembly Assembly { get; set; }

    private string TypeName { get; set; }

    private ILogger Logger { get; set; }

    private JsonLocalizationOptions Options { get; set; }

    private IServiceProvider ServiceProvider { get; set; }

    private ILocalizationResolve LocalizerResolver { get; set; }

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
        Assembly = assembly;
        TypeName = typeName;
        Logger = logger;
        Options = options;
        ServiceProvider = provider;
        LocalizerResolver = provider.GetRequiredService<ILocalizationResolve>();
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
        var factorys = ServiceProvider.GetService<IEnumerable<IStringLocalizerFactory>>();
        if (factorys != null)
        {
            var factory = factorys.LastOrDefault(a => a is not JsonStringLocalizerFactory);
            if (factory != null)
            {
                var type = Assembly.GetType(TypeName);
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
                // 从 Resource 文件中获取
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
        var factorys = ServiceProvider.GetService<IEnumerable<IStringLocalizerFactory>>();
        if (factorys != null)
        {
            var factory = factorys.LastOrDefault(a => a is not JsonStringLocalizerFactory);
            if (factory != null)
            {
                var type = Assembly.GetType(TypeName);
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
    /// GetAllJsonStrings 方法
    /// </summary>
    /// <param name="includeParentCultures"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    protected virtual IEnumerable<LocalizedString> GetAllJsonStrings(bool includeParentCultures)
    {
        var resourceNames = includeParentCultures
            ? GetAllStringsFromCultureHierarchy(CultureInfo.CurrentUICulture)
            : GetAllResourceStrings(GetCultureName(CultureInfo.CurrentUICulture));

        foreach (var name in resourceNames)
        {
            var value = GetJsonStringSafely(name);
            yield return new LocalizedString(name, value!, false, _searchedLocation);
        }
    }

    [ExcludeFromCodeCoverage]
    private string GetCultureName(CultureInfo culture)
    {
        var cultureInfoName = culture.Name;
        if (string.IsNullOrEmpty(cultureInfoName) && Options.EnableFallbackCulture)
        {
            cultureInfoName = Options.FallbackCulture;
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

        // localization resolve
        var value = resource.Value ?? LocalizerResolver.GetJsonStringByCulture(culture, name);
        if (value == null)
        {
            Logger.LogInformation($"{nameof(JsonStringLocalizer)} searched for '{name}' in '{_searchedLocation}' with culture '{culture}' not found.");
        }
        return value;
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
        ServiceProvider.GetRequiredService<ICacheManager>()
        .GetJsonStringByCulture(cultureName, Options, Assembly, TypeName);
}
