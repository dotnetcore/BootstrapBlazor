using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class PopConfirmButton
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
            CloseButtonText ??= Localizer[nameof(CloseButtonText)];
            Content ??= Localizer[nameof(Content)];
        }
    }
}
