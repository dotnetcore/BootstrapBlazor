// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// BootstrapBlazor 服务扩展类
    /// </summary>
    public static class BootstrapBlazorServiceCollectionExtensions
    {
        /// <summary>
        /// 增加 BootstrapBlazor 服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <param name="localizationAction"></param>
        /// <param name="locatorAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null, Action<JsonLocalizationOptions>? localizationAction = null, Action<IPLocatorOption>? locatorAction = null)
        {
            services.AddAuthorizationCore();
            services.AddJsonLocalization(localizationAction);
            services.TryAddScoped<IComponentIdGenerator, DefaultIdGenerator>();
            services.TryAddScoped<ITableExcelExport, DefaultExcelExport>();
            services.TryAddScoped(typeof(IDataService<>), typeof(NullDataService<>));
            services.TryAddScoped<DialogService>();
            services.TryAddScoped<MessageService>();
            services.TryAddScoped<PopoverService>();
            services.TryAddScoped<ToastService>();
            services.TryAddScoped<SwalService>();
            services.TryAddScoped<FullScreenService>();
            services.TryAddScoped<TabItemTextOptions>();
            services.TryAddScoped<TitleService>();
            services.TryAddScoped<DownloadService>();
            services.TryAddScoped<WebClientService>();
            services.TryAddSingleton<IConfigureOptions<BootstrapBlazorOptions>, ConfigureOptions<BootstrapBlazorOptions>>();
            services.Configure<BootstrapBlazorOptions>(options =>
            {
                configureOptions?.Invoke(options);
            });

            services.AddHttpClient();
            services.TryAddSingleton<IIPLocatorProvider, DefaultIPLocatorProvider>();
            services.Configure<IPLocatorOption>(options =>
            {
                locatorAction?.Invoke(options);
            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseBootstrapBlazor(this IApplicationBuilder builder)
        {
            // 获得客户端 IP 地址
            builder.UseWhen(context => context.Request.Path.StartsWithSegments("/ip.axd"), app => app.Run(async context =>
            {
                var ip = "";
                var headers = context.Request.Headers;
                if (headers.ContainsKey("X-Forwarded-For"))
                {
                    var ips = new List<string>();
                    foreach (var xf in headers["X-Forwarded-For"])
                    {
                        if (!string.IsNullOrEmpty(xf))
                        {
                            ips.Add(xf);
                        }
                    }
                    ip = string.Join(";", ips);
                }
                else
                {
                    ip = context.Connection.RemoteIpAddress.ToIPv4String();
                }

                context.Response.Headers.Add("Content-Type", new Microsoft.Extensions.Primitives.StringValues("application/json; charset=utf-8"));
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Id = context.TraceIdentifier, Ip = ip }));
            }));
            return builder;
        }
    }
}
