// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：Apache-2.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ServiceProviderHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static ServiceProvider ServiceProvider { get { return _lazy.Value; } }

        [NotNull]
        private static Lazy<ServiceProvider>? _lazy;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterService(IServiceCollection services)
        {
            _lazy = new Lazy<ServiceProvider>(() => services.BuildServiceProvider());
        }
    }
}
