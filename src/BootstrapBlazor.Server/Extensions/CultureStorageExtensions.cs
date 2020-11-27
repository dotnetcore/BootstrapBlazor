// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CultureStorageExtensions
    {
        /// <summary>
        /// 添加本地化持久化策略服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCultureStorage(this IServiceCollection services)
        {
            services.AddSingleton<ICultureStorage, DefaultCultureStorage>();
            return services;
        }

        internal class DefaultCultureStorage : ICultureStorage
        {
            public CultureStorageMode Mode { get; set; }
        }
    }
}
