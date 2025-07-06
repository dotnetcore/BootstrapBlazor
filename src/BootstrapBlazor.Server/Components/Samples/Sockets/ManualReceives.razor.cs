// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;
using System.Text;

namespace BootstrapBlazor.Server.Components.Samples.Sockets;

/// <summary>
/// 接收电文示例
/// </summary>
public partial class ManualReceives
{
    [Inject, NotNull]
    private ITcpSocketFactory? TcpSocketFactory { get; set; }

    private ITcpSocketClient _client = null!;

    private List<ConsoleMessageItem> _items = [];

    private readonly IPEndPoint _serverEndPoint = new(IPAddress.Loopback, 8810);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 从服务中获取 Socket 实例
        _client = TcpSocketFactory.GetOrCreate("demo-manual-receive", options =>
        {
            options.LocalEndPoint = new IPEndPoint(IPAddress.Loopback, 0);
            options.IsAutoReceive = false;
        });
    }

    private async Task OnConnectAsync()
    {
        if (_client is { IsConnected: false })
        {
            await _client.ConnectAsync(_serverEndPoint, CancellationToken.None);
        }
    }

    private async Task OnCloseAsync()
    {
        if (_client is { IsConnected: true })
        {
            await _client.CloseAsync();
        }
    }

    private Task OnClear()
    {
        _items = [];
        return Task.CompletedTask;
    }

    private async Task OnSendAsync()
    {
        if (_client is { IsConnected: true })
        {
            // 准备通讯数据
            var data = new byte[2] { 0x01, 0x02 };
            var result = await _client.SendAsync(data, CancellationToken.None);
            var state = result ? "成功" : "失败";

            // 记录日志
            _items.Add(new ConsoleMessageItem()
            {
                Message = $"{DateTime.Now}: 发送数据 {_client.LocalEndPoint} - {_serverEndPoint} Data: {BitConverter.ToString(data)} {state}"
            });

            if (result)
            {
                var buffer = await _client.ReceiveAsync(CancellationToken.None);
                var payload = buffer.ToArray();
                _items.Add(new ConsoleMessageItem()
                {
                    Message = $"{DateTime.Now}: 接收数据 {_client.LocalEndPoint} - {_serverEndPoint} Data {Encoding.UTF8.GetString(payload)} HEX: {BitConverter.ToString(payload)} 成功",
                    Color = Color.Success
                });
            }
        }
    }
}
