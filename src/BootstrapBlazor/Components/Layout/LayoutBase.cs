using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Layout 组件基类
    /// </summary>
    public abstract class LayoutBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("layout")
            .AddClass("has-sidebar", Side != null && IsFullSide)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 侧边栏 Style 字符串
        /// </summary>
        protected string? SideStyleString => CssBuilder.Default()
            .AddClass($"width: {SideWidth.ConvertToPercentString()}", !string.IsNullOrEmpty(SideWidth))
            .Build();

        /// <summary>
        /// 获得/设置 Header 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Header { get; set; }

        /// <summary>
        /// 获得/设置 Footer 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Footer { get; set; }

        /// <summary>
        /// 获得/设置 Side 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Side { get; set; }

        /// <summary>
        /// 获得/设置 Footer 高度 支持百分比 默认宽度为 300px
        /// </summary>
        [Parameter]
        public string SideWidth { get; set; } = "300";

        /// <summary>
        /// 获得/设置 Main 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Main { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏是否占满整个左侧 默认为 false
        /// </summary>
        [Parameter]
        public bool IsFullSide { get; set; }
    }
}
