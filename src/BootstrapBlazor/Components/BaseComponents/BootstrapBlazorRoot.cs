// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class BootstrapBlazorRoot : ComponentBase
    {
        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.OpenComponent<Dialog>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Download>(index++);
            builder.CloseComponent();

            builder.OpenComponent<FullScreen>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Message>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Print>(index++);
            builder.CloseComponent();

            builder.OpenComponent<PopoverConfirm>(index++);
            builder.CloseComponent();

            builder.OpenComponent<SweetAlert>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Title>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Toast>(index++);
            builder.CloseComponent();
        }
    }
}
