using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using BootstrapBlazor.Shared.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class LayoutPages
    {
        private bool IsFixedHeader { get; set; }

        private bool IsFixedFooter { get; set; }

        private bool IsFullSide { get; set; }

        [CascadingParameter]
        private PageLayout? RootPage { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (RootPage != null)
            {
                IsFullSide = RootPage.IsFullSide;
                IsFixedHeader = RootPage.IsFixedHeader;
                IsFixedFooter = RootPage.IsFixedFooter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnLayoutChanged(bool fullside, bool fixedHeader, bool fixedFooter)
        {
            if (RootPage != null)
            {
                await RootPage.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
                {
                    [nameof(RootPage.IsFullSide)] = fullside,
                    [nameof(RootPage.IsFixedFooter)] = fixedFooter,
                    [nameof(RootPage.IsFixedHeader)] = fixedHeader,
                }));

                // 获得 Razor 示例代码
                RootPage.Update();
            }
        }
    }
}
