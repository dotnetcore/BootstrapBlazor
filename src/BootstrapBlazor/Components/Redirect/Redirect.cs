// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class Redirect : ComponentBase
    {
        [Inject]
        [NotNull]
        private NavigationManager? Navigation { get; set; }

        [Parameter]
        public string Url { get; set; } = "Account/Login";

#if DEBUG
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            Navigation.NavigateTo(Url, true);
        }
#else
        protected override void OnInitialized()
        {
            Navigation.NavigateTo(Url, true);
        }
#endif
    }
}
