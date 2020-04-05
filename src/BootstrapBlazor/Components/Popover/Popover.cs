using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Popover 弹出窗组件
    /// </summary>
    public class Popover : Tooltip
    {
        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        public override PopoverType PopoverType { get; set; } = PopoverType.Popover;

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        [Parameter] public override string Title { get; set; } = "Popover";

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter] public override string Content { get; set; } = "Popover";
    }
}
