using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表格 Toolbar 按钮组件
    /// </summary>
    public class TableToolbarButton : ButtonBase
    {
        /// <summary>
        /// 获得/设置 显示标题 默认为 删除
        /// </summary>
        [Parameter] public string ButtonText { get; set; } = "删除";

        /// <summary>
        /// 获得/设置 按钮图标 默认为 fa-remove
        /// </summary>
        [Parameter] public string ButtonIcon { get; set; } = "fa fa-remove";

        /// <summary>
        /// 获得/设置 Table Toolbar 实例
        /// </summary>
        [CascadingParameter]
        protected TableToolbar? Toolbar { get; set; }

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
