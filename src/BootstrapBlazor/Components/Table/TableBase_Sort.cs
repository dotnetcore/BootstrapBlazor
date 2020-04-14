using System;

namespace BootstrapBlazor.Components
{
    partial class TableBase<TItem>
    {
        /// <summary>
        /// 获得/设置 排序字段名称
        /// </summary>
        public string? SortName { get; set; }

        /// <summary>
        /// 获得/设置 排序方式
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// 获得/设置 表头排序时回调方法
        /// </summary>
        protected Action<string, SortOrder> OnSort { get; set; } = new Action<string, SortOrder>((sortName, sortOrder) =>
        {

        });
    }
}
