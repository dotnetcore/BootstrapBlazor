// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
