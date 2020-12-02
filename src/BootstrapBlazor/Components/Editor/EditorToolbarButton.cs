// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 富文本框插件信息
    /// </summary>
    public class EditorToolbarButton
    {
        /// <summary>
        /// 获取或设置 插件名称
        /// </summary>
        [NotNull]
        public string? ButtonName { get; set; }

        /// <summary>
        /// 获取或设置 插件图标
        /// </summary>
        public string? IconClass { get; set; }

        /// <summary>
        /// 获取或设置 插件的提示信息
        /// </summary>
        public string? Tooltip { get; set; }
    }
}
