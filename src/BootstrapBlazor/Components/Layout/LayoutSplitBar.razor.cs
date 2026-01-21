// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">LayoutSplitBar 组件</para>
/// <para lang="en">LayoutSplitBar Component</para>
/// </summary>
public partial class LayoutSplitBar
{
    /// <summary>
    /// <para lang="zh">获得/设置 容器选择器 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Container Selector. Default null</para>
    /// <para lang="zh">组件拖动后设置容器 style="--bb-layout-sidebar-width: 200px;" 用于宽度调整</para>
    /// <para lang="en">Set container style="--bb-layout-sidebar-width: 200px;" after dragging component for width adjustment</para>
    /// </summary>
    [Parameter]
    public string? ContainerSelector { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最小宽度 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Minimum Width. Default null</para>
    /// </summary>
    [Parameter]
    public int? Min { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最大宽度 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Maximum Width. Default null</para>
    /// </summary>
    [Parameter]
    public int? Max { get; set; }

    private string? _minWidthString => Min.HasValue ? $"{Min}" : null;

    private string? _maxWidthString => Max.HasValue ? $"{Max}" : null;
}
