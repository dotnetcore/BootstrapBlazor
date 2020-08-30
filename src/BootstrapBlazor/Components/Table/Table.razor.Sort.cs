using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    partial class Table<TItem>
    {
        /// <summary>
        /// 获得/设置 排序字段名称
        /// </summary>
        protected string? SortName { get; set; }

        /// <summary>
        /// 获得/设置 排序方式
        /// </summary>
        protected SortOrder SortOrder { get; set; }

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
        public string SortIcon { get; set; } = "fa fa-sort";

        /// <summary>
        /// 获得/设置 表头排序时回调方法
        /// </summary>
        protected Func<Task> OnSortAsync { get; set; } = () => Task.CompletedTask;

        /// <summary>
        /// 点击列进行排序方法
        /// </summary>
        protected async Task OnClickHeader(ITableColumn col)
        {
            if (col.Sortable)
            {
                if (SortOrder == SortOrder.Unset) SortOrder = SortOrder.Asc;
                else if (SortOrder == SortOrder.Asc) SortOrder = SortOrder.Desc;
                else if (SortOrder == SortOrder.Desc) SortOrder = SortOrder.Unset;
                SortName = col.GetFieldName();

                // 通知 Table 组件刷新数据
                if (OnSortAsync != null) await OnSortAsync.Invoke();
            }
        }

        /// <summary>
        /// 获取指定列头样式字符串
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected string? GetHeaderClassString(ITableColumn col) => CssBuilder.Default()
            .AddClass("sortable", col.Sortable)
            .AddClass("filterable", col.Filterable)
            .Build();

        /// <summary>
        /// 获取指定列头样式字符串
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected string? GetHeaderWrapperClassString(ITableColumn col) => CssBuilder.Default("table-cell")
            .AddClass("is-sort", col.Sortable)
            .AddClass("is-filter", col.Filterable)
            .Build();

        /// <summary>
        /// 获得 Header 中表头文字样式
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected string? GetHeaderTextClassString(ITableColumn col) => CssBuilder.Default("table-text")
            .AddClass("text-left", col.Align == Alignment.Left)
            .AddClass("text-right", col.Align == Alignment.Right)
            .AddClass("text-center", col.Align == Alignment.Center)
            .Build();

        /// <summary>
        /// 获得 Cell 文字样式
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected string? GetCellClassString(ITableColumn col) => CssBuilder.Default("table-cell")
             .AddClass("justify-content-start", col.Align == Alignment.Left)
             .AddClass("justify-content-end", col.Align == Alignment.Right)
             .AddClass("justify-content-center", col.Align == Alignment.Center)
             .Build();

        /// <summary>
        /// 获取指定列头样式字符串
        /// </summary>
        /// <returns></returns>
        protected string? GetIconClassString(string fieldName)
        {
            var order = SortName == fieldName ? SortOrder : SortOrder.Unset;
            return order switch
            {
                SortOrder.Asc => SortIconAsc,
                SortOrder.Desc => SortIconDesc,
                _ => SortIcon
            };
        }
    }
}
