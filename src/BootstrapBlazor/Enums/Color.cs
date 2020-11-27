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
    /// 颜色枚举类型
    /// </summary>
    public enum Color
    {
        /// <summary>
        /// 无颜色
        /// </summary>
        None,

        /// <summary>
        /// active
        /// </summary>
        [Description("active")]
        Active,

        /// <summary>
        /// primary
        /// </summary>
        [Description("primary")]
        Primary,

        /// <summary>
        /// secondary
        /// </summary>
        [Description("secondary")]
        Secondary,

        /// <summary>
        /// success
        /// </summary>
        [Description("success")]
        Success,

        /// <summary>
        /// danger
        /// </summary>
        [Description("danger")]
        Danger,

        /// <summary>
        /// warning
        /// </summary>
        [Description("warning")]
        Warning,

        /// <summary>
        /// info
        /// </summary>
        [Description("info")]
        Info,

        /// <summary>
        /// light
        /// </summary>
        [Description("light")]
        Light,

        /// <summary>
        /// dark
        /// </summary>
        [Description("dark")]
        Dark,

        /// <summary>
        /// link
        /// </summary>
        [Description("link")]
        Link
    }
}
