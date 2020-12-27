// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ComponentCategory
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Desc { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Count { get; set; }
    }
}
