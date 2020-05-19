using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class SideMenu
    {
        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        public new IEnumerable<MenuItem> Items { get; set; } = new MenuItem[0];
    }
}
