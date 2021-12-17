// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LinkButton
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Url { get; set; } = "#";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Img { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Placement TooltipPlacement { get; set; } = Placement.Top;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        private bool Prevent => Url.StartsWith('#');
    }
}
