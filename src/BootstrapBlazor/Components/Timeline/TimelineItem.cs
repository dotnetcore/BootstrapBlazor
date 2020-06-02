using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 时间线选项
    /// </summary>
    public class TimelineItem
    {
        /// <summary>
        /// 获得/设置 时间线内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 时间线时间
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 获得/设置 时间线颜色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// 获得/设置 时间线图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 自定义组件
        /// </summary>
        public DynamicComponent? Component { get; set; }

        /// <summary>
        /// 获得 时间线节点样式
        /// </summary>
        internal string? ToNodeClassString() => CssBuilder.Default("timeline-item-node-normal timeline-item-node")
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None && string.IsNullOrEmpty(Icon))
            .AddClass("is-icon", !string.IsNullOrEmpty(Icon))
            .Build();

        /// <summary>
        /// 获得 图标样式
        /// </summary>
        /// <returns></returns>
        internal string? ToIconClassString() => CssBuilder.Default("timeline-item-icon")
            .AddClass(Icon, !string.IsNullOrEmpty(Icon))
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();
    }
}
