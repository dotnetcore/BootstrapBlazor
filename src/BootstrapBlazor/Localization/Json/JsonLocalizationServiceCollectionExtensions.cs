// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
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
    /// <param name="localizationAction">JsonLocalizationOptions 配置回调方法</param>
    /// <returns></returns>
    public static IServiceCollection AddJsonLocalization(this IServiceCollection services, Action<JsonLocalizationOptions>? localizationAction = null)
    {
        services.AddOptions();

        AddJsonLocalizationServices(services, localizationAction);

        return services;
    }

    private static void AddJsonLocalizationServices(IServiceCollection services, Action<JsonLocalizationOptions>? localizationAction = null)
    {
        // 防止被 AddLocalization 覆盖掉
        services.AddSingleton<IHtmlLocalizerFactory, JsonHtmlLocalizerFactory>();
        services.AddSingleton(typeof(IHtmlLocalizer<>), typeof(HtmlLocalizer<>));
        services.AddSingleton(typeof(IHtmlLocalizer), typeof(HtmlLocalizer));

        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        services.AddSingleton(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        services.AddSingleton(typeof(IStringLocalizer), typeof(StringLocalizer));

        if (localizationAction != null) services.Configure(localizationAction);
    }
}
