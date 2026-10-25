// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

sealed class DefaultConnectionService : IConnectionService, IAsyncDisposable
{
    private readonly SignalRConnection _connection;
    private readonly ILogger<DefaultConnectionService> _logger;
    private readonly CancellationTokenSource _lifetime = new();
    private readonly Task _pollingTask;
    private ConnectionSnapshot _snapshot = new();
    private int _disposed;

    public DefaultConnectionService(SignalRConnection connection, ILogger<DefaultConnectionService> logger)
    {
        _connection = connection;
        _logger = logger;
        _connection.Retain();
        _pollingTask = PollAsync(_lifetime.Token);
    }

    public long Count => _snapshot.Count;

    public ICollection<ConnectionItem> Connections => _snapshot.Connections;

    public bool TryGetValue(string key, [NotNullWhen(true)] out ConnectionItem? value)
    {
        value = _snapshot.Connections.FirstOrDefault(item => item.Id == key);
        return value != null;
    }

    public async Task AddOrUpdateAsync(ClientInfo client, CancellationToken cancellationToken = default)
    {
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _lifetime.Token);
        if (!await _connection.InvokeAsync<bool>("Heartbeat", client, cancellation.Token))
        {
            _logger.LogWarning("Connection heartbeats are disabled on the server.");
        }
    }

    private async Task PollAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _snapshot = await _connection.InvokeAsync<ConnectionSnapshot>("GetConnectionSnapshot", null, cancellationToken)
                        ?? throw new InvalidOperationException("The connection snapshot must not be null.");
                }
                catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogError(ex, "Unable to refresh the connection snapshot.");
                }
                await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
        {
            return;
        }

        _lifetime.Cancel();
        try
        {
            await _pollingTask;
        }
        finally
        {
            try
            {
                await _connection.ReleaseAsync();
            }
            finally
            {
                _lifetime.Dispose();
            }
        }
    }
}
