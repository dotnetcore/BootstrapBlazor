// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart 图表组件配置项实体类
/// </summary>
public class ChartOptions
{
    /// <summary>
    /// 获得/设置 图表 Title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得 X 坐标轴实例集合
    /// </summary>
    public ChartAxes X { get; } = new ChartAxes();

    /// <summary>
    /// 获得 Y 坐标轴实例集合
    /// </summary>

    public ChartAxes Y { get; } = new ChartAxes();

    /// <summary>
    /// 获得 Y2 坐标轴实例集合
    /// </summary>

    public ChartAxes Y2 { get; } = new ChartAxes();

    /// <summary>
    /// 获得/设置 图表所在canvas是否随其容器大小变化而变化 默认为 true
    /// </summary>
    public bool? Responsive { get; set; }

    /// <summary>
    /// 获取/设置 是否 约束图表比例 默认为 true
    /// </summary>
    public bool? MaintainAspectRatio { get; set; }

    /// <summary>
    /// 获得/设置 设置canvas的宽高比（值为1表示canvas是正方形），如果显示定义了canvas的高度，则此属性无效 默认为 2
    /// </summary>
    public int? AspectRatio { get; set; }

    /// <summary>
    /// 获得/设置 图表尺寸延迟变化时间 默认为 0
    /// </summary>
    public int? ResizeDelay { get; set; }

    /// <summary>
    /// 获得/设置 图表canvas高度 默认为空,跟随容器高度<para>如: 300px</para>
    /// </summary>
    public string? Height { get; set; }

    /// <summary>
    /// 获得/设置 图表canvas宽度 默认为空,跟随容器高度<para>如: 300px</para>
    /// </summary>
    public string? Width { get; set; }

    /// <summary>
    /// 获得/设置 折线图(Line) 宽度 默认 3 个像素
    /// </summary>
    public double BorderWidth { get; set; } = 3;

    /// <summary>
    /// 获得/设置 是否显示图例 默认 true 显示
    /// </summary>
    public bool ShowLegend { get; set; } = true;

    /// <summary>
    /// 获得/设置 图例显示位置 默认 top 显示
    /// </summary>
    [JsonConverter(typeof(ChartLegendPositionConverter))]
    public ChartLegendPosition LegendPosition { get; set; } = ChartLegendPosition.Top;

    /// <summary>
    /// 获得/设置 是否显示X轴线
    /// </summary>
    public bool? ShowXLine { get; set; }

    /// <summary>
    /// 获得/设置 是否显示Y轴线
    /// </summary>
    public bool? ShowYLine { get; set; }

    /// <summary>
    /// 获得/设置 是否显示X轴刻度
    /// </summary>
    public bool? ShowXScales { get; set; }

    /// <summary>
    /// 获得/设置 是否显示Y轴刻度
    /// </summary>
    public bool? ShowYScales { get; set; }

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
