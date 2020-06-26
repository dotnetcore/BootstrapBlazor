using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableBase<TItem>
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
        protected Func<string, SortOrder, Task> OnSortAsync { get; set; } = (sortName, sortOrder) => Task.CompletedTask;
    }
}
