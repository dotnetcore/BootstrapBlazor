// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// LayoutSidebar 组件
/// </summary>
public partial class LayoutSplitBar
{
    /// <summary>
    /// 获得/设置 容器选择器 默认 null 未设置
    /// 组件拖动后设置容器 style="--bb-layout-sidebar-width: 200px;" 用于宽度调整
    /// </summary>
    [Parameter]
    public string? ContainerSelector { get; set; }

    /// <summary>
    /// 获得/设置 最小宽度 默认 null 未设置
    /// </summary>
    [Parameter]
    public int? Min { get; set; }

    /// <summary>
    /// 获得/设置 最大宽度 默认 null 未设置
    /// </summary>
    [Parameter]
    public int? Max { get; set; }

    private string? _minWidthString => Min.HasValue ? $"{Min}" : null;

    private string? _maxWidthString => Max.HasValue ? $"{Max}" : null;
}
