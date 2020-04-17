using BootstrapBlazor.WebConsole.Pages.Components;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    partial class PopoverConfirms
    {
        /// <summary>
        /// 
        /// </summary>
        protected Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected void OnClose()
        {
            // 点击确认按钮后此方法被回调，点击取消按钮时此方法不会被调用
            Trace?.Log("OnClose Trigger");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnConfirm()
        {
            // 点击确认按钮后此方法被回调，点击取消按钮时此方法不会被调用
            Trace?.Log("OnConfirm Trigger");
        }
    }
}
