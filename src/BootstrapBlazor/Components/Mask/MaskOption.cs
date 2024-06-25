// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}
