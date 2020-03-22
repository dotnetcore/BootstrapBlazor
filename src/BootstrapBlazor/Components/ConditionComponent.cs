using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 条件输出组件
    /// </summary>
    public class ConditionComponent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 是否显示 默认显示组件内容
        /// </summary>
        [Parameter]
        public bool Condition { get; set; } = true;

        /// <summary>
        /// 获得/设置 子控件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Condition) builder.AddContent(0, ChildContent);
        }
    }
}
