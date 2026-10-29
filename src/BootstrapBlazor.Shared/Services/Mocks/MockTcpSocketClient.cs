// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Longbow.TcpSocket;
using System.Net;
using System.Text;
using System.Threading.Channels;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">仅模拟网站 Socket 示例协议，不创建真实网络连接。</para>
/// <para lang="en">Simulates the site's Socket sample protocols without real network connections.</para>
/// </summary>
internal sealed class MockTcpSocketClient(TcpSocketClientOptions options, ILogger<MockTcpSocketClient> logger) : ITcpSocketClient
{
    private CancellationTokenSource? _connectionTokenSource;
    private Task _receiveTask = Task.CompletedTask;
    private Channel<ReadOnlyMemory<byte>> _requests = Channel.CreateUnbounded<ReadOnlyMemory<byte>>();
    private Channel<ReadOnlyMemory<byte>> _responses = Channel.CreateUnbounded<ReadOnlyMemory<byte>>();
    private ReadOnlyMemory<byte> _pendingResponse;
    private int _port;
    private bool _disposed;

    public bool IsConnected { get; private set; }

    public TcpSocketClientOptions Options { get; } = options;

    public IPEndPoint LocalEndPoint { get; private set; } = options.LocalEndPoint ?? new IPEndPoint(IPAddress.Loopback, 0);

    public Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallback { get; set; }

    public Func<Task>? OnConnecting { get; set; }

    public Func<Task>? OnConnected { get; set; }

    public async ValueTask<bool> ConnectAsync(IPEndPoint remoteEndPoint, CancellationToken token)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        token.ThrowIfCancellationRequested();
        if (!IPAddress.IsLoopback(remoteEndPoint.Address) || remoteEndPoint.Port is not (8800 or 8810 or 8900 or 8901))
        {
            throw new NotSupportedException("The browser Mock supports only the site's loopback Socket sample endpoints.");
        }
        await CloseAsync();
        _port = remoteEndPoint.Port;
        _requests = Channel.CreateUnbounded<ReadOnlyMemory<byte>>();
        _responses = Channel.CreateUnbounded<ReadOnlyMemory<byte>>();
        _pendingResponse = default;
        _connectionTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        var connectionToken = _connectionTokenSource.Token;
        LocalEndPoint = new IPEndPoint(Options.LocalEndPoint?.Address ?? IPAddress.Loopback,
            Options.LocalEndPoint is { Port: > 0 } endpoint ? endpoint.Port : Random.Shared.Next(49152, 65536));
        await NotifyConnectedAsync(connectionToken);
        connectionToken.ThrowIfCancellationRequested();
        _receiveTask = RunAsync(connectionToken);
        return true;
    }

    private async Task NotifyConnectedAsync(CancellationToken token)
    {
        if (OnConnecting != null)
        {
            await OnConnecting();
        }
        token.ThrowIfCancellationRequested();
        IsConnected = true;
        if (OnConnected != null)
        {
            await OnConnected();
        }
    }

    public async ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        token.ThrowIfCancellationRequested();
        if (!IsConnected)
        {
            throw new InvalidOperationException("Connect the mock Socket client before sending data.");
        }
        if (_port is 8810 or 8900)
        {
            await _requests.Writer.WriteAsync(data.ToArray(), token);
        }
        return true;
    }

    public ValueTask<bool> SendAsync(IList<ArraySegment<byte>> data, CancellationToken token)
    {
        var buffer = new byte[data.Sum(segment => segment.Count)];
        var offset = 0;
        foreach (var segment in data)
        {
            segment.AsSpan().CopyTo(buffer.AsSpan(offset));
            offset += segment.Count;
        }
        return SendAsync(buffer, token);
    }

    public async ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (Options.IsAutoReceive)
        {
            throw new InvalidOperationException("Disable automatic receiving before using ReceiveAsync.");
        }
        if (!IsConnected || _connectionTokenSource == null)
        {
            throw new InvalidOperationException("Connect the mock Socket client before receiving data.");
        }
        token.ThrowIfCancellationRequested();
        if (buffer.IsEmpty)
        {
            return 0;
        }
        using var source = CancellationTokenSource.CreateLinkedTokenSource(token, _connectionTokenSource.Token);
        if (_pendingResponse.IsEmpty)
        {
            _pendingResponse = await _responses.Reader.ReadAsync(source.Token);
        }
        var length = Math.Min(buffer.Length, _pendingResponse.Length);
        _pendingResponse[..length].CopyTo(buffer);
        _pendingResponse = _pendingResponse[length..];
        return length;
    }

    private async Task DeliverAsync(ReadOnlyMemory<byte> data, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        if (Options.IsAutoReceive)
        {
            var callback = ReceivedCallback;
            if (callback != null)
            {
                foreach (Func<ReadOnlyMemory<byte>, ValueTask> handler in callback.GetInvocationList())
                {
                    token.ThrowIfCancellationRequested();
                    await handler(data);
                }
            }
        }
        else
        {
            await _responses.Writer.WriteAsync(data, token);
        }
    }

    private async Task RunAsync(CancellationToken token)
    {
        try
        {
            if (_port is 8810 or 8900)
            {
                await foreach (var _ in _requests.Reader.ReadAllAsync(token))
                {
                    if (_port == 8810)
                    {
                        await DeliverAsync(Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), token);
                    }
                    else
                    {
                        // 与 Server 相同，分两次交付 4 字节头和 8 字节体，由真实适配器组包。
                        // Match Server fragmentation; the real adapter assembles the header and body.
                        await DeliverAsync("2025"u8.ToArray(), token);
                        await Task.Delay(40, token);
                        await DeliverAsync(Encoding.UTF8.GetBytes(DateTime.Now.ToString("ddHHmmss")), token);
                    }
                }
            }
            else
            {
                do
                {
                    await DeliverAsync(Encoding.UTF8.GetBytes(DateTime.Now.ToString(
                        _port == 8901 ? "yyyyMMddHHmmss" : "yyyy-MM-dd HH:mm:ss")), token);
                    await Task.Delay(_port == 8901 ? 2000 : 10000, token);
                    if (_port == 8901)
                    {
                        IsConnected = false;
                        await DeliverAsync(ReadOnlyMemory<byte>.Empty, token);
                        if (!Options.IsAutoReconnect)
                        {
                            break;
                        }
                        await Task.Delay(Math.Max(1, Options.ReconnectInterval), token);
                        await NotifyConnectedAsync(token);
                    }
                }
                while (!token.IsCancellationRequested);
            }
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            IsConnected = false;
            _responses.Writer.TryComplete(ex);
            logger.LogError(ex, "Browser Socket mock failed on sample port {Port}.", _port);
            throw;
        }
    }

    public async ValueTask CloseAsync()
    {
        if (_connectionTokenSource != null)
        {
            _connectionTokenSource.Cancel();
            try
            {
                await _receiveTask;
            }
            finally
            {
                _connectionTokenSource.Dispose();
                _connectionTokenSource = null;
                IsConnected = false;
                _pendingResponse = default;
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        _disposed = true;
        ReceivedCallback = null;
        OnConnecting = null;
        OnConnected = null;
        await CloseAsync();
    }
}
