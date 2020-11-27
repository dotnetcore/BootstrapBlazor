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
    /// 图表类型枚举
    /// </summary>
    public enum ChartType
    {
        /// <summary>
        /// Line 图
        /// </summary>
        [Description("line")]
        Line = 0,

        /// <summary>
        /// Bar 图
        /// </summary>
        [Description("bar")]
        Bar,

        /// <summary>
        /// Pie 图
        /// </summary>
        [Description("pie")]
        Pie,

        /// <summary>
        /// Pie 图
        /// </summary>
        [Description("doughnut")]
        Doughnut,

        /// <summary>
        /// Bubble 图
        /// </summary>
        [Description("bubble")]
        Bubble
    }
}