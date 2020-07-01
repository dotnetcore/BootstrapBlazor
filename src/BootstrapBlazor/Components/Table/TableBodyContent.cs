using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TableBodyContent<TItem> : ComponentBase where TItem : class
    {
        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        /// <summary>
        /// 获得/设置 行数据实例
        /// </summary>
        [Parameter]
        public TItem? Item { get; set; }

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 渲染正常按钮
            if (Columns != null)
            {
                var index = 0;
                foreach (var col in Columns.Columns)
                {
                    builder.OpenComponent<TableCell>(index++);
                    builder.AddAttribute(index++, nameof(TableCell.ChildContent), GetValue(col));
                    builder.CloseComponent();
                }
            }
        }

        private RenderFragment GetValue(ITableColumn col) => builder =>
        {
            if (col.Template != null)
            {
                if (Item != null) builder.AddContent(0, col.Template.Invoke(Item));
            }
            else
            {
                builder.AddContent(0, GetItemValue(col.GetFieldName()));
            }
        };

        private string GetItemValue(string filedName) => Item?.GetPropertyValue<TItem, object>(filedName)?.ToString() ?? "";
    }
}
