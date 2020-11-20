using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

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
        /// <returns></returns>
        public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services)
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
            return services;
        }
    }
}
