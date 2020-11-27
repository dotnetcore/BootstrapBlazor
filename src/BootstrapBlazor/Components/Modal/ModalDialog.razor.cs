// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ModalDialog
    {
        private ElementReference DialogElement { get; set; }

        private JSInterop<ModalDialog>? Interop { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Interop = new JSInterop<ModalDialog>(JSRuntime);
                await Interop.Invoke(this, DialogElement, "bb_dialog", "init");
            }
        }

        /// <summary>
        /// Close 方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Close()
        {
            if (OnClose != null) await OnClose.Invoke();
        }

        /// <summary>
        /// Dispose 方法
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
