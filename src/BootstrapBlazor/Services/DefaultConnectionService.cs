// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">服务器端在线连接服务的默认实现</para>
/// <para lang="en">Default server-side online connection service.</para>
/// </summary>
class DefaultConnectionService : IConnectionService, IDisposable
{
    private readonly ConcurrentDictionary<string, ConnectionItem> _connectionCache = new();

    private readonly ConnectionHubOptions _options;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public DefaultConnectionService(IOptions<BootstrapBlazorOptions> options, ILogger<DefaultConnectionService> logger)
    {
        _options = options.Value.ConnectionHubOptions;
        if (OperatingSystem.IsBrowser())
        {
            // WebAssembly does not support querying server connection information
            return;
        }

        if (_options.Enable)
        {
            var token = _cancellationTokenSource.Token;
            Task.Run(async () =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        await Task.Delay(_options.ExpirationScanFrequency, token);

                        try
                        {
                            var keys = _connectionCache.Values.Where(i => i.LastBeatTime.Add(_options.TimeoutInterval) < DateTimeOffset.Now).Select(i => i.Id).ToList();
                            keys.ForEach(i => _connectionCache.TryRemove(i, out _));
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Unable to remove expired connections.");
                        }
                    }
                }
                catch (OperationCanceledException) when (token.IsCancellationRequested)
                {
                }
            });
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public long Count => _connectionCache.Values.LongCount(i => i.LastBeatTime.Add(_options.TimeoutInterval) > DateTimeOffset.Now);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    public Task AddOrUpdateAsync(ClientInfo client, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (!string.IsNullOrEmpty(client.Id))
        {
            _connectionCache.AddOrUpdate(client.Id, key => CreateItem(key, client), (k, v) => UpdateItem(v, client));
        }
        return Task.CompletedTask;
    }

    private static ConnectionItem CreateItem(string key, ClientInfo client) => new()
    {
        Id = key,
        ConnectionTime = DateTimeOffset.Now,
        LastBeatTime = DateTimeOffset.Now,
        ClientInfo = client
    };

    private static ConnectionItem UpdateItem(ConnectionItem item, ClientInfo val)
    {
        item.LastBeatTime = DateTimeOffset.Now;
        item.ClientInfo = val;
        return item;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public bool TryGetValue(string key, [NotNullWhen(true)] out ConnectionItem? value) => _connectionCache.TryGetValue(key, out value);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ICollection<ConnectionItem> Connections => _connectionCache.Values;

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            _cancellationTokenSource.Dispose();
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
