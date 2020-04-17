using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Toolbar 组件
    /// </summary>
    public partial class TableToolbar : ComponentBase
    {
        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 添加按钮到工具栏方法
        /// </summary>
        public void AddButtons(ButtonBase button) => Buttons.Add(button);

        /// <summary>
        /// 获得 Toolbar 按钮集合
        /// </summary>
        public ICollection<ButtonBase> Buttons { get; } = new HashSet<ButtonBase>();
    }
}
