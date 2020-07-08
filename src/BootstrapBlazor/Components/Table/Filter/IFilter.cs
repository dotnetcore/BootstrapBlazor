using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITableFitler 接口
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 获得 IFilter 实例中的过滤条件集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<FilterKeyValueAction> GetFilterConditions();
    }
}
