// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IStringLocalizerFactory 实现类</para>
/// <para lang="en">IStringLocalizerFactory implementation class</para>
/// </summary>
internal class JsonStringLocalizerFactory : ResourceManagerStringLocalizerFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly JsonLocalizationOptions _jsonLocalizationOptions;
    private readonly ILocalizationMissingItemHandler _localizationMissingItemHandler;
    private string? _typeName;

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="cacheManager"></param>
    /// <param name="localizationMissingItemHandler"></param>
    /// <param name="options"></param>
    /// <param name="jsonLocalizationOptions"></param>
    /// <param name="localizationOptions"></param>
    /// <param name="loggerFactory"></param>
    public JsonStringLocalizerFactory(
        ICacheManager cacheManager,
        ILocalizationMissingItemHandler localizationMissingItemHandler,
        IOptions<BootstrapBlazorOptions> options,
        IOptions<JsonLocalizationOptions> jsonLocalizationOptions,
        IOptions<LocalizationOptions> localizationOptions,
        ILoggerFactory loggerFactory) : base(localizationOptions, loggerFactory)
    {
        // <para lang="zh">由于某些应用场景如 (WTM) Blazor 还未加载时 Localizer 模块先开始工作了</para>
        // <para lang="en">Because in some application scenarios such as (WTM) Blazor, Localizer module starts working before loading</para>
        // <para lang="zh">为了保证 CacheManager 内部 Instance 可用这里需要使 ICacheManager 先实例化</para>
        // <para lang="en">In order to ensure that the internal Instance of CacheManager is available, ICacheManager needs to be instantiated first</para>
        cacheManager.SetStartTime();

        jsonLocalizationOptions.Value.FallbackCulture = options.Value.FallbackCulture;
        jsonLocalizationOptions.Value.EnableFallbackCulture = options.Value.EnableFallbackCulture;
        if (options.Value.IgnoreLocalizerMissing.HasValue)
        {
            jsonLocalizationOptions.Value.IgnoreLocalizerMissing = options.Value.IgnoreLocalizerMissing.Value;
        }
        if (options.Value.DisableGetLocalizerFromService.HasValue)
        {
            jsonLocalizationOptions.Value.DisableGetLocalizerFromService = options.Value.DisableGetLocalizerFromService.Value;
        }
        if (options.Value.DisableGetLocalizerFromResourceManager.HasValue)
        {
            jsonLocalizationOptions.Value.DisableGetLocalizerFromResourceManager = options.Value.DisableGetLocalizerFromResourceManager.Value;
        }
        _localizationMissingItemHandler = localizationMissingItemHandler;
        _loggerFactory = loggerFactory;
        _jsonLocalizationOptions = jsonLocalizationOptions.Value;
    }

    /// <summary>
    /// <para lang="zh">GetResourcePrefix 方法</para>
    /// <para lang="en">GetResourcePrefix method</para>
    /// </summary>
    /// <param name="typeInfo"></param>
    protected override string GetResourcePrefix(TypeInfo typeInfo)
    {
        var typeName = typeInfo.FullName;
        if (string.IsNullOrEmpty(typeName))
        {
            throw new InvalidOperationException($"{nameof(typeInfo)} full name is null or String.Empty.");
        }

        if (typeInfo.IsGenericType)
        {
            var index = typeName.IndexOf('`');
            typeName = typeName[..index];
        }
        _typeName = typeName;

        return base.GetResourcePrefix(typeInfo);
    }

    /// <summary>
    /// <para lang="zh">GetResourcePrefix 方法</para>
    /// <para lang="en">GetResourcePrefix method</para>
    /// </summary>
    /// <param name="baseResourceName"></param>
    /// <param name="baseNamespace"></param>
    protected override string GetResourcePrefix(string baseResourceName, string baseNamespace)
    {
        // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I5SRA1
        var resourcePrefix = base.GetResourcePrefix(baseResourceName, baseNamespace);
        _typeName = $"{baseNamespace}.{baseResourceName}";

        return resourcePrefix;
    }

    private IResourceNamesCache ResourceNamesCache { get; } = new ResourceNamesCache();

    /// <summary>
    /// Creates a <see cref="ResourceManagerStringLocalizer"/> for the given input
    /// </summary>
    /// <param name="assembly"><para lang="zh">The assembly to create a <see cref="ResourceManagerStringLocalizer"/> for</para><para lang="en">The assembly to create a <see cref="ResourceManagerStringLocalizer"/> for</para></param>
    /// <param name="baseName"><para lang="zh">The base name of the resource to search for</para><para lang="en">The base name of the resource to search for</para></param>
    protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName) => new JsonStringLocalizer(assembly, _typeName!, baseName, _jsonLocalizationOptions, _loggerFactory.CreateLogger<JsonStringLocalizer>(), ResourceNamesCache, _localizationMissingItemHandler);
}
