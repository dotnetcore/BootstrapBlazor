// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">CarouselImage 内部组件</para>
///  <para lang="en">CarouselImage internal component</para>
/// </summary>
internal class CarouselImage : ComponentBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 图片路径</para>
    ///  <para lang="en">Gets or sets the image URL</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 点击回调委托</para>
    ///  <para lang="en">Gets or sets the click callback delegate</para>
    ///  <para><version>10.2.2</version></para>
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
