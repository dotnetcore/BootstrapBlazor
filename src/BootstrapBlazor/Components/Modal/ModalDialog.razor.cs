// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
                await Interop.Invoke(this, DialogElement, "bb_modal_dialog", nameof(Close));
            }
        }

        private async Task OnClickClose()
        {
            if (OnClose != null)
            {
                Modal?.RemoveDialog(this);
                await OnClose();
            }
        }

        /// <summary>
        /// Close 方法 客户端按 ESC 键盘时调用
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public Task Close() => OnClickClose();

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
