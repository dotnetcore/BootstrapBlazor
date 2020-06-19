using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ComponentCategory
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Desc { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Count { get; set; }
    }
}
