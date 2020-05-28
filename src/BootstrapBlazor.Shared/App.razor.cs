using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class App
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender && JSRuntime != null)
            {
                JSRuntime.InvokeVoidAsync("$.loading");
            }
        }
    }
}
