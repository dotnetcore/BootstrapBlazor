using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MenuLink
    {
        private string? ClassString => CssBuilder.Default()
            .AddClass("active", Active)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Text { get; set; }
    }
}
