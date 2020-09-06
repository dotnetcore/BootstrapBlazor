using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 弹窗类组件基类
    /// </summary>
    public abstract class PopupComponentBase : BootstrapComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected ElementReference PopupElement { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender) await JSRuntime.InvokeVoidAsync(PopupElement, "bb_pop", "init");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _ = JSRuntime.InvokeVoidAsync(PopupElement, "bb_pop", "dispose");
            }
        }
    }
}
