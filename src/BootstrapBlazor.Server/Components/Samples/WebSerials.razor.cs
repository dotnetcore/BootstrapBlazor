// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// WebSerials 组件
/// </summary>
public partial class WebSerials : IAsyncDisposable
{
    private string _sendData = "";
    private int _sendInterval = 1000;
    private bool _appendCRLF;
    private bool _isHEX;
    private bool _isLoop;
    private readonly ConsoleMessageCollection _messages = new(8);

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

    [Inject, NotNull]
    private ISerialService? SerialService { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private ISerialPort? _serialPort;

    private readonly SerialOptions _serialOptions = new();

    private bool CheckOpen => _serialPort is not { IsOpen: false };

    private bool CheckClose => _serialPort is not { IsOpen: true };

    private async Task GetPort()
    {
        _serialPort = await SerialService.GetPort();
        if (SerialService.IsSupport == false)
        {
            await ToastService.Error(Localizer["NotSupportSerialTitle"], Localizer["NotSupportSerialContent"]);
        }
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

            if (_serialPort.IsOpen == false)
            {
                await ToastService.Error(Localizer["OpenPortSerialTitle"], Localizer["OpenPortSerialContent"]);
            }
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

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
            if (_loopSendTokenSource != null)
            {
                _loopSendTokenSource.Cancel();
                _loopSendTokenSource = null;
            }
        if (_serialPort != null)
        {
            await _serialPort.DisposeAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
