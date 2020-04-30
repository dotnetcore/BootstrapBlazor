using Microsoft.AspNetCore.Components;

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
    }
}
