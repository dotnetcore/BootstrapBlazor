using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// QRCode 组件
    /// </summary>
    public sealed partial class QRCode
    {
        private ElementReference QRCodeElement { get; set; }

        private JSInterop<QRCode>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 二维码生成后回调委托
        /// </summary>
        [Parameter]
        public Func<Task>? OnGenerated { get; set; }

        private async Task Clear()
        {
            await JSRuntime.InvokeVoidAsync(QRCodeElement, "bb_qrcode", "clear");
        }

        private async Task Generate()
        {
            if (Interop == null) Interop = new JSInterop<QRCode>(JSRuntime);
            await Interop.Invoke(this, QRCodeElement, "bb_qrcode", "generate");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Generated()
        {
            if (OnGenerated != null) await OnGenerated.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Interop?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
