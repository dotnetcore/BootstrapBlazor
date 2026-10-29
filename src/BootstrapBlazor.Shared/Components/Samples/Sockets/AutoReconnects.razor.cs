// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Shared.Components.Samples.Sockets;

/// <summary>
/// <para lang="zh">Socket 自动重连示例。</para>
/// <para lang="en">Socket automatic reconnect sample.</para>
/// </summary>
public partial class AutoReconnects : IAsyncDisposable
{
    [Inject, NotNull]
    private ITcpSocketFactory? TcpSocketFactory { get; set; }

    private ITcpSocketClient _client = null!;
    private bool _started;
    private bool _disposed;

    private List<ConsoleMessageItem> _items = [];

    private readonly IPEndPoint _serverEndPoint = new(IPAddress.Loopback, 8901);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 从服务中获取 Socket 实例
        _client = TcpSocketFactory.GetOrCreate(options =>
        {
            options.LocalEndPoint = new IPEndPoint(IPAddress.Loopback, 0);
            options.IsAutoReconnect = true;
            options.ReconnectInterval = 5000;
        });
        _client.ReceivedCallback += OnReceivedAsync;
        _client.OnConnecting = () => AddMessageAsync(new ConsoleMessageItem { Message = $"{DateTime.Now} 正在连接到 {_serverEndPoint}，请稍候..." });
        _client.OnConnected = () => AddMessageAsync(new ConsoleMessageItem { Message = $"{DateTime.Now} 已连接到 {_serverEndPoint}，等待接收数据", Color = Color.Success });
    }

    private async Task OnConnectAsync()
    {
        if (!_started)
        {
            _started = true;
            try
            {
                _started = await _client.ConnectAsync(_serverEndPoint, CancellationToken.None);
            }
            catch
            {
                _started = false;
                throw;
            }
        }
    }

    private async Task OnCloseAsync()
    {
        _started = false;
        await _client.CloseAsync();
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
        await AddMessageAsync(data.IsEmpty
            ? new ConsoleMessageItem { Message = $"{DateTime.Now} 当前连接已关闭，5s 后自动重连", Color = Color.Danger }
            : new ConsoleMessageItem { Message = $"{DateTime.Now} 接收到来自站点的数据为 {payload}" });
    }

    private Task AddMessageAsync(ConsoleMessageItem item) => InvokeAsync(() =>
    {
        if (_disposed)
        {
            return;
        }
        _items.Add(item);
        // 保持队列中最大数量为 50
        while (_items.Count > 50)
        {
            _items.RemoveAt(0);
        }
        StateHasChanged();
    });

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        _disposed = true;
        _started = false;
        _client.ReceivedCallback -= OnReceivedAsync;
        _client.OnConnecting = null;
        _client.OnConnected = null;
        await _client.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
