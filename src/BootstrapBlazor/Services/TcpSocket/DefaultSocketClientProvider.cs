// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;
using System.Net.Sockets;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

/// <summary>
/// TcpSocket 客户端默认实现
/// </summary>
[UnsupportedOSPlatform("browser")]
class DefaultSocketClientProvider : ISocketClientProvider
{
    private TcpClient? _client;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsConnected => _client?.Connected ?? false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Any, 0);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
    {
        _client = new TcpClient(LocalEndPoint);
        await _client.ConnectAsync(endPoint, token).ConfigureAwait(false);
        if (_client.Connected)
        {
            if (_client.Client.LocalEndPoint is IPEndPoint localEndPoint)
            {
                LocalEndPoint = localEndPoint;
            }
        }
        return _client.Connected;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        var ret = false;
        if (_client != null)
        {
            var stream = _client.GetStream();
            await stream.WriteAsync(data, token).ConfigureAwait(false);
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default)
    {
        var len = 0;
        if (_client is { Connected: true })
        {
            var stream = _client.GetStream();
            len = await stream.ReadAsync(buffer, token).ConfigureAwait(false);
        }
        return len;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ValueTask CloseAsync()
    {
        if (_client != null)
        {
            _client.Close();
            _client = null;
        }
        return ValueTask.CompletedTask;
    }
}
