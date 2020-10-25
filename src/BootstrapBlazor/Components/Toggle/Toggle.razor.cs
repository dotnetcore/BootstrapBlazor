using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Toggle
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Toggle>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            OnText ??= Localizer[nameof(OnText)];
            OffText ??= Localizer[nameof(OffText)];
        }
    }
}
