using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Column 表头列组件
    /// </summary>
    public class TableHeaderContent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        /// <summary>
        /// 获得/设置 升序图标
        /// </summary>
        [Parameter]
        public string SortIconAsc { get; set; } = "fa fa-sort-asc";

        /// <summary>
        /// 获得/设置 降序图标
        /// </summary>
        [Parameter]
        public string SortIconDesc { get; set; } = "fa fa-sort-desc";

        /// <summary>
        /// 获得/设置 默认图标
        /// </summary>
        [Parameter]
        public string SortDefault { get; set; } = "fa fa-sort";

        private string sortName = "";
        private SortOrder sortOrder;

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 渲染正常按钮
            if (Columns != null)
            {
                var index = 0;
                foreach (var header in Columns.Columns)
                {
                    var fieldName = header.GetFieldName();
                    var displayName = header.GetDisplayName();
                    builder.OpenElement(index++, "th");

                    // 移除 bind-Field
                    header.AdditionalAttributes?.Remove("FieldChanged");
                    builder.AddMultipleAttributes(index++, header.AdditionalAttributes);

                    // 如果允许排序
                    if (header.Sort)
                    {
                        builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, async () =>
                        {
                            if (sortName != fieldName) sortOrder = SortOrder.Asc;
                            else sortOrder = sortOrder == SortOrder.Asc ? SortOrder.Desc : SortOrder.Asc;
                            sortName = fieldName;

                            // 通知 Table 组件刷新数据
                            if (Columns.OnSortAsync != null) await Columns.OnSortAsync.Invoke(sortName, sortOrder);
                        }));
                        builder.AddAttribute(index++, "class", "sortable");
                    }
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", CssBuilder.Default("table-cell").AddClass("is-sort", header.Sort).Build());

                    builder.OpenElement(index++, "span");
                    builder.AddContent(index++, displayName);
                    builder.CloseElement(); // span

                    if (header.Sort)
                    {
                        builder.OpenElement(index++, "i");
                        var order = sortName == fieldName ? sortOrder : SortOrder.Unset;
                        var icon = order switch
                        {
                            SortOrder.Asc => SortIconAsc,
                            SortOrder.Desc => SortIconDesc,
                            _ => SortDefault
                        };
                        builder.AddAttribute(index++, "class", icon);
                        builder.CloseElement(); // end i
                    }
                    builder.CloseElement(); // end div
                    builder.CloseElement(); // end th
                }
            }
        }
    }
}
