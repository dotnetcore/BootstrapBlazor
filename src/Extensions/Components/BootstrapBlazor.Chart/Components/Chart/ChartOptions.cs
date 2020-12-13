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
    /// Chart 图表组件配置项实体类
    /// </summary>
    public class ChartOptions
    {
        /// <summary>
        /// 获得/设置 ChartTitle 实例
        /// </summary>
        public ChartTitle Title { get; } = new ChartTitle();

        /// <summary>
        /// 获得 X 坐标轴实例集合
        /// </summary>
        public List<ChartAxes> XAxes { get; } = new List<ChartAxes>();

        /// <summary>
        /// 获得/设置 是否显示 X 坐标轴刻度线 默认为 true
        /// </summary>
        public bool ShowXAxesLine { get; set; } = true;

        /// <summary>
        /// 获得 X 坐标轴实例集合
        /// </summary>

        public List<ChartAxes> YAxes { get; } = new List<ChartAxes>();

        /// <summary>
        /// 获得/设置 是否显示 Y 坐标轴刻度线 默认为 true
        /// </summary>
        public bool ShowYAxesLine { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否 适配移动端 默认为 true
        /// </summary>
        public bool Responsive { get; set; } = true;
    }
}
