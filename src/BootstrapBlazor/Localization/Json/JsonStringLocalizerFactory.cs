﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// IStringLocalizerFactory 实现类
/// </summary>
internal class JsonStringLocalizerFactory : ResourceManagerStringLocalizerFactory
{
    private ILoggerFactory LoggerFactory { get; set; }

    private ILocalizationMissingItemHandler LocalizationMissingItemHandler { get; set; }

    [NotNull]
    private string? TypeName { get; set; }

    private bool IgnoreLocalizerMissing { get; set; }

    /// <summary>
    /// 构造函数
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
        IOptionsMonitor<BootstrapBlazorOptions> options,
        IOptions<JsonLocalizationOptions> jsonLocalizationOptions,
        IOptions<LocalizationOptions> localizationOptions,
        ILoggerFactory loggerFactory) : base(localizationOptions, loggerFactory)
    {
        // 由于某些应用场景如 (WTM) Blazor 还未加载时 Localizer 模块先开始工作了
        // 为了保证 CacheManager 内部 Instance 可用这里需要使 ICacheManager 先实例化
        cacheManager.SetStartTime();

        jsonLocalizationOptions.Value.FallbackCulture = options.CurrentValue.FallbackCulture;
        jsonLocalizationOptions.Value.EnableFallbackCulture = options.CurrentValue.EnableFallbackCulture;
        if (options.CurrentValue.IgnoreLocalizerMissing.HasValue)
        {
            jsonLocalizationOptions.Value.IgnoreLocalizerMissing = options.CurrentValue.IgnoreLocalizerMissing.Value;
        }
        IgnoreLocalizerMissing = jsonLocalizationOptions.Value.IgnoreLocalizerMissing;
        LocalizationMissingItemHandler = localizationMissingItemHandler;
        LoggerFactory = loggerFactory;
        options.OnChange(OnChange);

        [ExcludeFromCodeCoverage]
        void OnChange(BootstrapBlazorOptions op)
        {
            jsonLocalizationOptions.Value.EnableFallbackCulture = op.EnableFallbackCulture;
            jsonLocalizationOptions.Value.FallbackCulture = op.FallbackCulture;
            if (op.IgnoreLocalizerMissing.HasValue)
            {
                jsonLocalizationOptions.Value.IgnoreLocalizerMissing = op.IgnoreLocalizerMissing.Value;
                IgnoreLocalizerMissing = op.IgnoreLocalizerMissing.Value;
            }
        }
    }

    /// <summary>
    /// GetResourcePrefix 方法
    /// </summary>
    /// <param name="typeInfo"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
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
        TypeName = typeName;

        return base.GetResourcePrefix(typeInfo);
    }

    /// <summary>
    /// GetResourcePrefix 方法
    /// </summary>
    /// <param name="baseResourceName"></param>
    /// <param name="baseNamespace"></param>
    /// <returns></returns>
    protected override string GetResourcePrefix(string baseResourceName, string baseNamespace)
    {
        // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I5SRA1
        var resourcePrefix = base.GetResourcePrefix(baseResourceName, baseNamespace);
        TypeName = $"{baseNamespace}.{baseResourceName}";

        return resourcePrefix;
    }

    private IResourceNamesCache ResourceNamesCache { get; } = new ResourceNamesCache();

    /// <summary>
    /// Creates a <see cref="ResourceManagerStringLocalizer"/> for the given input
    /// </summary>
    /// <param name="assembly">The assembly to create a <see cref="ResourceManagerStringLocalizer"/> for</param>
    /// <param name="baseName">The base name of the resource to search for</param>
    /// <returns></returns>
    protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName) => new JsonStringLocalizer(assembly, TypeName, baseName, IgnoreLocalizerMissing, LoggerFactory.CreateLogger<JsonStringLocalizer>(), ResourceNamesCache, LocalizationMissingItemHandler);
}
