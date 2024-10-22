// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
