// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 按钮类型枚举
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 正常按钮
        /// </summary>
        [Description("button")]
        Button,

        /// <summary>
        /// 提交按钮
        /// </summary>
        [Description("submit")]
        Submit,

        /// <summary>
        /// 重置按钮
        /// </summary>
        [Description("reset")]
        Reset
    }
}
