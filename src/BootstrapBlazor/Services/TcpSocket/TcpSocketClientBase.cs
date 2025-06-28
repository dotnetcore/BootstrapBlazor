// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base implementation for a TCP socket client, enabling connection, data transmission, and reception over
/// TCP.
/// </summary>
/// <remarks>This abstract class serves as a foundation for implementing TCP socket clients. It provides methods
/// for connecting to a remote endpoint, sending and receiving data, and managing connection state. Derived classes can
/// extend or customize the behavior as needed.</remarks>
public abstract class TcpSocketClientBase<TSocketClient> : ITcpSocketClient where TSocketClient : class, ISocketClient
{
    /// <summary>
    /// Gets or sets the underlying socket client used for network communication.
    /// </summary>
    protected TSocketClient? Client { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ILogger? Logger { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsConnected => Client?.IsConnected ?? false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Loopback, 0);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int ReceiveBufferSize { get; set; } = 1024 * 64;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsAutoReceive { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int ConnectTimeout { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int SendTimeout { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int ReceiveTimeout { get; set; }

    /// <summary>
    /// Gets or sets the handler responsible for processing data packages.
    /// </summary>
    public IDataPackageHandler? DataPackageHandler { get; protected set; }

    private IPEndPoint? _remoteEndPoint;
    private IPEndPoint? _localEndPoint;
    private CancellationTokenSource? _receiveCancellationTokenSource;

    /// <summary>
    /// Creates and initializes a new instance of the socket client for the specified endpoint.
    /// </summary>
    /// <param name="localEndPoint">The network endpoint to which the socket client will connect. Cannot be null.</param>
    /// <returns>An instance of <typeparamref name="TSocketClient"/> configured for the specified endpoint.</returns>
    protected abstract TSocketClient CreateSocketClient(IPEndPoint localEndPoint);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void SetDataHandler(IDataPackageHandler handler)
    {
        DataPackageHandler = handler;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="endPoint"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
    {
        var ret = false;
        try
        {
            // 释放资源
            await CloseAsync();

            // 创建新的 TcpClient 实例
            Client ??= CreateSocketClient(LocalEndPoint);
            _localEndPoint = LocalEndPoint;
            _remoteEndPoint = null;

            var connectionToken = token;
            if (ConnectTimeout > 0)
            {
                // 设置连接超时时间
                var connectTokenSource = new CancellationTokenSource(ConnectTimeout);
                connectionToken = CancellationTokenSource.CreateLinkedTokenSource(token, connectTokenSource.Token).Token;
            }
            await Client.ConnectAsync(endPoint, connectionToken);

            if (Client.IsConnected)
            {
                _localEndPoint = Client.LocalEndPoint;
                _remoteEndPoint = endPoint;

                if (IsAutoReceive)
                {
                    _ = Task.Run(AutoReceiveAsync, token);
                }
            }
            ret = Client.IsConnected;
        }
        catch (OperationCanceledException ex)
        {
            var message = token.IsCancellationRequested
                ? $"TCP Socket connect operation was canceled from {LocalEndPoint} to {endPoint}"
                : $"TCP Socket connect operation timed out from {LocalEndPoint} to {endPoint}";
            Log(LogLevel.Warning, ex, message);
        }
        catch (Exception ex)
        {
            Log(LogLevel.Error, ex, $"TCP Socket connection failed from {LocalEndPoint} to {endPoint}");
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        if (Client is not { IsConnected: true })
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
        }

        var ret = false;
        try
        {
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

            ret = await Client.SendAsync(data, sendToken);
        }
        catch (OperationCanceledException ex)
        {
            Log(LogLevel.Warning, ex, token.IsCancellationRequested
                ? $"TCP Socket send operation was canceled from {_localEndPoint} to {_remoteEndPoint}"
                : $"TCP Socket send operation timed out from {_localEndPoint} to {_remoteEndPoint}");
        }
        catch (Exception ex)
        {
            Log(LogLevel.Error, ex, $"TCP Socket send failed from {_localEndPoint} to {_remoteEndPoint}");
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async ValueTask<Memory<byte>> ReceiveAsync(CancellationToken token = default)
    {
        if (Client is not { IsConnected: true })
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
        }

        if (IsAutoReceive)
        {
            throw new InvalidOperationException("Cannot call ReceiveAsync when IsAutoReceive is true. Use the auto-receive mechanism instead.");
        }

        using var block = MemoryPool<byte>.Shared.Rent(ReceiveBufferSize);
        var buffer = block.Memory;
        var len = await ReceiveCoreAsync(Client, buffer, token);
        return buffer[..len];
    }

    private async ValueTask AutoReceiveAsync()
    {
        _receiveCancellationTokenSource ??= new();
        while (_receiveCancellationTokenSource is { IsCancellationRequested: false })
        {
            if (Client is not { IsConnected: true })
            {
                throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
            }

            using var block = MemoryPool<byte>.Shared.Rent(ReceiveBufferSize);
            var buffer = block.Memory;
            var len = await ReceiveCoreAsync(Client, buffer, _receiveCancellationTokenSource.Token);
            if (len == 0)
            {
                break;
            }
        }
    }

    private async ValueTask<int> ReceiveCoreAsync(ISocketClient client, Memory<byte> buffer, CancellationToken token)
    {
        var len = 0;
        try
        {
            var receiveToken = token;
            if (ReceiveTimeout > 0)
            {
                // 设置接收超时时间
                var receiveTokenSource = new CancellationTokenSource(ReceiveTimeout);
                receiveToken = CancellationTokenSource.CreateLinkedTokenSource(receiveToken, receiveTokenSource.Token).Token;
            }

            len = await client.ReceiveAsync(buffer, receiveToken);
            if (len == 0)
            {
                // 远端主机关闭链路
                Log(LogLevel.Information, null, $"TCP Socket {_localEndPoint} received 0 data closed by {_remoteEndPoint}");
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
            Log(LogLevel.Warning, ex, token.IsCancellationRequested
                ? $"TCP Socket receive operation canceled from {_localEndPoint} to {_remoteEndPoint}"
                : $"TCP Socket receive operation timed out from {_localEndPoint} to {_remoteEndPoint}");
        }
        catch (Exception ex)
        {
            Log(LogLevel.Error, ex, $"TCP Socket receive failed from {_localEndPoint} to {_remoteEndPoint}");
        }
        return len;
    }

    /// <summary>
    /// Logs a message with the specified log level, exception, and additional context.
    /// </summary>
    protected void Log(LogLevel logLevel, Exception? ex, string? message)
    {
        Logger?.Log(logLevel, ex, "{Message}", message);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual ValueTask CloseAsync()
    {
        return DisposeAsync(true);
    }

    /// <summary>
    /// Releases the resources used by the current instance of the class.
    /// </summary>
    /// <remarks>This method is called to free both managed and unmanaged resources. If the <paramref
    /// name="disposing"/> parameter is <see langword="true"/>, the method releases managed resources in addition to
    /// unmanaged resources. Override this method in a derived class to provide custom cleanup logic.</remarks>
    /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only
    /// unmanaged resources.</param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            // 取消接收数据的任务
            if (_receiveCancellationTokenSource != null)
            {
                _receiveCancellationTokenSource.Cancel();
                _receiveCancellationTokenSource.Dispose();
                _receiveCancellationTokenSource = null;
            }

            if (Client != null)
            {
                await Client.CloseAsync();
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
