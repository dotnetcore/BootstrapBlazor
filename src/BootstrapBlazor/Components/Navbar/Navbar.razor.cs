// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Navbar 组件
/// </summary>
public partial class Navbar
{
    /// <summary>
    /// 获得/设置 组件大小 默认 <see cref="Size.Medium"/>
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Medium;

    /// <summary>
    /// 获得/设置 背景色样式名称 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? BackgroundColorCssClass { get; set; }

    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("navbar")
        .AddClass($"navbar-expand", Size == Size.None)
        .AddClass($"navbar-expand-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass(BackgroundColorCssClass)
        .AddClass("bg-body-tertiary", string.IsNullOrEmpty(BackgroundColorCssClass))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}

