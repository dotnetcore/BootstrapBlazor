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
sealed class DefaultTcpSocketClient(IPEndPoint endPoint) : ITcpSocketClient
{
    private TcpClient? _client;
    private IDataPackageHandler? _dataPackageHandler;
    private CancellationTokenSource? _receiveCancellationTokenSource;
    private IPEndPoint? _remoteEndPoint;

    public bool IsConnected => _client?.Connected ?? false;

    public IPEndPoint LocalEndPoint { get; set; } = endPoint;

    [NotNull]
    public ILogger<DefaultTcpSocketClient>? Logger { get; set; }

    public int ReceiveBufferSize { get; set; } = 1024 * 64;

    public bool IsAutoReceive { get; set; } = true;

    public Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    public int ConnectTimeout { get; set; }

    public int SendTimeout { get; set; }

    public int ReceiveTimeout { get; set; }

    public void SetDataHandler(IDataPackageHandler handler)
    {
        _dataPackageHandler = handler;
    }

    public async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
    {
        var ret = false;
        try
        {
            // 释放资源
            Close();

            // 创建新的 TcpClient 实例
            _client ??= new TcpClient(LocalEndPoint);

            var connectionToken = token;
            if (ConnectTimeout > 0)
            {
                // 设置连接超时时间
                var connectTokenSource = new CancellationTokenSource(ConnectTimeout);
                connectionToken = CancellationTokenSource.CreateLinkedTokenSource(token, connectTokenSource.Token).Token;
            }
            await _client.ConnectAsync(endPoint, connectionToken);

            // 设置本地以及远端端点信息
            LocalEndPoint = (IPEndPoint)_client.Client.LocalEndPoint!;
            _remoteEndPoint = endPoint;

            if (IsAutoReceive)
            {
                // 开始接收数据
                _ = Task.Run(ReceiveAsync, token);
            }
            ret = true;
        }
        catch (OperationCanceledException ex)
        {
            if (token.IsCancellationRequested)
            {
                Logger.LogWarning(ex, "TCP Socket connect operation was canceled from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, endPoint);
            }
            else
            {
                Logger.LogWarning(ex, "TCP Socket connect operation timed out from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, endPoint);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "TCP Socket connection failed from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, endPoint);
        }
        return ret;
    }

    public async ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        if (_client is not { Connected: true })
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
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
            Logger.LogWarning(ex, "TCP Socket send operation was canceled from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "TCP Socket send failed from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
        }
        return ret;
    }

    public async ValueTask<Memory<byte>> ReceiveAsync(CancellationToken token = default)
    {
        if (IsAutoReceive)
        {
            throw new InvalidOperationException("Cannot call ReceiveAsync when IsAutoReceive is true. Use the auto-receive mechanism instead.");
        }

        if (_client == null || !_client.Connected)
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
        }

        var ret = Memory<byte>.Empty;
        try
        {
            _receiveCancellationTokenSource ??= new();
            using var block = MemoryPool<byte>.Shared.Rent(ReceiveBufferSize);
            var buffer = block.Memory;
            var stream = _client.GetStream();
            var len = await stream.ReadAsync(buffer, _receiveCancellationTokenSource.Token);
            if (len == 0)
            {
                // 远端主机关闭链路
                Logger.LogInformation("TCP Socket {LocalEndPoint} received 0 data closed by {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
            }
            else
            {
                buffer = buffer[..len];

                if (ReceivedCallBack != null)
                {
                    await ReceivedCallBack(buffer);
                }

                if (_dataPackageHandler != null)
                {
                    await _dataPackageHandler.ReceiveAsync(buffer);
                }

                ret = buffer;
            }
        }
        catch (OperationCanceledException ex)
        {
            Logger.LogWarning(ex, "TCP Socket receive operation was canceled from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "TCP Socket receive failed from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
        }
        return ret;
    }

    private async ValueTask ReceiveAsync()
    {
        _receiveCancellationTokenSource ??= new();
        while (_receiveCancellationTokenSource is { IsCancellationRequested: false })
        {
            if (_client is not { Connected: true })
            {
                throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
            }

            try
            {
                using var block = MemoryPool<byte>.Shared.Rent(ReceiveBufferSize);
                var buffer = block.Memory;
                var stream = _client.GetStream();
                var len = await stream.ReadAsync(buffer, _receiveCancellationTokenSource.Token);
                if (len == 0)
                {
                    // 远端主机关闭链路
                    Logger.LogInformation("TCP Socket {LocalEndPoint} received 0 data closed by {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
                    break;
                }
                else
                {
                    buffer = buffer[..len];

                    if (ReceivedCallBack != null)
                    {
                        await ReceivedCallBack(buffer);
                    }

                    if (_dataPackageHandler != null)
                    {
                        await _dataPackageHandler.ReceiveAsync(buffer);
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                Logger.LogWarning(ex, "TCP Socket receive operation was canceled from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "TCP Socket receive failed from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
            }
        }
    }

    public void Close()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _remoteEndPoint = null;

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
