// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart 图表组件数据集合实体类
/// </summary>
public class ChartDataset
{
    /// <summary>
    /// 获得/设置 数据集合名称
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// 获得/设置 数据集合
    /// </summary>
    public IEnumerable<object>? Data { get; set; }

    /// <summary>
    /// 获得/设置 是否填充 默认 false
    /// </summary>
    public bool Fill { get; set; }

    /// <summary>
    /// 获得/设置 折线曲率 默认 0.4
    /// </summary>
    public float Tension { get; set; } = 0.4f;

    /// <summary>
    /// 获得/设置 是否是Y2坐标轴
    /// </summary>
    [JsonIgnore]
    public bool IsAxisY2 { get; set; }

    /// <summary>
    /// Y坐标轴ID
    /// </summary> 
    public string? YAxisID { get => IsAxisY2 ? "y2" : "y"; }

    /// <summary>
    /// 数据点样式
    /// </summary>
    [JsonConverter(typeof(ChartEnumDescriptionConverter<ChartPointStyle>))]
    public ChartPointStyle PointStyle { get; set; } = ChartPointStyle.Circle;

    /// <summary>
    /// 显示数据点样式
    /// </summary>
    public bool ShowPointStyle { get; set; } = true;

    /// <summary>
    /// 数据点半径
    /// </summary>
    public int PointRadius { get; set; } = 3;

    /// <summary>
    /// 数据点悬停半径，鼠标指向时的大小
    /// </summary>
    public int PointHoverRadius { get; set; } = 4;


    /// <summary>
    /// 获得/设置 折线图(Line) 宽度 默认 3 个像素
    /// </summary>
    public double BorderWidth { get; set; } = 3;


}
