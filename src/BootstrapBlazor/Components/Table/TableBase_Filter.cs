using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableBase<TItem>
    {
        /// <summary>
        /// 获得/设置 过滤条件集合
        /// </summary>
        protected List<ITableFilter> Filters { get; } = new List<ITableFilter>(10);

        /// <summary>
        /// 获得/设置 表头过滤时回调方法
        /// </summary>
        protected Func<IEnumerable<ITableFilter>, Task> OnFilterAsync { get; set; } = _ => Task.CompletedTask;
    }
}
