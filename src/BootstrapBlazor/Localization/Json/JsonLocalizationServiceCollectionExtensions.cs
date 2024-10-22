// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 多语言支持注入服务扩展类
/// </summary>
internal static class JsonLocalizationServiceCollectionExtensions
{
    /// <summary>
    /// 注入 Json 格式多语言服务
    /// </summary>
    /// <param name="services">IServiceCollection 实例</param>
    /// <param name="localizationConfigure">JsonLocalizationOptions 配置回调方法</param>
    /// <returns></returns>
    public static IServiceCollection AddJsonLocalization(this IServiceCollection services, Action<JsonLocalizationOptions>? localizationConfigure = null)
    {
        // 防止被 AddLocalization 覆盖掉
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
