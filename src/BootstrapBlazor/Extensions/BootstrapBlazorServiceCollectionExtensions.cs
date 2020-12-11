// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;

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
        /// <returns></returns>
        public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null)
        {
            services.AddJsonLocalization();
            services.AddSingleton<IComponentIdGenerator, DefaultIdGenerator>();
            services.AddSingleton<ITableExcelExport, DefaultExcelExport>();
            services.AddScoped<DialogService>();
            services.AddScoped<MessageService>();
            services.AddScoped<PopoverService>();
            services.AddScoped<ToastService>();
            services.AddScoped<SwalService>();
            services.AddSingleton<IConfigureOptions<BootstrapBlazorOptions>, ConfigureOptions<BootstrapBlazorOptions>>();
            services.Configure<BootstrapBlazorOptions>(options =>
            {
                configureOptions?.Invoke(options);

                // fix(#I2925C): https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I2925C
                if (CultureInfo.CurrentUICulture.Name == "en")
                {
                    CultureInfo.CurrentUICulture = new CultureInfo(options.DefaultUICultureInfoName ?? "en-US");
                }
            });

            return services;
        }
    }
}
