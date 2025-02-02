// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Affix 固钉组件
/// </summary>
public partial class Affix
{
    /// <summary>
    /// 获得/设置 指定偏移量后触发
    /// </summary>
    [Parameter]
    public float Offset { get; set; }

    /// <summary>
    /// 获得/设置 固定位置枚举 默认 <see cref="AffixPosition.Top"/>
    /// </summary>
    [Parameter]
    public AffixPosition Position { get; set; }

    /// <summary>
    /// 获得/设置 z-index 值 默认 100
    /// </summary>
    [Parameter]
    public int? ZIndex { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-affix")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default("position: sticky;")
        .AddStyle("z-index", $"{ZIndex}", ZIndex.HasValue)
        .AddStyle(Position.ToDescriptionString(), $"{Offset}px")
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();
}
