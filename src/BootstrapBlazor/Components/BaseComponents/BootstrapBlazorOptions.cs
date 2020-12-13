// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Delay 配置类
    /// </summary>
    public class BootstrapBlazorOptions
    {
        /// <summary>
        /// 获得/设置 Toast 组件 Delay 默认值 默认为 0
        /// </summary>
        public int ToastDelay { get; set; }

        /// <summary>
        /// 获得/设置 Message 组件 Delay 默认值 默认为 0
        /// </summary>
        public int MessageDelay { get; set; }

        /// <summary>
        /// 获得/设置 Swal 组件 Delay 默认值 默认为 0
        /// </summary>
        public int SwalDelay { get; set; }

        /// <summary>
        /// 获得/设置 默认 UI 文化信息 默认为 null 未设置采用系统设置
        /// </summary>
        public string? DefaultUICultureInfoName { get; set; }

        /// <summary>
        /// 获得 组件内置本地化语言列表
        /// </summary>
        public IEnumerable<string> SupportedCultures { get; } = new string[] { "zh-CN", "en-US" };
    }
}
