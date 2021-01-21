// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Linq;

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
            services.AddJsonLocalization(setupAction);
            services.TryAddSingleton<IComponentIdGenerator, DefaultIdGenerator>();
            services.TryAddSingleton<ITableExcelExport, DefaultExcelExport>();
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

                // fix(#I2925C): https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I2925C
                if (CultureInfo.CurrentUICulture.Name == "en" || !options.SupportedCultures.Any(c => c.Equals(CultureInfo.CurrentUICulture.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    CultureInfo.CurrentUICulture = new CultureInfo(options.DefaultUICultureInfoName ?? "en-US");
                }
            });

            ServiceProviderHelper.RegisterService(services);

            return services;
        }
    }
}
