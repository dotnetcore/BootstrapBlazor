// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

public partial class SocketApp : ComponentBase
{
    [Inject, NotNull]
    private ITcpSocketFactory? TcpSocketFactory { get; set; }

    private Color _connectColor = Color.None;

    private bool _flash = false;

    private ITcpSocketClient _client = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 从服务中获取 Socket 实例
        _client = TcpSocketFactory.GetOrCreate("bb", key => new IPEndPoint(IPAddress.Loopback, 0));
        _flash = _client.IsConnected;
        _connectColor = _client.IsConnected ? Color.Success : Color.None;
    }

    private async Task OnConnectAsync()
    {
        if (_client is { IsConnected: false })
        {
            await _client.ConnectAsync("127.0.0.1", 8800, CancellationToken.None);
            _flash = _client.IsConnected;
            _connectColor = _client.IsConnected ? Color.Success : Color.None;
        }
    }
}
