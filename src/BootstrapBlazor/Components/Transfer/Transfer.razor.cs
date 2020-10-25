using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Transfer
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private IStringLocalizer<Transfer>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            LeftPanelText ??= Localizer[nameof(LeftPanelText)];
            RightPanelText ??= Localizer[nameof(RightPanelText)];
        }
    }
}
