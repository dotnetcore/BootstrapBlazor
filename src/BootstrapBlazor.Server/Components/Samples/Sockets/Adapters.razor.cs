// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Components.Components;
using System.Net;
using System.Text;

namespace BootstrapBlazor.Server.Components.Samples.Sockets;

/// <summary>
/// 数据适配器示例
/// </summary>
public partial class Adapters : IDisposable
{
    [Inject, NotNull]
    private ITcpSocketFactory? TcpSocketFactory { get; set; }

    private ITcpSocketClient _client = null!;

    private List<ConsoleMessageItem> _items = [];

    private readonly IPEndPoint _serverEndPoint = new(IPAddress.Loopback, 8900);

    private readonly CancellationTokenSource _connectTokenSource = new();
    private readonly CancellationTokenSource _sendTokenSource = new();
    private readonly CancellationTokenSource _receiveTokenSource = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 从服务中获取 ITcpSocketClient 实例
        _client = TcpSocketFactory.GetOrCreate("demo-adapter", options =>
        {
            // 关闭自动接收功能
            options.IsAutoReceive = false;
            // 设置本地使用的 IP地址与端口
            options.LocalEndPoint = new IPEndPoint(IPAddress.Loopback, 0);
        });
    }

    private async Task OnConnectAsync()
    {
        if (_client is { IsConnected: false })
        {
            await _client.ConnectAsync(_serverEndPoint, _connectTokenSource.Token);
            var state = _client.IsConnected ? "成功" : "失败";
            _items.Add(new ConsoleMessageItem()
            {
                Message = $"{DateTime.Now}: 连接 {_client.LocalEndPoint} - {_serverEndPoint} {state}",
                Color = _client.IsConnected ? Color.Success : Color.Danger
            });
        }
    }

    private async Task OnSendAsync()
    {
        if (_client is { IsConnected: true })
        {
            // 准备通讯数据
            var data = new byte[12];
            "2025"u8.CopyTo(data);
            Encoding.UTF8.GetBytes(DateTime.Now.ToString("ddHHmmss")).CopyTo(data, 4);
            var result = await _client.SendAsync(data, _sendTokenSource.Token);
            if (result)
            {
                // 发送成功
                var payload = await _client.ReceiveAsync(_receiveTokenSource.Token);
                if (!payload.IsEmpty)
                {
                    // 解析接收到的数据
                    // 响应头: 4 字节表示响应体长度 [0x32, 0x30, 0x32, 0x35]
                    // 响应体: 8 字节当前时间戳字符串
                    data = payload.ToArray();
                    var body = BitConverter.ToString(data);
                    _items.Add(new ConsoleMessageItem()
                    {
                        Message = $"{DateTime.Now}: 接收到来自 {_serverEndPoint} 数据: {Encoding.UTF8.GetString(data)} HEX: {body}"
                    });
                }
            }
        }
    }

    private async Task OnCloseAsync()
    {
        if (_client is { IsConnected: true })
        {
            await _client.CloseAsync();
            var state = _client.IsConnected ? "失败" : "成功";
            _items.Add(new ConsoleMessageItem()
            {
                Message = $"{DateTime.Now}: 关闭 {_client.LocalEndPoint} - {_serverEndPoint} {state}",
                Color = _client.IsConnected ? Color.Danger : Color.Success
            });
        }
    }

    private Task OnClear()
    {
        _items = [];
        return Task.CompletedTask;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // 释放连接令牌资源
            _connectTokenSource.Cancel();
            _connectTokenSource.Dispose();

            // 释放发送令牌资源
            _sendTokenSource.Cancel();
            _sendTokenSource.Dispose();

            // 释放接收令牌资源
            _receiveTokenSource.Cancel();
            _receiveTokenSource.Dispose();
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
