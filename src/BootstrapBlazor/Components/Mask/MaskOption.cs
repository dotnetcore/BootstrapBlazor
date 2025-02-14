// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// MaskOption 配置类
/// </summary>
public class MaskOption
{
    /// <summary>
    /// 获得/设置 z-index 值 默认 未设置 使用 样式 1050
    /// </summary>
    public int? ZIndex { get; set; }

    /// <summary>
    /// 获得/设置 opacity 值 默认 未设置 使用 样式 0.5
    /// </summary>
    public float? Opacity { get; set; }

    /// <summary>
    /// 获得/设置 background-color 值 默认 未设置 使用 样式 #000
    /// </summary>
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 遮罩父容器 id 默认 null 未设置
    /// </summary>
    public string? ContainerId { get; set; }

    /// <summary>
    /// 获得/设置 遮罩父容器选择器 Selector 默认 null 未设置
    /// </summary>
    public string? Selector { get; set; }
}
