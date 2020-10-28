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