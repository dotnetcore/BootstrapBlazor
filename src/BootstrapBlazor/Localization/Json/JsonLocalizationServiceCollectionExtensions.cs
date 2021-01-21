// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 多语言支持注入服务扩展类
    /// </summary>
    public static class JsonLocalizationServiceCollectionExtensions
    {
        /// <summary>
        /// 注入 Json 格式多语言服务
        /// </summary>
        /// <param name="services">IServiceCollection 实例</param>
        /// <param name="setupAction">JsonLocalizationOptions 配置回调方法</param>
        /// <returns></returns>
        public static IServiceCollection AddJsonLocalization(this IServiceCollection services, Action<JsonLocalizationOptions>? setupAction = null)
        {
            services.AddOptions();

            AddJsonLocalizationServices(services, setupAction);

            return services;
        }

        private static void AddJsonLocalizationServices(IServiceCollection services, Action<JsonLocalizationOptions>? setupAction = null)
        {
            services.TryAddSingleton<IHtmlLocalizerFactory, JsonHtmlLocalizerFactory>();
            services.TryAddScoped(typeof(IHtmlLocalizer<>), typeof(HtmlLocalizer<>));
            services.TryAddScoped(typeof(IHtmlLocalizer), typeof(HtmlLocalizer));

            services.TryAddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.TryAddScoped(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            services.TryAddScoped(typeof(IStringLocalizer), typeof(StringLocalizer));

            if (setupAction != null) services.Configure(setupAction);
        }
    }
}
