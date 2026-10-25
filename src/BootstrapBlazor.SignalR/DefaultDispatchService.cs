// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text.Json;

namespace BootstrapBlazor.Components;

sealed class DefaultDispatchService<TEntry>(
    SignalRConnection connection,
    ILogger<DefaultDispatchService<TEntry>> logger) : IDispatchService<TEntry>, IAsyncDisposable
{
    private static readonly string _entryType = typeof(TEntry).FullName ?? typeof(TEntry).Name;
    private static readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);
    private readonly List<Func<DispatchEntry<TEntry>, Task>> _callbacks = [];
    private readonly object _locker = new();
    private readonly SemaphoreSlim _subscriptions = new(1, 1);
    private readonly CancellationTokenSource _lifetime = new();

    public async Task DispatchAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default)
    {
        await connection.DispatchAsync(_entryType, entry, cancellationToken);
        await DispatchCoreAsync(entry, cancellationToken);
    }

    public async Task SubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default)
    {
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _lifetime.Token);
        var token = cancellation.Token;
        await _subscriptions.WaitAsync(token);
        try
        {
            bool first;
            lock (_locker)
            {
                first = _callbacks.Count == 0;
                _callbacks.Add(callback);
            }
            if (first)
            {
                try
                {
                    while (true)
                    {
                        token.ThrowIfCancellationRequested();
                        try
                        {
                            await connection.SubscribeAsync(_entryType, ReceiveAsync, token);
                            break;
                        }
                        catch (Exception ex) when (!token.IsCancellationRequested &&
                            (ex is HttpRequestException or IOException or WebSocketException or TimeoutException or OperationCanceledException ||
                             ex is InvalidOperationException && connection.IsReconnecting))
                        {
                            logger.LogWarning(ex, "Unable to subscribe to dispatch entry type {EntryType}; retrying.", _entryType);
                            await Task.Delay(TimeSpan.FromSeconds(3), token);
                        }
                    }
                }
                catch
                {
                    lock (_locker)
                    {
                        _callbacks.Remove(callback);
                    }
                    throw;
                }
            }
        }
        finally
        {
            _subscriptions.Release();
        }
    }

    public async Task UnSubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default)
    {
        await _subscriptions.WaitAsync(cancellationToken);
        try
        {
            bool last;
            lock (_locker)
            {
                last = _callbacks.Remove(callback) && _callbacks.Count == 0;
            }
            if (last)
            {
                await connection.UnsubscribeAsync(_entryType, cancellationToken);
            }
        }
        finally
        {
            _subscriptions.Release();
        }
    }

    private Task ReceiveAsync(JsonElement json)
        => DispatchCoreAsync(json.Deserialize<DispatchEntry<TEntry>>(_serializerOptions)
            ?? throw new JsonException("The dispatch entry must not be null."));

    private async Task DispatchCoreAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Func<DispatchEntry<TEntry>, Task>[] callbacks;
        lock (_locker)
        {
            callbacks = _callbacks.ToArray();
        }
        foreach (var callback in callbacks)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await callback(entry);
            }
            catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
            {
                logger.LogError(ex, "A dispatch subscriber failed for entry type {EntryType}.", _entryType);
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        _lifetime.Cancel();
        await _subscriptions.WaitAsync();
        try
        {
            bool subscribed;
            lock (_locker)
            {
                subscribed = _callbacks.Count > 0;
                _callbacks.Clear();
            }
            if (subscribed)
            {
                await connection.UnsubscribeAsync(_entryType, CancellationToken.None);
            }
        }
        finally
        {
            _subscriptions.Release();
        }
    }
}
