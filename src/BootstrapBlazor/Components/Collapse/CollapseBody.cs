using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// CollapseBody 组件
    /// </summary>
    public class CollapseBody : IdComponentBase
    {
        /// <summary>
        /// 获得 折叠展示样式集合
        /// </summary>
        protected string? CollapseClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected bool IsCollapsing { get; set; }

        /// <summary>
        /// 是否展开折叠面板
        /// </summary>
        [Parameter] public bool IsCollapsed { get; set; } = true;

        /// <summary>
        /// 获得/设置 子组件实例
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            CollapseClass = CssBuilder.Default("collapse")
                .AddClass("collapsing", IsCollapsing)
                .AddClass("show", !IsCollapsed)
                .Build();
        }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.OpenElement(index++, "div");
            builder.AddAttribute(index++, "class", CollapseClass);
            builder.AddAttribute(index++, "id", Id);

            builder.OpenElement(index++, "div");
            builder.AddAttribute(index++, "class", "card card-body");
            builder.AddContent(index++, ChildContent);
            builder.CloseElement();

            builder.CloseElement();
        }

        /// <summary>
        /// 动画支持
        /// </summary>
        /// <returns></returns>
        public void DoAnimations(bool collapsed)
        {
            JSRuntime.InvokeRun(Id, "collapse", collapsed ? "hide" : "show");
        }
    }
}
