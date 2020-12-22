// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：Apache-2.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ColumnName 属性标签用于标示列头显示信息支持多语言
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        /// <summary>
        /// 获得/设置 列名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 获得/设置 资源文件键值名称 为空时使用当前属性名
        /// </summary>
        public string? ResourceName { get; set; }

        /// <summary>
        /// 获得/设置 资源文件所在程序集 为空时扫描当前程序集
        /// </summary>
        public Type? ResourceType { get; set; }

        /// <summary>
        /// 获得显示名称方法
        /// </summary>
        /// <returns></returns>
        public string? GetName()
        {
            var ret = Name;
            if (ResourceType != null && !string.IsNullOrEmpty(ResourceName))
            {
                var options = ServiceProviderHelper.ServiceProvider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
                var loggerFactory = ServiceProviderHelper.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var factory = new JsonStringLocalizerFactory(options, loggerFactory);
                var localizer = factory.Create(ResourceType);
                ret = localizer[ResourceName];
            }
            return ret;
        }
    }
}
