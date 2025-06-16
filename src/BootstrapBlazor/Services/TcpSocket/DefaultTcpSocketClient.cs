// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Net.Sockets;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
class DefaultTcpSocketClient(string host, int port) : ITcpSocketClient
{
    private TcpClient? _client;

    public bool IsConnected => _client?.Connected ?? false;

    public ILogger? Logger { get; set; }

    public async Task<bool> ConnectAsync(string host, int port, CancellationToken token = default)
    {
        var ret = false;
        try
        {
            _client ??= new TcpClient();
            await _client.ConnectAsync(host, port, token);
            ret = true;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "TCP Socket connection failed to {Host}:{Port}", host, port);
        }
        return ret;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // 释放托管资源
            if (_client != null)
            {
                try
                {
                    _client.Close();
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Error closing TCP Socket connection to {Host}:{Port}", host, port);
                }
                finally
                {
                    _client = null;
                }
            }
        }
    }

    public async Task<bool> SendAsync(Memory<byte> data, CancellationToken token = default)
    {
        if (_client is not { Connected: true })
        {
            throw new InvalidOperationException("TCP Socket is not connected.");
        }

        var ret = false;
        try
        {
            var stream = _client.GetStream();
            await stream.WriteAsync(data, token);
            ret = true;
        }
        catch (OperationCanceledException ex)
        {
            Logger?.LogWarning(ex, "TCP Socket send operation was canceled to {Host}:{Port}", host, port);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "TCP Socket send failed to {Host}:{Port}", host, port);
        }
        return ret;
    }

    public async Task<Memory<byte>> ReceiveAsync(int bufferSize = 1024 * 10, CancellationToken token = default)
    {
        if (_client is not { Connected: true })
        {
            throw new InvalidOperationException("TCP Socket is not connected.");
        }

        var block = ArrayPool<byte>.Shared.Rent(bufferSize);
        var buffer = new Memory<byte>(block);
        try
        {
            var stream = _client.GetStream();
            var len = await stream.ReadAsync(buffer, token);
            if (len == 0)
            {
                Logger?.LogInformation("TCP Socket received {len} data from {Host}:{Port}", len, host, port);
            }
            else
            {
                buffer = buffer[..len];
            }
        }
        catch (OperationCanceledException ex)
        {
            Logger?.LogWarning(ex, "TCP Socket receive operation was canceled to {Host}:{Port}", host, port);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "TCP Socket receive failed to {Host}:{Port}", host, port);
        }
        ArrayPool<byte>.Shared.Return(block);
        return buffer;
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
