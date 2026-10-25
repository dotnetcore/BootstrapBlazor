// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text.Json;

namespace BootstrapBlazor.Components;

sealed class SignalRDispatchConnection : IAsyncDisposable
{
    private readonly HubConnection _connection;
    private readonly ILogger<SignalRDispatchConnection> _logger;
    private readonly ConcurrentDictionary<string, Func<JsonElement, Task>> _subscriptions = new(StringComparer.Ordinal);
    private readonly SemaphoreSlim _gate = new(1, 1);
    private readonly CancellationTokenSource _lifetime = new();
    private readonly IDisposable _receiver;

    public SignalRDispatchConnection(NavigationManager navigation, ILogger<SignalRDispatchConnection> logger, string hubPath)
    {
        _logger = logger;
        _connection = new HubConnectionBuilder()
            .WithUrl(navigation.ToAbsoluteUri(hubPath))
            .WithAutomaticReconnect(new DispatchRetryPolicy())
            .Build();
        _receiver = _connection.On<string, JsonElement>("Receive", ReceiveAsync);
        _connection.Reconnected += RestoreSubscriptionsAsync;
    }

    public async Task SubscribeAsync(string entryType, Func<JsonElement, Task> callback, CancellationToken cancellationToken)
    {
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _lifetime.Token);
        var token = cancellation.Token;
        await _gate.WaitAsync(token);
        try
        {
            _subscriptions[entryType] = callback;
            await EnsureConnectedAsync(token);
            await _connection.InvokeAsync("Subscribe", entryType, token);
        }
        catch
        {
            _subscriptions.TryRemove(entryType, out _);
            throw;
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task UnsubscribeAsync(string entryType, CancellationToken cancellationToken)
    {
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _lifetime.Token);
        var token = cancellation.Token;
        await _gate.WaitAsync(token);
        try
        {
            _subscriptions.TryRemove(entryType, out _);
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.InvokeAsync("Unsubscribe", entryType, token);
            }
            if (_subscriptions.IsEmpty)
            {
                await _connection.StopAsync(token);
            }
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task DispatchAsync<TEntry>(string entryType, DispatchEntry<TEntry> entry, CancellationToken cancellationToken)
    {
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _lifetime.Token);
        var token = cancellation.Token;
        await _gate.WaitAsync(token);
        try
        {
            await EnsureConnectedAsync(token);
            await _connection.InvokeAsync("Dispatch", entryType, entry, token);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task EnsureConnectedAsync(CancellationToken cancellationToken)
    {
        if (_connection.State == HubConnectionState.Disconnected)
        {
            for (var attempt = 0; ; attempt++)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);
                    break;
                }
                catch (Exception ex) when (attempt < 2 && !cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning(ex, "Unable to connect to the dispatch Hub; retrying.");
                    await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
                }
            }

            foreach (var entryType in _subscriptions.Keys)
            {
                await _connection.InvokeAsync("Subscribe", entryType, cancellationToken);
            }
        }

        if (_connection.State != HubConnectionState.Connected)
        {
            throw new InvalidOperationException("The dispatch connection is reconnecting. Retry when it is connected.");
        }
    }

    private async Task RestoreSubscriptionsAsync(string? connectionId)
    {
        try
        {
            await _gate.WaitAsync(_lifetime.Token);
            try
            {
                foreach (var entryType in _subscriptions.Keys)
                {
                    await _connection.InvokeAsync("Subscribe", entryType, _lifetime.Token);
                }
            }
            finally
            {
                _gate.Release();
            }
        }
        catch (OperationCanceledException) when (_lifetime.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to restore dispatch subscriptions after reconnecting.");
        }
    }

    private async Task ReceiveAsync(string entryType, JsonElement entry)
    {
        if (_subscriptions.TryGetValue(entryType, out var callback))
        {
            try
            {
                await callback(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to receive a dispatch for entry type {EntryType}.", entryType);
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        _lifetime.Cancel();
        _connection.Reconnected -= RestoreSubscriptionsAsync;
        _receiver.Dispose();
        await _connection.DisposeAsync();
        _lifetime.Dispose();
    }

    private sealed class DispatchRetryPolicy : IRetryPolicy
    {
        public TimeSpan? NextRetryDelay(RetryContext retryContext)
            => TimeSpan.FromSeconds(Math.Min(30, retryContext.PreviousRetryCount * 3));
    }
}
