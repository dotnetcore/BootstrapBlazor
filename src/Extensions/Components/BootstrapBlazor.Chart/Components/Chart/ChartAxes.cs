// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart 图表坐标轴实体类
/// </summary>
public class ChartAxes
{
    /// <summary>
    /// 获得/设置 坐标轴显示名称
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 坐标轴显示名称颜色
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// 获得/设置 是否堆砌 默认 false
    /// </summary>
    public bool Stacked { get; set; }
}
