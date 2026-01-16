// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Navbar 组件
///</para>
/// <para lang="en">Navbar component
///</para>
/// </summary>
public partial class Navbar
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件大小 默认 <see cref="Size.Medium"/>
    ///</para>
    /// <para lang="en">Gets or sets component大小 Default is <see cref="Size.Medium"/>
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Medium;

    /// <summary>
    /// <para lang="zh">获得/设置 背景色样式名称 默认 null 未设置
    ///</para>
    /// <para lang="en">Gets or sets 背景色style名称 Default is null 未Sets
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? BackgroundColorCssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板
    ///</para>
    /// <para lang="en">Gets or sets 子componenttemplate
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得 组件样式
    ///</para>
    /// <para lang="en">Gets componentstyle
    ///</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("navbar")
        .AddClass($"navbar-expand", Size == Size.None)
        .AddClass($"navbar-expand-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass(BackgroundColorCssClass)
        .AddClass("bg-body-tertiary", string.IsNullOrEmpty(BackgroundColorCssClass))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}

