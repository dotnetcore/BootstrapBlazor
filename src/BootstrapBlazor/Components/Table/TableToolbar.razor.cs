using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Toolbar 组件
    /// </summary>
    public partial class TableToolbar<TItem> : ComponentBase
    {
        /// <summary>
        /// 获得 Toolbar 按钮集合
        /// </summary>
        private List<IToolbarButton<TItem>> Buttons { get; } = new List<IToolbarButton<TItem>>();

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

        private async Task OnToolbarButtonClick(TableToolbarButton<TItem> button)
        {
            if (!button.IsDisabled)
            {
                if (button.OnClick != null) await button.OnClick.Invoke();

                // 传递当前选中行给回调委托方法
                if (button.OnClickCallback != null)
                {
                    await button.OnClickCallback.Invoke(OnGetSelectedRows());
                }
            }
        }

        /// <summary>
        /// 添加按钮到工具栏方法
        /// </summary>
        public void AddButton(IToolbarButton<TItem> button) => Buttons.Add(button);
    }
}
