using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Scroll 组件基类
    /// </summary>
    public abstract class ScrollBase : BootstrapComponentBase
    {
        /// <summary>
        /// Scroll 组件 DOM 实例
        /// </summary>
        protected ElementReference ScrollElement { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("scroll")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 组件 Style
        /// </summary>
        protected string? StyleString => CssBuilder.Default()
            .AddClass($"height: {Height}px", Height > 0)
            .AddClass($"width: {Width}px", Width > 0)
            .AddStyleFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 是否自动隐藏
        /// </summary>
        protected string? AutoHideString => IsAutoHide ? "true" : "false";

        /// <summary>
        /// 获得 滚动条样式
        /// </summary>
        protected string? IsDarkString => IsDark ? "true" : "false";

        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 组件高度
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 获得/设置 组件宽度
        /// </summary>
        [Parameter]
        public int Width { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        [Parameter]
        public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏延时时间 默认 1000 毫秒
        /// </summary>
        [Parameter]
        public int Delay { get; set; } = 1000;

        /// <summary>
        /// 获得/设置 是否为暗黑模式
        /// </summary>
        [Parameter]
        public bool IsDark { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                await JSRuntime.Invoke(ScrollElement, "scroll");
            }
        }
    }
}
