using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表格 Toolbar 按钮组件
    /// </summary>
    public class TableToolbarButton : ButtonBase
    {
        /// <summary>
        /// 获得/设置 按钮点击后回调委托
        /// </summary>
        [Parameter]
        public Delegate? OnClickCallback { get; set; }

        /// <summary>
        /// 获得/设置 Table Toolbar 实例
        /// </summary>
        [CascadingParameter]
        protected ITableToolbar? Toolbar { get; set; }

        /// <summary>
        /// 组件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Toolbar?.AddButtons(this);
        }
    }
}
