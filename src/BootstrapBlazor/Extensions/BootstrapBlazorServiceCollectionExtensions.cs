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
            services.TryAddScoped<TabItemTextOptions>();
            services.TryAddScoped<TitleService>();
            services.TryAddScoped<DownloadService>();
            services.TryAddSingleton<IConfigureOptions<BootstrapBlazorOptions>, ConfigureOptions<BootstrapBlazorOptions>>();
            services.Configure<BootstrapBlazorOptions>(options =>
            {
                configureOptions?.Invoke(options);
            });
            ServiceProviderHelper.RegisterService(services);
            return services;
        }
    }
}
