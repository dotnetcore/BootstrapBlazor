using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITable 接口
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// 获得 ITableColumn 集合
        /// </summary>
        List<ITableColumn> Columns { get; }

        /// <summary>
        /// 获得 过滤条件集合
        /// </summary>
        Dictionary<string, IFilterAction> Filters { get; }

        /// <summary>
        /// 获得 过滤异步回调方法
        /// </summary>
        Func<Task>? OnFilterAsync { get; }
    }
}
