using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Column 表头列组件
    /// </summary>
    public class TableHeaderContent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 升序图标
        /// </summary>
        [Parameter]
        public string? SortIconAsc { get; set; }

        /// <summary>
        /// 获得/设置 降序图标
        /// </summary>
        [Parameter]
        public string? SortIconDesc { get; set; }

        /// <summary>
        /// 获得/设置 默认图标
        /// </summary>
        [Parameter]
        public string? SortIcon { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        private string sortName = "";
        private SortOrder sortOrder;

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Columns?.RegisterFilterChangedNotify(StateHasChanged);
        }

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
                    builder.AddAttribute(index++, "class", CssBuilder.Default()
                        .AddClass("sortable", header.Sortable)
                        .AddClass("filterable", header.Filterable)
                        .Build());

                    // 如果允许排序
                    if (header.Sortable)
                    {
                        builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, async () =>
                        {
                            if (sortOrder == SortOrder.Unset) sortOrder = SortOrder.Asc;
                            else if (sortOrder == SortOrder.Asc) sortOrder = SortOrder.Desc;
                            else if (sortOrder == SortOrder.Desc) sortOrder = SortOrder.Unset;
                            sortName = fieldName;

                            // 通知 Table 组件刷新数据
                            if (Columns.OnSortAsync != null) await Columns.OnSortAsync.Invoke(sortName, sortOrder);
                        }));
                    }
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", CssBuilder.Default("table-cell")
                        .AddClass("is-sort", header.Sortable)
                        .AddClass("is-filter", header.Filterable)
                        .Build());

                    builder.OpenElement(index++, "span");
                    builder.AddAttribute(index++, "class", "table-text");
                    builder.AddContent(index++, displayName);
                    builder.CloseElement(); // span

                    if (header.Filterable)
                    {
                        builder.OpenElement(index++, "i");
                        builder.AddAttribute(index++, "class", CssBuilder.Default("fa fa-fw fa-filter")
                            .AddClass("active", Columns.HasFilter(fieldName))
                            .Build());
                        builder.AddAttribute(index++, "data-field", fieldName);
                        builder.AddEventStopPropagationAttribute(index++, "onclick", true);
                        builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                        {
                            // 点击 Filter 小图标事件
                            Columns.ShowTableFilter(fieldName);
                        }));
                        builder.CloseElement(); // end i
                    }

                    if (header.Sortable)
                    {
                        builder.OpenElement(index++, "i");
                        var order = sortName == fieldName ? sortOrder : SortOrder.Unset;
                        var icon = order switch
                        {
                            SortOrder.Asc => SortIconAsc,
                            SortOrder.Desc => SortIconDesc,
                            _ => SortIcon
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
