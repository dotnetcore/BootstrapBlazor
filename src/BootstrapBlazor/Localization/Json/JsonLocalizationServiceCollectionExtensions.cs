// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <para lang="zh">多语言支持注入服务扩展类</para>
/// <para lang="en">Multi-language support injection service extension class</para>
/// </summary>
internal static class JsonLocalizationServiceCollectionExtensions
{
    /// <summary>
    /// <para lang="zh">注入 Json 格式多语言服务</para>
    /// <para lang="en">Inject Json format multi-language service</para>
    /// </summary>
    /// <param name="services">
    /// <para lang="zh">IServiceCollection 实例</para>
    /// <para lang="en">IServiceCollection instance</para></param>
    /// <param name="localizationConfigure">
    /// <para lang="zh">JsonLocalizationOptions 配置回调方法</para>
    /// <para lang="en">JsonLocalizationOptions configuration callback method</para>
    /// </param>
    /// <returns></returns>
    public static IServiceCollection AddJsonLocalization(this IServiceCollection services, Action<JsonLocalizationOptions>? localizationConfigure = null)
    {
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        services.TryAddTransient<IStringLocalizer, StringLocalizer>();
        services.TryAddSingleton<ILocalizationResolve, NullLocalizationResolve>();
        services.TryAddSingleton<ILocalizationMissingItemHandler, NullLocalizationMissingItemHandler>();
        if (localizationConfigure != null)
        {
            services.Configure(localizationConfigure);
        }
        return services;
    }
}
