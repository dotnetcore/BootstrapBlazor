// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">NavMenu 组件基类</para>
///  <para lang="en">NavMenu Component Base Class</para>
/// </summary>
public partial class Nav
{
    /// <summary>
    ///  <para lang="zh">获得 组件样式</para>
    ///  <para lang="en">Get Component Style</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("nav")
        .AddClass("justify-content-center", Alignment == Alignment.Center && !IsVertical)
        .AddClass("justify-content-end", Alignment == Alignment.Right && !IsVertical)
        .AddClass("flex-column", IsVertical)
        .AddClass("nav-pills", IsPills)
        .AddClass("nav-fill", IsFill)
        .AddClass("nav-justified", IsJustified)
        .AddClass("text-end", Alignment == Alignment.Right && IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 组件数据源</para>
    ///  <para lang="en">Get/Set Component Data Source</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<NavLink>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件对齐方式</para>
    ///  <para lang="en">Get/Set Component Alignment</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; } = Alignment.Left;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否垂直分布</para>
    ///  <para lang="en">Get/Set Whether to distribute vertically</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否为胶囊</para>
    ///  <para lang="en">Get/Set Whether it is pills</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsPills { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否填充</para>
    ///  <para lang="en">Get/Set Whether to fill</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsFill { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否等宽</para>
    ///  <para lang="en">Get/Set Whether to be equal width</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsJustified { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件内容</para>
    ///  <para lang="en">Get/Set Component Content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">OnParametersSet 方法</para>
    ///  <para lang="en">OnParametersSet Method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<NavLink>();
    }
}
