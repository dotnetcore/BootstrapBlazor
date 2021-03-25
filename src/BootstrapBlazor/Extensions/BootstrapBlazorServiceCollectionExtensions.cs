// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

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
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null, Action<JsonLocalizationOptions>? setupAction = null)
        {
            services.AddAuthorizationCore();
            services.AddJsonLocalization(setupAction);
            services.TryAddScoped<IComponentIdGenerator, DefaultIdGenerator>();
            services.TryAddScoped<ITableExcelExport, DefaultExcelExport>();
            services.TryAddScoped(typeof(IDataService<>), typeof(NullDataService<>));
            services.TryAddScoped<DialogService>();
            services.TryAddScoped<MessageService>();
            services.TryAddScoped<PopoverService>();
            services.TryAddScoped<ToastService>();
            services.TryAddScoped<SwalService>();
            services.AddScoped<TabItemTextOptions>();
            services.AddSingleton<IConfigureOptions<BootstrapBlazorOptions>, ConfigureOptions<BootstrapBlazorOptions>>();
            services.Configure<BootstrapBlazorOptions>(options =>
            {
                configureOptions?.Invoke(options);
            });

            if (OperatingSystem.IsBrowser())
            {
                ServiceProviderHelper.RegisterService(services);
            }
            else
            {
                services.AddHttpContextAccessor();
            }
            return services;
        }

        /// <summary>
        /// 添加 ServiceProvider
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void AddBootstrapBlazor(this IServiceProvider serviceProvider)
        {
            // wasm 模式非常重要
            ServiceProviderHelper.RegisterProvider(serviceProvider);
        }
    }
}
