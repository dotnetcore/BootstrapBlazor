using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITableFitler 接口
    /// </summary>
    public interface ITableFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<FilterKeyValueAction> GetFilters();
    }
}
