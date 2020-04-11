using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 单元格组件
    /// </summary>
    public class TableCell : ComponentBase
    {
        /// <summary>
        /// 获得/设置 RenderFragment 实例
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.OpenElement(index++, "td");
            builder.OpenElement(index++, "div");
            builder.AddAttribute(index++, "class", "table-cell");
            builder.AddContent(index++, ChildContent);
            builder.CloseElement();
            builder.CloseElement();
        }
    }
}
