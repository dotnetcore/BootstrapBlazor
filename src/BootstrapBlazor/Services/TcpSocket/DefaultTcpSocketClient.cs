// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Net;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
class DefaultTcpSocketClient(SocketClientOptions options) : ITcpSocketClient
{
    /// <summary>
    /// Gets or sets the socket client provider used for managing socket connections.
    /// </summary>
    private ISocketClientProvider? SocketClientProvider { get; set; }

    /// <summary>
    /// Gets or sets the logger instance used for logging messages and events.
    /// </summary>
    private ILogger? Logger { get; set; }

    /// <summary>
    /// Gets or sets the service provider used to resolve dependencies.
    /// </summary>
    public IServiceProvider? ServiceProvider { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public SocketClientOptions Options => options;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsConnected => SocketClientProvider?.IsConnected ?? false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IPEndPoint LocalEndPoint => SocketClientProvider?.LocalEndPoint ?? new IPEndPoint(IPAddress.Any, 0);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<Task>? OnConnecting { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<Task>? OnConnected { get; set; }

    private IPEndPoint? _remoteEndPoint;
    private IPEndPoint? _localEndPoint;
    private CancellationTokenSource? _receiveCancellationTokenSource;
    private CancellationTokenSource? _autoConnectTokenSource;

    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="endPoint"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
    {
        if (IsConnected)
        {
            return true;
        }

        var connectionToken = GenerateConnectionToken(token);
        try
        {
            await _semaphoreSlim.WaitAsync(connectionToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // 如果信号量等待被取消，则直接返回 IsConnected
            // 不管是超时还是被取消，都不需要重连，肯定有其他线程在连接中
            return IsConnected;
        }

        if (IsConnected)
        {
            _semaphoreSlim.Release();
            return true;
        }

        var reconnect = true;
        var ret = false;
        SocketClientProvider = ServiceProvider?.GetRequiredService<ISocketClientProvider>()
            ?? throw new InvalidOperationException("SocketClientProvider is not registered in the service provider.");

        try
        {
            if (OnConnecting != null)
            {
                await OnConnecting();
            }
            ret = await ConnectCoreAsync(SocketClientProvider, endPoint, connectionToken);
            if (OnConnected != null)
            {
                await OnConnected();
            }
        }
        catch (OperationCanceledException ex)
        {
            if (token.IsCancellationRequested)
            {
                Log(LogLevel.Warning, ex, $"TCP Socket connect operation was canceled from {LocalEndPoint} to {endPoint}");
                reconnect = false;
            }
            else
            {
                Log(LogLevel.Warning, ex, $"TCP Socket connect operation timed out from {LocalEndPoint} to {endPoint}");
            }
        }
        catch (Exception ex)
        {
            Log(LogLevel.Error, ex, $"TCP Socket connection failed from {LocalEndPoint} to {endPoint}");
        }

        // 释放信号量
        _semaphoreSlim.Release();

        if (reconnect)
        {
            _autoConnectTokenSource = new();

            if (!ret)
            {
                Reconnect();
            }
        }
        return ret;
    }

    private void Reconnect()
    {
        if (_autoConnectTokenSource != null && options.IsAutoReconnect && _remoteEndPoint != null)
        {
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(options.ReconnectInterval, _autoConnectTokenSource.Token).ConfigureAwait(false);
                    await ConnectAsync(_remoteEndPoint, _autoConnectTokenSource.Token).ConfigureAwait(false);
                }
                catch { }
            }, CancellationToken.None).ConfigureAwait(false);
        }
    }

    private async ValueTask<bool> ConnectCoreAsync(ISocketClientProvider provider, IPEndPoint endPoint, CancellationToken token)
    {
        // 释放资源
        await CloseCoreAsync();

        // 创建新的 TcpClient 实例
        provider.LocalEndPoint = Options.LocalEndPoint;

        _localEndPoint = Options.LocalEndPoint;
        _remoteEndPoint = endPoint;

        var ret = await provider.ConnectAsync(endPoint, token);

        if (ret)
        {
            _localEndPoint = provider.LocalEndPoint;

            if (options.IsAutoReceive)
            {
                _ = Task.Run(AutoReceiveAsync, CancellationToken.None).ConfigureAwait(false);
            }
        }
        return ret;
    }

    private CancellationToken GenerateConnectionToken(CancellationToken token)
    {
        var connectionToken = token;
        if (Options.ConnectTimeout > 0)
        {
            // 设置连接超时时间
            var connectTokenSource = new CancellationTokenSource(options.ConnectTimeout);
            connectionToken = CancellationTokenSource.CreateLinkedTokenSource(token, connectTokenSource.Token).Token;
        }
        return connectionToken;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        if (SocketClientProvider is not { IsConnected: true })
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
        }

        var ret = false;
        var reconnect = true;
        try
        {
            var sendToken = token;
            if (options.SendTimeout > 0)
            {
                // 设置发送超时时间
                var sendTokenSource = new CancellationTokenSource(options.SendTimeout);
                sendToken = CancellationTokenSource.CreateLinkedTokenSource(token, sendTokenSource.Token).Token;
            }
            ret = await SocketClientProvider.SendAsync(data, sendToken);
        }
        catch (OperationCanceledException ex)
        {
            if (token.IsCancellationRequested)
            {
                reconnect = false;
                Log(LogLevel.Warning, ex, $"TCP Socket send operation was canceled from {_localEndPoint} to {_remoteEndPoint}");
            }
            else
            {
                Log(LogLevel.Warning, ex, $"TCP Socket send operation timed out from {_localEndPoint} to {_remoteEndPoint}");
            }
        }
        catch (Exception ex)
        {
            Log(LogLevel.Error, ex, $"TCP Socket send failed from {_localEndPoint} to {_remoteEndPoint}");
        }

        Log(LogLevel.Information, null, $"Sending data from {_localEndPoint} to {_remoteEndPoint}, Data Length: {data.Length} Data Content: {BitConverter.ToString(data.ToArray())} Result: {ret}");

        if (!ret && reconnect)
        {
            // 如果发送失败并且需要重连则尝试重连
            Reconnect();
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async ValueTask<Memory<byte>> ReceiveAsync(CancellationToken token = default)
    {
        if (SocketClientProvider is not { IsConnected: true })
        {
            throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
        }

        if (options.IsAutoReceive)
        {
            throw new InvalidOperationException("Cannot call ReceiveAsync when IsAutoReceive is true. Use the auto-receive mechanism instead.");
        }

        using var block = MemoryPool<byte>.Shared.Rent(options.ReceiveBufferSize);
        var buffer = block.Memory;
        var len = await ReceiveCoreAsync(SocketClientProvider, buffer, token);
        if (len == 0)
        {
            Reconnect();
        }
        return buffer[..len];
    }

    private async ValueTask AutoReceiveAsync()
    {
        // 自动接收方法
        _receiveCancellationTokenSource ??= new();
        while (_receiveCancellationTokenSource is { IsCancellationRequested: false })
        {
            if (SocketClientProvider is not { IsConnected: true })
            {
                throw new InvalidOperationException($"TCP Socket is not connected {LocalEndPoint}");
            }

            using var block = MemoryPool<byte>.Shared.Rent(options.ReceiveBufferSize);
            var buffer = block.Memory;
            var len = await ReceiveCoreAsync(SocketClientProvider, buffer, _receiveCancellationTokenSource.Token);
            if (len == 0)
            {
                // 远端关闭或者 DisposeAsync 方法被调用时退出
                break;
            }
        }

        Reconnect();
    }

    private async ValueTask<int> ReceiveCoreAsync(ISocketClientProvider client, Memory<byte> buffer, CancellationToken token)
    {
        var reconnect = true;
        var len = 0;
        try
        {
            var receiveToken = token;
            if (options.ReceiveTimeout > 0)
            {
                // 设置接收超时时间
                var receiveTokenSource = new CancellationTokenSource(options.ReceiveTimeout);
                receiveToken = CancellationTokenSource.CreateLinkedTokenSource(receiveToken, receiveTokenSource.Token).Token;
            }

            len = await client.ReceiveAsync(buffer, receiveToken);
            if (len == 0)
            {
                // 远端主机关闭链路
                Log(LogLevel.Information, null, $"TCP Socket {_localEndPoint} received 0 data closed by {_remoteEndPoint}");
                buffer = Memory<byte>.Empty;
            }
            else
            {
                buffer = buffer[..len];
            }

            if (ReceivedCallBack != null)
            {
                // 如果订阅回调则触发回调
                await ReceivedCallBack(buffer);
            }
        }
        catch (OperationCanceledException ex)
        {
            if (token.IsCancellationRequested)
            {
                Log(LogLevel.Warning, ex, $"TCP Socket receive operation canceled from {_localEndPoint} to {_remoteEndPoint}");
                reconnect = false;
            }
            else
            {
                Log(LogLevel.Warning, ex, $"TCP Socket receive operation timed out from {_localEndPoint} to {_remoteEndPoint}");
            }
        }
        catch (Exception ex)
        {
            Log(LogLevel.Error, ex, $"TCP Socket receive failed from {_localEndPoint} to {_remoteEndPoint}");
        }

        Log(LogLevel.Information, null, $"Receiving data from {_localEndPoint} to {_remoteEndPoint}, Data Length: {len} Data Content: {BitConverter.ToString(buffer.ToArray())}");

        if (len == 0 && reconnect)
        {
            // 如果接收数据长度为 0 并且需要重连则尝试重连
            Reconnect();
        }
        return len;
    }

    /// <summary>
    /// Logs a message with the specified log level, exception, and additional context.
    /// </summary>
    private void Log(LogLevel logLevel, Exception? ex, string? message)
    {
        if (options.EnableLog)
        {
            Logger ??= ServiceProvider?.GetRequiredService<ILogger<DefaultTcpSocketClient>>();
            Logger?.Log(logLevel, ex, "{Message}", message);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask CloseAsync()
    {
        // 取消重连任务
        if (_autoConnectTokenSource != null)
        {
            _autoConnectTokenSource.Cancel();
            _autoConnectTokenSource.Dispose();
            _autoConnectTokenSource = null;
        }

        await CloseCoreAsync();
    }

    private async ValueTask CloseCoreAsync()
    {
        // 取消接收数据的任务
        if (_receiveCancellationTokenSource != null)
        {
            _receiveCancellationTokenSource.Cancel();
            _receiveCancellationTokenSource.Dispose();
            _receiveCancellationTokenSource = null;
        }

        if (SocketClientProvider != null)
        {
            await SocketClientProvider.CloseAsync();
        }
    }

    /// <summary>
    /// Releases the resources used by the current instance of the class.
    /// </summary>
    /// <remarks>This method is called to free both managed and unmanaged resources. If the <paramref
    /// name="disposing"/> parameter is <see langword="true"/>, the method releases managed resources in addition to
    /// unmanaged resources. Override this method in a derived class to provide custom cleanup logic.</remarks>
    /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only
    /// unmanaged resources.</param>
    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            await CloseAsync();
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
