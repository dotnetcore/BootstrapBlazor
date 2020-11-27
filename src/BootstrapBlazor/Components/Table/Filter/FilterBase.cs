// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 类型过滤器基类
    /// /// </summary>
    public abstract class FilterBase : ComponentBase, IFilterAction
    {
        /// <summary>
        /// 
        /// </summary>
        protected virtual FilterLogic Logic { get; set; }

        /// <summary>
        /// 获得/设置 相关 Field 字段名称
        /// </summary>
        protected string? FieldKey { get; set; }

        /// <summary>
        /// 获得/设置 条件数量
        /// </summary>
        [Parameter]
        public int Count { get; set; }

        /// <summary>
        /// 获得/设置 所属 TableFilter 实例
        /// </summary>
        [CascadingParameter]
        protected TableFilter? TableFilter { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (TableFilter != null)
            {
                TableFilter.FilterAction = this;
                FieldKey = TableFilter.FieldKey;
            }
        }

        /// <summary>
        /// 重置过滤条件方法
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// 获得过滤窗口的所有条件方法
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<FilterKeyValueAction> GetFilterConditions();
    }
}
