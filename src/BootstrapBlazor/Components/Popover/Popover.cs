namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Popover 弹出窗组件
    /// </summary>
    public class Popover : Tooltip
    {
        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PopoverType = PopoverType.Popover;
        }
    }
}
