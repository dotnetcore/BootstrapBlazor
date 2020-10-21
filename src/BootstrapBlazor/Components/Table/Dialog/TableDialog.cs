using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TableDialog<TModel> : ComponentBase
    {
        /// <summary>
        /// 获得/设置 EditModel 实例
        /// </summary>
        [Parameter]
        public TModel? Model { get; set; }

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
