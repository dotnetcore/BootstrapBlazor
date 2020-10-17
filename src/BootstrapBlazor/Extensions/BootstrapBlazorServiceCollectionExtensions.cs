using BootstrapBlazor.Components;
using BootstrapBlazor.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            services.TryAddSingleton<IComponentIdGenerator, DefaultIdGenerator>();
            services.TryAddScoped<DialogService>();
            services.TryAddScoped<MessageService>();
            services.TryAddScoped<PopoverService>();
            services.TryAddScoped<ToastService>();
            services.TryAddScoped<SwalService>();
            return services;
        }
    }
}
