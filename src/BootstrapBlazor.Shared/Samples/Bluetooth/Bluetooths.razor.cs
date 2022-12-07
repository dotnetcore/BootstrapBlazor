// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Bluetooths
{
    Printer printer { get; set; } = new Printer();

    /// <summary>
    /// 显示内置界面
    /// </summary>
    bool ShowUI { get; set; } = false;

    private string? message;
    private string? statusmessage;
    private string? errmessage;

    private Task OnResult(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdateStatus(string message)
    {
        this.statusmessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        this.errmessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnGetDevices(List<string>? devices)
    {
        this.message = "";
        if (devices == null || devices!.Count == 0) return Task.CompletedTask;
        this.message += $"已配对设备{devices.Count}:{Environment.NewLine}";
        devices.ForEach(a => this.message += $"   {a}{Environment.NewLine}");
        //this.message = this.message.Replace(Environment.NewLine, "<br/>");
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SwitchUI()
    {
        ShowUI = !ShowUI;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Commands",
            Description = "打印指令(cpcl/esp/pos代码)",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Print",
            Description = "打印",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateStatus",
            Description = "状态更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateError",
            Description = "错误更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "PrinterElement",
            Description = "UI界面元素的引用对象,为空则使用整个页面",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Opt",
            Description = "打印机选项",
            Type = "PrinterOption",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "ShowUI",
            Description = "获得/设置 显示内置UI",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new AttributeItem() {
            Name = "Debug",
            Description = "获得/设置 显示log",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new AttributeItem() {
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
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "NamePrefix",
            Description = "初始搜索设备名称前缀,默认null",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
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
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Name",
            Description = "设备名称",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "Value",
            Description = "设备数值:例如心率/电量%",
            Type = "decimal?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "Status",
            Description = "状态",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "Error",
            Description = "错误",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },

    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributesBatteryLevel() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "GetBatteryLevel",
            Description = "查询电量",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateValue",
            Description = "数值更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateStatus",
            Description = "状态更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateError",
            Description = "错误更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "BatteryLevelElement",
            Description = "UI界面元素的引用对象",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
    };


    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributesHeartrate() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "GetHeartrate",
            Description = "连接心率带",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "StopHeartrate",
            Description = "停止监听心率",
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateValue",
            Description = "数值更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateStatus",
            Description = "状态更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnUpdateError",
            Description = "错误更新回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "HeartrateElement",
            Description = "UI界面元素的引用对象",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
    };

}
