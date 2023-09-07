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

    private string? _message;
    private string? _statusMessage;
    private string? _errorMessage;
    private readonly WebSerialOptions options = new() { BaudRate = 115200 };

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
        this._message = $"{DateTime.Now:hh:mm:ss} 收到数据: {message}{Environment.NewLine}" + this._message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnConnect(bool flag)
    {
        this.IsConnected = flag;
        if (flag)
        {
            _message = null;
            _statusMessage = null;
            _errorMessage = null;
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnLog(string message)
    {
        _statusMessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        this._errorMessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static IEnumerable<SelectedItem> ListToSelectedItem()
    {
        foreach (var item in WebSerialOptions.BaudRateList)
        {
            yield return new SelectedItem(item.ToString(), item.ToString());
        }
    }

    private void OnApply()
    {
        options.BaudRate = SelectedBaudRate;
        Flag = !Flag;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "OnReceive",
            Description = "收到数据回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnLog",
            Description = "Log回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnError",
            Description = "错误回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Element",
            Description = "UI界面元素的引用对象,为空则使用整个页面",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "ConnectBtnTitle",
            Description = "获得/设置 连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "连接"
        },
        new()
        {
            Name = "DisconnectBtnTitle",
            Description = "获得/设置 断开连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "断开连接"
        },
        new()
        {
            Name = "WriteBtnTitle",
            Description = "获得/设置 写入按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "写入"
        },
        new()
        {
            Name = "ShowUI",
            Description = "获得/设置 显示内置 UI",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new()
        {
            Name = "Debug",
            Description = "获得/设置 显示 log",
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        }
    };

    /// <summary>s
    /// 获得WebSerialOptions属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetWebSerialOptionsAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "BaudRate",
            Description = "波特率",
            Type = "int",
            ValueList = "-",
            DefaultValue = "9600"
        },
        new()
        {
            Name = "DataBits",
            Description = "数据位",
            Type = "int",
            ValueList = "7|8",
            DefaultValue = "8"
        },
        new()
        {
            Name = "ParityType",
            Description = "流控制",
            Type = "WebSerialFlowControlType",
            ValueList = "none|even|odd",
            DefaultValue = "none"
        },
        new()
        {
            Name = "StopBits",
            Description = "停止位",
            Type = "int",
            ValueList = "1|2",
            DefaultValue = "1"
        },
        new()
        {
            Name = "BufferSize",
            Description = "读写缓冲区",
            Type = "int",
            ValueList = "-",
            DefaultValue = "255"
        },
        new()
        {
            Name = "FlowControlType",
            Description = "校验",
            Type = "WebSerialParityType",
            ValueList = "none|hardware",
            DefaultValue = "none"
        },
        new()
        {
            Name =nameof(WebSerialOptions.InputWithHex),
            Description = "HEX发送",
            Type =  "bool",
            ValueList = "-",
            DefaultValue = "false"
        },
        new()
        {
            Name =nameof(WebSerialOptions.OutputInHex),
            Description = "HEX接收",
            Type =  "bool",
            ValueList = "-",
            DefaultValue ="false"
        },
        new()
        {
            Name =nameof(WebSerialOptions.AutoConnect),
            Description = "自动连接设备",
            Type =  "bool",
            ValueList = "-",
            DefaultValue ="true"
        },
        new()
        {
            Name =nameof(WebSerialOptions.AutoFrameBreakType),
            Description = "自动断帧方式",
            Type =  "AutoFrameBreakType",
            ValueList = "-",
            DefaultValue ="Character"
        },
        new(){
            Name =nameof(WebSerialOptions.FrameBreakChar),
            Description = "断帧字符",
            Type =  "string",
            ValueList = "-",
            DefaultValue ="\\n"
        },
        new()
        {
            Name =nameof(WebSerialOptions.Break),
            Description = "Break",
            Type =  "bool",
            ValueList = "-",
            DefaultValue ="false"
        },
        new()
        {
            Name =nameof(WebSerialOptions.DTR),
            Description = "DTR",
            Type =  "bool",
            ValueList = "-",
            DefaultValue ="false"
        },
        new()
        {
            Name =nameof(WebSerialOptions.RTS),
            Description = "RTS",
            Type =  "bool",
            ValueList = "-",
            DefaultValue ="false"
        },
        new()
        {
            Name = nameof(WebSerialOptions.ConnectBtnTitle),
            Description = "获得/设置 连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "连接"
        },
        new()
        {
            Name = nameof(WebSerialOptions.DisconnectBtnTitle),
            Description = "获得/设置 断开连接按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "连接"
        },
        new()
        {
            Name = nameof(WebSerialOptions.WriteBtnTitle),
            Description = "获得/设置 写入按钮文本",
            Type = "string",
            ValueList = "",
            DefaultValue = "写入"
        }
    };
}
