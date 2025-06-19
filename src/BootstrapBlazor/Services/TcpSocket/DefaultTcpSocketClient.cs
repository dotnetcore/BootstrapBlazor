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
    private IDataPackageHandler? _dataPackageHandler;
    private CancellationTokenSource? _receiveCancellationTokenSource;

    public bool IsConnected => _client?.Connected ?? false;

    public IPEndPoint LocalEndPoint { get; }

    public ILogger<DefaultTcpSocketClient>? Logger { get; set; }

    public int ReceiveBufferSize { get; set; } = 1024 * 10;

    public DefaultTcpSocketClient(string host, int port = 0)
    {
        LocalEndPoint = new IPEndPoint(GetIPAddress(host), port);
    }

    private static IPAddress GetIPAddress(string host) => host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
        ? IPAddress.Loopback
        : IPAddress.TryParse(host, out var ip) ? ip : Dns.GetHostAddresses(host).FirstOrDefault() ?? IPAddress.Loopback;

    public void SetDataHandler(IDataPackageHandler handler)
    {
        _dataPackageHandler = handler;
    }

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
            // 释放资源
            Close();

            // 创建新的 TcpClient 实例
            _client ??= new TcpClient(LocalEndPoint);
            await _client.ConnectAsync(endPoint, token);

            // 开始接收数据
            _ = Task.Run(ReceiveAsync, token);
            ret = true;
        }
        catch (OperationCanceledException ex)
        {
            LogWarning(ex, $"TCP Socket connect operation was canceled to {endPoint}");
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
            if (_dataPackageHandler != null)
            {
                data = await _dataPackageHandler.SendAsync(data);
            }
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

    private async Task ReceiveAsync()
    {
        using var block = MemoryPool<byte>.Shared.Rent(ReceiveBufferSize);
        var buffer = block.Memory;
        _receiveCancellationTokenSource ??= new();
        while (_receiveCancellationTokenSource is { IsCancellationRequested: false })
        {
            if (_client is not { Connected: true })
            {
                throw new InvalidOperationException("TCP Socket is not connected.");
            }

            try
            {
                var stream = _client.GetStream();
                var len = await stream.ReadAsync(buffer, _receiveCancellationTokenSource.Token);
                if (len == 0)
                {
                    // 远端主机关闭链路
                    LogInformation($"TCP Socket received {len} data from {_client.Client.RemoteEndPoint}");
                    break;
                }
                else
                {
                    buffer = buffer[..len];

                    if (_dataPackageHandler != null)
                    {
                        await _dataPackageHandler.ReceiveAsync(buffer);
                    }
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
        }
    }

    public void Close()
    {
        Dispose(true);
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
            // 取消接收数据的任务
            if (_receiveCancellationTokenSource is not null)
            {
                _receiveCancellationTokenSource.Cancel();
                _receiveCancellationTokenSource.Dispose();
                _receiveCancellationTokenSource = null;
            }

            // 释放 TcpClient 资源
            if (_client != null)
            {
                _client.Close();
                _client = null;
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
