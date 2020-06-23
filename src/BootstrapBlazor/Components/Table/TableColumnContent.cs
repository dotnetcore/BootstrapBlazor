using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TableColumnContent 组件
    /// </summary>
    public class TableColumnContent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        /// <summary>
        /// RenderTreeBuilder 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Columns != null)
            {
                var index = 0;
                foreach (var col in Columns.Columns)
                {
                    builder.OpenElement(index++, "col");
                    if (col.Width > 0) builder.AddAttribute(index++, "width", col.Width);
                    builder.CloseElement();
                }
            }
        }
    }
}
