using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    public sealed partial class Select<TValue>
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Select<TValue>>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlaceHolder = Localizer[nameof(PlaceHolder)];
        }
    }
}
