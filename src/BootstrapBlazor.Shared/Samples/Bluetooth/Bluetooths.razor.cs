// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Bluetooths
/// </summary>
public partial class Bluetooths
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {

        new() {
            Name = "Commands",
            Description = "打印指令(cpcl/esp/pos代码)",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new() {
            Name = "Print",
            Description = "打印",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new() {
            Name = "OnUpdateStatus",
            Description = "状态更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new() {
            Name = "OnUpdateError",
            Description = "错误更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new() {
            Name = "PrinterElement",
            Description = "UI界面元素的引用对象,为空则使用整个页面",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
        new() {
            Name = "Opt",
            Description = "打印机选项",
            Type = "PrinterOption",
            ValueList = "-",
            DefaultValue = "-"
        },
        new() {
            Name = "ShowUI",
            Description = "获得/设置 显示内置UI",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new() {
            Name = "Debug",
            Description = "获得/设置 显示log",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new() {
            Name = "Devicename",
            Description = "获得/设置 设备名称",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetPrinterOptionAttributes() => new AttributeItem[]
    {

        new() {
            Name = "NamePrefix",
            Description = "初始搜索设备名称前缀,默认null",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new() {
            Name = "MaxChunk",
            Description = "数据切片大小,默认100",
            Type = "int",
            ValueList = "-",
            DefaultValue = "100"
        },
    };

    /// <summary>
    /// 获得蓝牙设备类
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetBluetoothDeviceAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Name",
            Description = "设备名称",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "Value",
            Description = "设备数值:例如心率/电量%",
            Type = "decimal?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "Status",
            Description = "状态",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "Error",
            Description = "错误",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected static IEnumerable<AttributeItem> GetAttributesBatteryLevel() => new AttributeItem[]
    {
        new()
        {
            Name = "GetBatteryLevel",
            Description = "查询电量",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateValue",
            Description = "数值更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateStatus",
            Description = "状态更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateError",
            Description = "错误更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "BatteryLevelElement",
            Description = "UI界面元素的引用对象",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        }
    };


    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributesHeartrate() => new AttributeItem[]
    {
        new()
        {
            Name = "GetHeartrate",
            Description = "连接心率带",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "StopHeartrate",
            Description = "停止监听心率",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateValue",
            Description = "数值更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateStatus",
            Description = "状态更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateError",
            Description = "错误更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "HeartrateElement",
            Description = "UI界面元素的引用对象",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        }
    };
}
