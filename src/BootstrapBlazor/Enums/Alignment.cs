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
    /// 对齐方式枚举类型
    /// </summary>
    public enum Alignment
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// 
        /// </summary>
        [Description("left")]
        Left,

        /// <summary>
        /// 
        /// </summary>
        [Description("center")]
        Center,

        /// <summary>
        /// 
        /// </summary>
        [Description("right")]
        Right
    }
}
