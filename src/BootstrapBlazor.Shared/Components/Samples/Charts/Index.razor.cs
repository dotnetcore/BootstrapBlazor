﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Charts;

/// <summary>
/// 
/// </summary>
public sealed partial class Index
{
    /// <summary>
    /// 获得属性列表
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new() {
            Name = "Title",
            Description = "图表标题",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Height",
            Description = "组件高度支持单位, 如: 30% , 30px , 30em , calc(30%)",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Width",
            Description = "组件宽度支持单位, 如: 30% , 30px , 30em , calc(30%)",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Responsive",
            Description = "设置图表所在canvas是否随其容器大小变化而变化",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = "MaintainAspectRatio",
            Description = "设置是否约束图表比例",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = "AspectRatio",
            Description = "设置canvas的宽高比（值为1表示canvas是正方形），如果显示定义了canvas的高度，则此属性无效",
            Type = "int",
            ValueList = " - ",
            DefaultValue = "2"
        },
        new() {
            Name = "ResizeDelay",
            Description = "设置 图表尺寸延迟变化时间",
            Type = "int",
            ValueList = " - ",
            DefaultValue = "0"
        },
        new() {
            Name = "Angle",
            Description = "设置 Bubble 模式下显示角度 180 为 半圆 360 为正圆",
            Type = "int",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new() {
            Name = "LoadingText",
            Description = "设置正在加载文本",
            Type = "string",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new() {
            Name = "ChartType",
            Description = "图表组件渲染类型",
            Type = "ChartType",
            ValueList = "Line|Bar|Pie|Doughnut|Bubble",
            DefaultValue = "Line"
        },
        new() {
            Name = "ChartAction",
            Description = "图表组件组件方法",
            Type = "ChartAction",
            ValueList = "Update|AddDataset|RemoveDataset|AddData|RemoveData|SetAngle|Reload",
            DefaultValue = "Update"
        },
        new() {
            Name = "DisplayLegend",
            Description = "是否显示图例",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = "LegendPosition",
            Description = "图例显示位置",
            Type = "ChartLegendPosition",
            ValueList = "Top|Bottom|Left|Right",
            DefaultValue = "Top"
        },
        new()
        {
            Name = "OnInitAsync",
            Description="组件数据初始化委托方法",
            Type ="Func<Task<ChartDataSource>>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = "OnAfterInitAsync",
            Description="客户端绘制图表完毕后回调此委托方法",
            Type ="Func<Task>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = "OnAfterUpdateAsync",
            Description="客户端更新图表完毕后回调此委托方法",
            Type ="Func<ChartAction, Task>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = "Update",
            Description="更新图表方法",
            Type ="Task",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = "Reload",
            Description="重新加载,强制重新渲染图表",
            Type ="Task",
            ValueList = " - ",
            DefaultValue = " - "
        }

    ];

    /// <summary>
    /// 获得方法列表
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<MethodItem> GetMethodAttributes() => new MethodItem[]
    {
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.OnInitAsync),
            Description = "组件数据初始化委托方法",
            Parameters = "Func<Task<ChartDataSource>>",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.OnAfterInitAsync),
            Description = "客户端绘制图表完毕后回调此委托方法",
            Parameters = "Func<Task>",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.OnAfterUpdateAsync),
            Description = "客户端更新图表完毕后回调此委托方法",
            Parameters = "Func<ChartAction, Task>",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.Update),
            Description = "更新图表方法",
            Parameters ="Task",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.Reload),
            Description = "重新加载,强制重新渲染图表",
            Parameters = "Task",
            ReturnValue = " — "
        }
    };
}
