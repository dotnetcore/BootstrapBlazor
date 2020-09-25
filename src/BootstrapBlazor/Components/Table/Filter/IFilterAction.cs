using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITableFitler 接口
    /// </summary>
    public interface IFilterAction
    {
        /// <summary>
        /// 获得 IFilter 实例中的过滤条件集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<FilterKeyValueAction> GetFilterConditions();

        /// <summary>
        /// 重置过滤条件方法
        /// </summary>
        void Reset();
    }
}
