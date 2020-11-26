using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class State
    {
        /// <summary>
        /// 获得/设置 是否为新组件 默认为 false 为 New
        /// </summary>
        [Parameter]
        public bool IsNew { get; set; }

        /// <summary>
        /// 获得/设置 组件数量
        /// </summary>
        [Parameter]
        public int Count { get; set; }
    }
}
