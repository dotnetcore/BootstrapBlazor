// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DockView
    {
        [NotNull]
        private IJSObjectReference? DockViewModule { get; set; }

        [NotNull]
        private ElementReference? Element { get; set; }

        [Inject]
        [NotNull]
        private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        [NotNull]
        public DockViewConfig? Config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // import JavaScript
                DockViewModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.Dock/Components/DockView/DockView.razor.js");
                await DockViewModule.InvokeVoidAsync("init", Element, Config);
            }
        }
    }
}
