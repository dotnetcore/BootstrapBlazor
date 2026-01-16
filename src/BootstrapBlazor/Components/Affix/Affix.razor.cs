// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Affix 固钉组件</para>
///  <para lang="en">Affix component</para>
/// </summary>
public partial class Affix
{
    /// <summary>
    ///  <para lang="zh">获得/设置 指定偏移量后触发</para>
    ///  <para lang="en">Gets or sets the offset value to trigger</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public float Offset { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 固定位置枚举 默认 <see cref="AffixPosition.Top"/></para>
    ///  <para lang="en">Gets or sets the affix position. Default is <see cref="AffixPosition.Top"/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public AffixPosition Position { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 z-index 值 默认 100</para>
    ///  <para lang="en">Gets or sets the z-index value. Default is 100</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? ZIndex { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件内容</para>
    ///  <para lang="en">Gets or sets the child content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-affix")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default("position: sticky;")
        .AddClass($"z-index: {ZIndex};", ZIndex.HasValue)
        .AddClass($"{Position.ToDescriptionString()}: {Offset.ToString(CultureInfo.InvariantCulture)}px;")
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();
}
