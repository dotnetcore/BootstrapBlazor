// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/


using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// WebSerials
/// </summary>
public partial class WebSerials
{

    private string? message;
    private string? statusmessage;
    private string? errmessage;
    private WebSerialOptions options = new WebSerialOptions() { BaudRate = 115200 };

    [NotNull]
    private IEnumerable<SelectedItem> BaudRateList { get; set; } = ListToSelectedItem();

    [DisplayName("波特率")]
    private int SelectedBaudRate { get; set; } = 115200;
    private bool Flag { get; set; }
    private bool IsConnected { get; set; }

    [NotNull]
    private WebSerial? WebSerial { get; set; }

    private Task OnReceive(string? message)
    {
        this.message = $"{DateTime.Now:hh:mm:ss} 收到数据: {message}{Environment.NewLine}" + this.message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnConnect(bool flag)
    {
        this.IsConnected = flag;
        if (flag)
        {
            message = null;
            statusmessage = null;
            errmessage = null;
        }
        else
        {
            //Flag=false;
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnLog(string message)
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<SelectedItem> ListToSelectedItem()
    {
        foreach (var item in WebSerialOptions.BaudRateList)
        {
            yield return new SelectedItem(item.ToString(), item.ToString());
        }
    }

    private Task OnApply()
    {
        options.BaudRate = SelectedBaudRate;
        Flag = !Flag;
        //StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "OnReceive",
            Description = "收到数据回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnLog",
            Description = "Log回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnError",
            Description = "错误回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Element",
            Description = "UI界面元素的引用对象,为空则使用整个页面",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "ConnectBtnTitle",
            Description = "获得/设置 连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "连接"
        },
        new AttributeItem() {
            Name = "DisconnectBtnTitle",
            Description = "获得/设置 断开连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "断开连接"
        },
        new AttributeItem() {
            Name = "WriteBtnTitle",
            Description = "获得/设置 写入按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "写入"
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
    };

    /// <summary>s
    /// 获得WebSerialOptions属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetWebSerialOptionsAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "BaudRate",
            Description = "波特率",
            Type = "int",
            ValueList = "-",
            DefaultValue = "9600"
        },
        new AttributeItem() {
            Name = "DataBits",
            Description = "数据位",
            Type = "int",
            ValueList = "7|8",
            DefaultValue = "8"
        },
        new AttributeItem() {
            Name = "ParityType",
            Description = "流控制",
            Type = "WebSerialFlowControlType",
            ValueList = "none|even|odd",
            DefaultValue = "none"
        },
        new AttributeItem() {
            Name = "StopBits",
            Description = "停止位",
            Type = "int",
            ValueList = "1|2",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "BufferSize",
            Description = "读写缓冲区",
            Type = "int",
            ValueList = "-",
            DefaultValue = "255"
        },
        new AttributeItem() {
            Name = "FlowControlType",
            Description = "校验",
            Type = "WebSerialParityType",
            ValueList = "none|hardware",
            DefaultValue = "none"
        },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.InputWithHex),
                Description = "HEX发送",
                Type =  "bool",
                ValueList = "-",
                DefaultValue = "false"
            },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.OutputInHex),
                Description = "HEX接收",
                Type =  "bool",
                ValueList = "-",
                DefaultValue ="false"
            },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.AutoConnect),
                Description = "自动连接设备",
                Type =  "bool",
                ValueList = "-",
                DefaultValue ="true"
            },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.AutoFrameBreakType),
                Description = "自动断帧方式",
                Type =  "AutoFrameBreakType",
                ValueList = "-",
                DefaultValue ="Character"
            },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.FrameBreakChar),
                Description = "断帧字符",
                Type =  "string",
                ValueList = "-",
                DefaultValue ="\\n"
            },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.Break),
                Description = "Break", 
                Type =  "bool",
                ValueList = "-",
                DefaultValue ="false"
            },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.DTR),
                Description = "DTR",
                Type =  "bool",
                ValueList = "-",
                DefaultValue ="false"
            },
            new AttributeItem(){
                Name =nameof(WebSerialOptions.RTS),
                Description = "RTS",
                Type =  "bool",
                ValueList = "-",
                DefaultValue ="false"
            },
             new AttributeItem() {
                Name = nameof(WebSerialOptions.ConnectBtnTitle),
                Description = "获得/设置 连接按钮文本",
                Type = "string",
                ValueList = "",
                DefaultValue = "连接"
            },
            new AttributeItem() {
                Name = nameof(WebSerialOptions.DisconnectBtnTitle),
                Description = "获得/设置 断开连接按钮文本",
                Type = "string",
                ValueList = "",
                DefaultValue = "连接"
            },
            new AttributeItem() {
                Name = nameof(WebSerialOptions.WriteBtnTitle),
                Description = "获得/设置 写入按钮文本",
                Type = "string",
                ValueList = "",
                DefaultValue = "写入"
            },    };

}
