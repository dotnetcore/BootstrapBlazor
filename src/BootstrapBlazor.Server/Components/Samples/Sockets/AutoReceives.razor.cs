// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Server.Components.Samples.Sockets;

/// <summary>
/// 接收电文示例
/// </summary>
public partial class AutoReceives : IDisposable
{
    [Inject, NotNull]
    private ITcpSocketFactory? TcpSocketFactory { get; set; }

    private ITcpSocketClient _client = null!;

    private List<ConsoleMessageItem> _items = [];

    private readonly IPEndPoint _serverEndPoint = new(IPAddress.Loopback, 8800);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 从服务中获取 Socket 实例
        _client = TcpSocketFactory.GetOrCreate("demo-auto-receive", options =>
        {
            options.LocalEndPoint = new IPEndPoint(IPAddress.Loopback, 0);
        });
        _client.ReceivedCallback += OnReceivedAsync;
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

    private async ValueTask OnReceivedAsync(ReadOnlyMemory<byte> data)
    {
        // 将数据显示为十六进制字符串
        var payload = System.Text.Encoding.UTF8.GetString(data.Span);
        _items.Add(new ConsoleMessageItem
        {
            Message = $"接收到来自站点的数据为 {payload}"
        });

        // 保持队列中最大数量为 50
        if (_items.Count > 50)
        {
            _items.RemoveAt(0);
        }
        await InvokeAsync(StateHasChanged);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_client is { IsConnected: true })
            {
                _client.ReceivedCallback -= OnReceivedAsync;
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
