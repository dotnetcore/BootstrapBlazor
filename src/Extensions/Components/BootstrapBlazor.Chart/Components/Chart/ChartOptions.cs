// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart 图表组件配置项实体类
/// </summary>
public class ChartOptions
{
    /// <summary>
    /// 获得/设置 表格 Title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得 X 坐标轴实例集合
    /// </summary>
    public ChartAxes X { get; } = new ChartAxes();

    /// <summary>
    /// 获得 X 坐标轴实例集合
    /// </summary>

    public ChartAxes Y { get; } = new ChartAxes();

    /// <summary>
    /// 获得/设置 是否 适配移动端 默认为 true
    /// </summary>
    public bool Responsive { get; set; } = true;

    /// <summary>
    /// 获取/设置 是否 约束图表比例 默认为 true
    /// </summary>
    public bool MaintainAspectRatio { get; set; } = true;

    /// <summary>
    /// 获得/设置 数据显示颜色
    /// </summary>
    public Dictionary<string, string> Colors { get; set; } = new Dictionary<string, string>()
        {
            { "blue", "rgb(54, 162, 235)" },
            { "green", "rgb(75, 192, 192)" },
            { "red", "rgb(255, 99, 132)" },
            { "orange", "rgb(255, 159, 64)" },
            { "yellow", "rgb(255, 205, 86)" },
            { "tomato", "rgb(255, 99, 71)" },
            { "pink", "rgb(255, 192, 203)" },
            { "violet", "rgb(238, 130, 238)" }
        };
}
