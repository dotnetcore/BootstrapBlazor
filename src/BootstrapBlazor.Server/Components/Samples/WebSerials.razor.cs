// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// WebSerials 组件
/// </summary>
public partial class WebSerials : IDisposable
{
    private string _sendData = "";
    private int _sendInterval = 1000;
    private bool _appendCRLF;
    private bool _isHEX;
    private bool _isLoop;
    private ConsoleMessageCollection _messages = new(8);

    private string? _message;
    private string? _statusMessage;
    private string? _errorMessage;
    private readonly WebSerialOptions options = new() { BaudRate = 115200, AutoGetSignals = true };

    private readonly List<SelectedItem> _baudRateList =
    [
        new("300", "300"),
        new("600", "600"),
        new("1200", "1200"),
        new("2400", "2400"),
        new("4800", "4800"),
        new("9600", "9600"),
        new("14400", "14400"),
        new("19200", "19200"),
    ];

    private readonly List<SelectedItem> _bufferSizes =
    [
        new("255", "255"),
        new("1024", "1024")
    ];

    private readonly List<SelectedItem> _dataBits = [new("7", "7"), new("8", "8")];

    private readonly List<SelectedItem> _stopBits = [new("1", "1"), new("2", "2")];

    private bool Flag { get; set; }

    private bool IsConnected { get; set; }

    /// <summary>
    /// 收到的信号数据
    /// </summary>
    public WebSerialSignals Signals { get; set; } = new WebSerialSignals();

    [Inject, NotNull]
    private ISerialService? SerialService { get; set; }

    private ISerialPort? _serialPort;

    private SerialOptions _serialOptions = new();

    private bool CheckOpen => _serialPort is not { IsOpen: false };

    private bool CheckClose => _serialPort is not { IsOpen: true };

    private async Task GetPort()
    {
        _serialPort = await SerialService.GetPort();
    }

    private async Task OpenPort()
    {
        if (_serialPort != null)
        {
            _serialPort.DataReceive = async data =>
            {
                _messages.Add(new ConsoleMessageItem()
                {
                    IsHtml = true,
                    Message = $"{DateTime.Now}: --><br/>Text: {Encoding.ASCII.GetString(data)}<br/>HEX: {Convert.ToHexString(data)}"
                });
                await InvokeAsync(StateHasChanged);
            };
            await _serialPort.Open(_serialOptions);
        }
    }

    private async Task ClosePort()
    {
        if (_serialPort != null)
        {
            await _serialPort.Close();
        }
    }

    private CancellationTokenSource? _loopSendTokenSource;

    private async Task Write()
    {
        if (_serialPort == null)
        {
            return;
        }

        if (_isLoop)
        {
            _loopSendTokenSource ??= new CancellationTokenSource();
            while (_isLoop && _loopSendTokenSource is { IsCancellationRequested: false } && _sendInterval > 500)
            {
                try
                {
                    await InternalSend(_serialPort);
                    await Task.Delay(_sendInterval, _loopSendTokenSource.Token);
                }
                catch { }
            }
        }
        else
        {
            await InternalSend(_serialPort);
        }
    }

    private async Task InternalSend(ISerialPort serialPort)
    {
        var data = _sendData;
        if (_appendCRLF)
        {
            data += "\r\n";
        }
        var buffer = _isHEX
            ? ConvertToHex(data)
            : Encoding.ASCII.GetBytes(data);
        await serialPort.Write(buffer);
    }

    private static byte[] ConvertToHex(string data)
    {
        var ret = new List<byte>();
        for (int i = 0; i < data.Length;)
        {
            if (i + 2 <= data.Length)
            {
                var seg = data.Substring(i, 2);
                ret.Add(Convert.ToByte(seg, 16));
            }
            i = i + 2;
        }
        return [.. ret];
    }

    private Task OnSignals(WebSerialSignals? signals)
    {
        if (signals is null) return Task.CompletedTask;

        Signals = signals;

        if (!options.AutoGetSignals)
        {
            // 仅在不自动获取信号时才显示
            _message = $"{DateTime.Now:hh:mm:ss} 收到信号数据: {Environment.NewLine}" +
                            $"RING:  {signals.RING}{Environment.NewLine}" +
                            $"DSR:   {signals.DSR}{Environment.NewLine}" +
                            $"CTS:   {signals.CTS}{Environment.NewLine}" +
                            $"DCD:   {signals.DCD}{Environment.NewLine}" +
                            $"{_message}{Environment.NewLine}";
        }

        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnConnect(bool flag)
    {
        IsConnected = flag;
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
        _errorMessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private void OnApply()
    {
        //options.BaudRate = SelectedBaudRate;
        //Flag = !Flag;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "OnReceive",
            Description = "收到数据回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnSignals",
            Description = "收到信号数据回调方法",
            Type = "Func<WebSerialSignals, Task>?",
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
    ];

    /// <summary>s
    /// 获得WebSerialOptions属性方法
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetWebSerialOptionsAttributes() =>
    [
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
            Name = "StopBits",
            Description = "停止位",
            Type = "int",
            ValueList = "1|2",
            DefaultValue = "1"
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
        },
        new()
        {
            Name = nameof(WebSerialOptions.AutoGetSignals),
            Description = "获得/设置 自动检查状态",
            Type = "bool",
            ValueList = "-",
            DefaultValue ="false"
        }
    ];

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_loopSendTokenSource != null)
            {
                _loopSendTokenSource.Cancel();
                _loopSendTokenSource = null;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
