// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class App
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && OperatingSystem.IsBrowser() && JSRuntime != null)
            {
                await JSRuntime.InvokeVoidAsync("$.loading");
            }
        }
    }
}
