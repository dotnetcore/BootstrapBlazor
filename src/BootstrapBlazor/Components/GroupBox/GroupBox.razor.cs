// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">GroupBox 组件</para>
/// <para lang="en">GroupBox Component</para>
/// </summary>
public sealed partial class GroupBox
{
    private string? ClassString => CssBuilder.Default("groupbox")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets Child Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Title 属性 默认为 null</para>
    /// <para lang="en">Gets or sets Title Property Default null</para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }
}
