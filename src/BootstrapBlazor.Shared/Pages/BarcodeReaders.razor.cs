using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class BarcodeReaders
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 显示扫码界面
        /// </summary>
        bool ShowScanBarcode { get; set; } = false;

        /// <summary>
        /// 条码
        /// </summary>
        public string? BarCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void DismissClick(MouseEventArgs e)
        {
            Trace?.Log($"Alert Dismissed");
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "ScanResult",
                Description="扫码结果回调方法",
                Type ="EventCallback<string>"
            },
            new EventItem()
            {
                Name = "Close",
                Description="关闭扫码框回调方法",
                Type ="EventCallback"
            }
        };


    }
}
