// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json;

/// <summary>
/// IStringLocalizerFactory 实现类
/// </summary>
internal class JsonStringLocalizerFactory : ResourceManagerStringLocalizerFactory
{
    private JsonLocalizationOptions Options { get; set; }

    private ILoggerFactory LoggerFactory { get; set; }

    [NotNull]
    private string? TypeName { get; set; }

    private IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsonOptions"></param>
    /// <param name="resxOptions"></param>
    /// <param name="options"></param>
    /// <param name="loggerFactory"></param>
    /// <param name="provider"></param>
    public JsonStringLocalizerFactory(
        IOptions<JsonLocalizationOptions> jsonOptions,
        IOptions<LocalizationOptions> resxOptions,
        IOptionsMonitor<BootstrapBlazorOptions> options,
        ILoggerFactory loggerFactory,
        IServiceProvider provider) : base(resxOptions, loggerFactory)
    {
        Options = jsonOptions.Value;
        Options.FallbackCulture = options.CurrentValue.FallbackCulture;
        Options.EnableFallbackCulture = options.CurrentValue.EnableFallbackCulture;
        LoggerFactory = loggerFactory;
        ServiceProvider = provider;

        options.OnChange(OnChange);

        [ExcludeFromCodeCoverage]
        void OnChange(BootstrapBlazorOptions op)
        {
            Options.FallbackCulture = op.FallbackCulture;
            Options.EnableFallbackCulture = op.EnableFallbackCulture;
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
    /// 
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="baseName"></param>
    /// <returns></returns>
    protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName) => new JsonStringLocalizer(
            this,
            assembly,
            TypeName,
            baseName,
            LoggerFactory.CreateLogger<JsonStringLocalizer>(),
            Options,
            ServiceProvider);

    /// <summary>
    /// 获得 IResourceNamesCache 实例
    /// </summary>
    /// <returns></returns>
    internal IResourceNamesCache GetCache()
    {
        var field = this.GetType().BaseType!.GetField("_resourceNamesCache", BindingFlags.NonPublic | BindingFlags.Instance);
        var ret = field!.GetValue(this) as IResourceNamesCache;
        return ret!;
    }
}
