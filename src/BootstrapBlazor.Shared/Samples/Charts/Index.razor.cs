// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Charts;

/// <summary>
/// 
/// </summary>
public sealed partial class Index
{

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Title",
            Description = "图表标题",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Height",
            Description = "组件高度支持单位, 如: 30% , 30px , 30em , calc(30%)",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Width",
            Description = "组件宽度支持单位, 如: 30% , 30px , 30em , calc(30%)",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Responsive",
            Description = "设置图表所在canvas是否随其容器大小变化而变化",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "MaintainAspectRatio",
            Description = "设置是否约束图表比例",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "AspectRatio",
            Description = "设置canvas的宽高比（值为1表示canvas是正方形），如果显示定义了canvas的高度，则此属性无效",
            Type = "int",
            ValueList = " - ",
            DefaultValue = "2"
        },
        new AttributeItem() {
            Name = "设置 图表尺寸延迟变化时间",
            Description = "",
            Type = "int",
            ValueList = " - ",
            DefaultValue = "0"
        },
        new AttributeItem() {
            Name = "Angle",
            Description = "设置 Bubble 模式下显示角度 180 为 半圆 360 为正圆",
            Type = "int",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new AttributeItem() {
            Name = "LoadingText",
            Description = "设置正在加载文本",
            Type = "string",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new AttributeItem() {
            Name = "ChartType",
            Description = "图表组件渲染类型",
            Type = "ChartType",
            ValueList = "Line|Bar|Pie|Doughnut|Bubble",
            DefaultValue = "Line"
        },
        new AttributeItem() {
            Name = "ChartAction",
            Description = "图表组件组件方法",
            Type = "ChartAction",
            ValueList = "Update|AddDataset|RemoveDataset|AddData|RemoveData|SetAngle|Reload",
            DefaultValue = "Update"
        },
        new AttributeItem() {
            Name = "",
            Description = "",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "OnInitAsync",
            Description="组件数据初始化委托方法",
            Type ="Func<Task<ChartDataSource>>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new AttributeItem()
        {
            Name = "OnAfterInitAsync",
            Description="客户端绘制图表完毕后回调此委托方法",
            Type ="Func<Task>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new AttributeItem()
        {
            Name = "OnAfterUpdateAsync",
            Description="客户端更新图表完毕后回调此委托方法",
            Type ="Func<ChartAction, Task>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new AttributeItem()
        {
            Name = "Update",
            Description="更新图表方法",
            Type ="Task",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new AttributeItem()
        {
            Name = "Reload",
            Description="重新加载,强制重新渲染图表",
            Type ="Task",
            ValueList = " - ",
            DefaultValue = " - "
        }

    };
}
