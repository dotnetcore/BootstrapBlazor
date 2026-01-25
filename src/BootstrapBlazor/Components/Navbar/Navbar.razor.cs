// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Navbar 组件</para>
/// <para lang="en">Navbar component</para>
/// </summary>
public partial class Navbar
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件大小，默认为 <see cref="Size.Medium"/></para>
    /// <para lang="en">Gets or sets the component size. Default is <see cref="Size.Medium"/></para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Medium;

    /// <summary>
    /// <para lang="zh">获得/设置 背景色样式名称，默认为 null 未设置</para>
    /// <para lang="en">Gets or sets the background color CSS class name. Default is null</para>
    /// </summary>
    [Parameter]
    public string? BackgroundColorCssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板</para>
    /// <para lang="en">Gets or sets the child component template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("navbar")
        .AddClass($"navbar-expand", Size == Size.None)
        .AddClass($"navbar-expand-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass(BackgroundColorCssClass)
        .AddClass("bg-body-tertiary", string.IsNullOrEmpty(BackgroundColorCssClass))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}

