// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toolbar 组件用于显示工具栏内容</para>
/// <para lang="en">Toolbar Component for displaying toolbar content</para>
/// </summary>
public partial class Toolbar
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否允许换行显示工具栏内容，默认 false</para>
    /// <para lang="en">Gets or sets whether to allow toolbar content wrapping. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板</para>
    /// <para lang="en">Gets or sets the child component template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-toolbar")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass("flex-wrap: wrap;", IsWrap)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();
}
