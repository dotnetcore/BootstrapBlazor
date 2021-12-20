using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SwitchButton
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? OnText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? OffText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool ToggleState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<bool> ToggleStateChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        private async Task Toggle()
        {
            ToggleState = !ToggleState;
            if (ToggleStateChanged.HasDelegate)
            {
                await ToggleStateChanged.InvokeAsync(ToggleState);
            }
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }

        private string? GetText() => ToggleState ? OnText : OffText;
    }
}
