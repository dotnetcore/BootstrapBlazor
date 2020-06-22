using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class CarouselImage : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnClick { get; set; }

        private async Task OnClickImage()
        {
            if (OnClick != null) await OnClick(ImageUrl ?? "");
        }
    }
}
