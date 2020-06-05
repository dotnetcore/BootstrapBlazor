using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Split 组件基类
    /// </summary>
    public abstract class SplitBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件 DOM 实例
        /// </summary>
        protected ElementReference SplitElement { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("split")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 组件 Wrapper 样式
        /// </summary>
        protected string? WrapperClassString => CssBuilder.Default("split-wrapper")
            .AddClass("is-horizontal", !IsVertical)
            .Build();

        /// <summary>
        /// 获得 第一个窗格 Style
        /// </summary>
        protected string? StyleString => CssBuilder.Default()
            .AddClass($"flex-basis: {Basis.ConvertToPercentString()};")
            .Build();

        /// <summary>
        /// 获得/设置 是否垂直分割
        /// </summary>
        [Parameter]
        public bool IsVertical { get; set; }

        /// <summary>
        /// 获得/设置 第一个窗格初始化位置占比 默认为 50%
        /// </summary>
        [Parameter]
        public string Basis { get; set; } = "50%";

        /// <summary>
        /// 获得/设置 第一个窗格模板
        /// </summary>
        [Parameter]
        public RenderFragment? FirstPaneTemplate { get; set; }

        /// <summary>
        /// 获得/设置 第二个窗格模板
        /// </summary>
        [Parameter]
        public RenderFragment? SecondPaneTemplate { get; set; }

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
                await JSRuntime.Invoke(SplitElement, "split");
            }
        }
    }
}
