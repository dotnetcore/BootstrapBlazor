// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureOptions<TOption> : ConfigureFromConfigurationOptions<TOption> where TOption : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ConfigureOptions(IConfiguration config)
            : base(config.GetSection(typeof(TOption).Name))
        {

        }
    }
}
