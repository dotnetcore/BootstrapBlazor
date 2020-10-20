using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Switch
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Switch>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            OnInnerText = Localizer[nameof(OnInnerText)];
            OffInnerText = Localizer[nameof(OffInnerText)];
        }
    }
}
