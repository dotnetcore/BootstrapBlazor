using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BarcodeReader 条码扫描
    /// </summary>
    public abstract class BarcodeReaderBase : BootstrapComponentBase
    {

        /// <summary>
        /// 扫码结果回调方法
        /// </summary>
        [Parameter]
        public EventCallback<string> ScanResult { get; set; }

        /// <summary>
        /// 关闭扫码框回调方法
        /// </summary>
        [Parameter]
        public EventCallback Close { get; set; }


        /// <summary>
        /// 扫码结果
        /// </summary>
        [Parameter]
        public string? Result { get; set; }

    }
}
