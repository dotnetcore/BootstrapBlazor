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
    /// 
    /// </summary>
    public abstract class TableDialog<TModel> : ComponentBase
    {
#nullable disable
        /// <summary>
        /// 获得/设置 EditModel 实例
        /// </summary>
        [Parameter]
        public TModel Model { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 BodyTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TModel>? BodyTemplate { get; set; }

        /// <summary>
        /// 获得 表头集合
        /// </summary>
        [Parameter]
        public IEnumerable<ITableColumn>? Columns { get; set; }

        /// <summary>
        /// 获得/设置 是否显示标签
        /// </summary>
        [Parameter]
        public bool ShowLabel { get; set; }
    }
}
