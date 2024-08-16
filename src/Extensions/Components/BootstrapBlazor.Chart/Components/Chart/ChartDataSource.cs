// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart 图表组件数据源
/// </summary>
public class ChartDataSource
{
    /// <summary>
    /// 获得/设置 图表 X 轴显示数据标签集合
    /// </summary>
    public IEnumerable<string>? Labels { get; set; }

    /// <summary>
    /// 获得 图表 数据集
    /// </summary>
    [JsonIgnore]
    public List<ChartDataset> Data { get; } = [];

    /// <summary>
    /// 传递给 JS 的配置装箱实例，内部使用，添加数据集请使用 <see cref="Data"/> 属性。
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<object> DataJS { get => Data.Cast<object>().ToList(); }

    /// <summary>
    /// 获得 组件配置项 设置标题 轴坐标等
    /// </summary>
    public ChartOptions Options { get; } = new ChartOptions();

    /// <summary>
    /// 获得/设置 图标类型 默认为 line
    /// </summary>
    public string Type { get; set; } = "line";

    /// <summary>
    /// 获得/设置 扩展数据 默认为空 序列化到浏览器与数据集合 <see cref="ChartDataSource "/> 合并，方便把组件未提供的参数传递到浏览器
    /// </summary>
    public object? AppendData { get; set; }
}
