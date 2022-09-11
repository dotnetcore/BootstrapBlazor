// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal class CarouselImage : ComponentBase
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnClick { get; set; }

    private async Task OnClickImage()
    {
        if (OnClick != null)
        {
            await OnClick(ImageUrl ?? "");
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "img");
        if (!string.IsNullOrEmpty(ImageUrl))
        {
            builder.AddAttribute(1, "src", ImageUrl);
        }
        if (OnClick != null)
        {
            builder.AddAttribute(2, "onclick", OnClickImage);
        }
        builder.CloseElement();
    }
}
