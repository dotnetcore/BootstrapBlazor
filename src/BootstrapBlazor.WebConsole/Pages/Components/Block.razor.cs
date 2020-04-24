using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.WebConsole.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Block
    {
        /// <summary>
        /// 
        /// </summary>
        private ElementReference CodeElement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public string Title { get; set; } = "未设置";

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public string Introduction { get; set; } = "未设置";

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public string? CodeFile { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject] private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            JSRuntime.InvokeVoidAsync("$.highlight", CodeElement);
        }
    }
}
