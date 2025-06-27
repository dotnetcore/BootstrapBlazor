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
sealed class DefaultTcpSocketClient(IPEndPoint localEndPoint) : TcpSocketClientBase
{
    private TcpClient? _client;
    private CancellationTokenSource? _receiveCancellationTokenSource;
    private IPEndPoint? _remoteEndPoint;

    public override bool IsConnected => _client?.Connected ?? false;

    [NotNull]
    public ILogger<DefaultTcpSocketClient>? Logger { get; set; }

    public override async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
    {
        var ret = false;
        try
        {
            // 释放资源
            await CloseAsync();

            // 创建新的 TcpClient 实例
            _client ??= new TcpClient(localEndPoint);

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
                _ = Task.Run(AutoReceiveAsync);
            }
            ret = _client.Connected;
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

    public override async ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        if (_client is not { Connected: true })
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
        }

        var ret = false;
        try
        {
            var stream = _client.GetStream();

            var sendToken = token;
            if (SendTimeout > 0)
            {
                // 设置发送超时时间
                var sendTokenSource = new CancellationTokenSource(SendTimeout);
                sendToken = CancellationTokenSource.CreateLinkedTokenSource(token, sendTokenSource.Token).Token;
            }

            if (DataPackageHandler != null)
            {
                data = await DataPackageHandler.SendAsync(data, sendToken);
            }

            await stream.WriteAsync(data, sendToken);
            ret = true;
        }
        catch (OperationCanceledException ex)
        {
            if (token.IsCancellationRequested)
            {
                Logger.LogWarning(ex, "TCP Socket send operation was canceled from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
            }
            else
            {
                Logger.LogWarning(ex, "TCP Socket send operation timed out from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "TCP Socket send failed from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
        }
        return ret;
    }

    public override async ValueTask<Memory<byte>> ReceiveAsync(CancellationToken token = default)
    {
        if (_client is not { Connected: true })
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
        }

        if (IsAutoReceive)
        {
            throw new InvalidOperationException("Cannot call ReceiveAsync when IsAutoReceive is true. Use the auto-receive mechanism instead.");
        }

        using var block = MemoryPool<byte>.Shared.Rent(ReceiveBufferSize);
        var buffer = block.Memory;
        var len = await ReceiveCoreAsync(_client, buffer, token);
        return buffer[0..len];
    }

    private async ValueTask AutoReceiveAsync()
    {
        _receiveCancellationTokenSource ??= new();
        while (_receiveCancellationTokenSource is { IsCancellationRequested: false })
        {
            if (_client is not { Connected: true })
            {
                throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
            }

            using var block = MemoryPool<byte>.Shared.Rent(ReceiveBufferSize);
            var buffer = block.Memory;
            var len = await ReceiveCoreAsync(_client, buffer, _receiveCancellationTokenSource.Token);
            if (len == 0)
            {
                break;
            }
        }
    }

    private async ValueTask<int> ReceiveCoreAsync(TcpClient client, Memory<byte> buffer, CancellationToken token)
    {
        var len = 0;
        try
        {
            var stream = client.GetStream();

            var receiveToken = token;
            if (ReceiveTimeout > 0)
            {
                // 设置接收超时时间
                var receiveTokenSource = new CancellationTokenSource(ReceiveTimeout);
                receiveToken = CancellationTokenSource.CreateLinkedTokenSource(receiveToken, receiveTokenSource.Token).Token;
            }
            len = await stream.ReadAsync(buffer, receiveToken);
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

                if (DataPackageHandler != null)
                {
                    await DataPackageHandler.ReceiveAsync(buffer, receiveToken);
                }
                len = buffer.Length;
            }
        }
        catch (OperationCanceledException ex)
        {
            if (token.IsCancellationRequested)
            {
                Logger.LogWarning(ex, "TCP Socket receive operation canceled from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
            }
            else
            {
                Logger.LogWarning(ex, "TCP Socket receive operation timed out from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "TCP Socket receive failed from {LocalEndPoint} to {RemoteEndPoint}", LocalEndPoint, _remoteEndPoint);
        }
        return len;
    }

    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            LocalEndPoint = null;
            _remoteEndPoint = null;

            // 取消接收数据的任务
            if (_receiveCancellationTokenSource != null)
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
}
