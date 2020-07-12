using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Toolbar 组件
    /// </summary>
    public partial class TableToolbar<TItem> : ComponentBase, ITableToolbar
    {
        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 按钮点击后回调委托
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TItem>> OnGetSelectedRows { get; set; } = () => Enumerable.Empty<TItem>();

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
