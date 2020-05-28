using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class TableBase<TItem>
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
        protected Func<string, SortOrder, Task> OnSortAsync { get; set; } = new Func<string, SortOrder, Task>((sortName, sortOrder) => Task.CompletedTask);
    }
}
