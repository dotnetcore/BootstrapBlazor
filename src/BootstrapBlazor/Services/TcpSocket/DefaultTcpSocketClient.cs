// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
class DefaultTcpSocketClient : ITcpSocketClient
{
    private TcpClient? _client;

    public bool IsConnected => _client?.Connected ?? false;

    public IPEndPoint LocalEndPoint { get; }

    public ILogger<DefaultTcpSocketClient>? Logger { get; set; }

    public DefaultTcpSocketClient(string host, int port = 0)
    {
        LocalEndPoint = new IPEndPoint(GetIPAddress(host), port);
    }

    private static IPAddress GetIPAddress(string host) => host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
        ? IPAddress.Loopback
        : IPAddress.TryParse(host, out var ip) ? ip : Dns.GetHostAddresses(host).FirstOrDefault() ?? IPAddress.Loopback;

    public Task<bool> ConnectAsync(string host, int port, CancellationToken token = default)
    {
        var endPoint = new IPEndPoint(GetIPAddress(host), port);
        return ConnectAsync(endPoint, token);
    }

    public async Task<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
    {
        var ret = false;
        try
        {
            _client ??= new TcpClient();
            await _client.ConnectAsync(endPoint, token);
            ret = true;
        }
        catch (Exception ex)
        {
            LogError(ex, $"TCP Socket connection failed to {endPoint}");
        }
        return ret;
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
            LogWarning(ex, $"TCP Socket send operation was canceled to {_client.Client.RemoteEndPoint}");
        }
        catch (Exception ex)
        {
            LogError(ex, $"TCP Socket send failed to {_client.Client.RemoteEndPoint}");
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
                LogInformation($"TCP Socket received {len} data from {_client.Client.RemoteEndPoint}");
            }
            else
            {
                buffer = buffer[..len];
            }
        }
        catch (OperationCanceledException ex)
        {
            LogWarning(ex, $"TCP Socket receive operation was canceled to {_client.Client.RemoteEndPoint}");
        }
        catch (Exception ex)
        {
            LogError(ex, $"TCP Socket receive failed to {_client.Client.RemoteEndPoint}");
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(block);
        }
        return buffer;
    }

    private void LogInformation(string message)
    {
        Logger?.LogInformation("{message}", message);
    }

    private void LogWarning(Exception ex, string message)
    {
        Logger?.LogWarning(ex, "{message}", message);
    }

    private void LogError(Exception ex, string message)
    {
        Logger?.LogError(ex, "{message}", message);
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
                catch (Exception) { }
                finally
                {
                    _client = null;
                }
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
