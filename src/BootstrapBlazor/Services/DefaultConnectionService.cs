// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// 当前链接服务
/// </summary>
class DefaultConnectionService : IConnectionService, IDisposable
{
    private readonly ConcurrentDictionary<string, CollectionItem> _connectionCache = new();

    private readonly CollectionHubOptions _options = default!;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public DefaultConnectionService(IOptions<BootstrapBlazorOptions> options)
    {
        _options = options.Value.CollectionHubOptions ?? new CollectionHubOptions();

        Task.Run(() =>
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    Task.Delay(_options.ExpirationScanFrequency, _cancellationTokenSource.Token);

                    var keys = _connectionCache.Values.Where(i => i.LastBeatTime.AddMilliseconds(_options.BeatInterval) < DateTimeOffset.Now).Select(i => i.Id).ToList();
                    keys.ForEach(i => _connectionCache.TryRemove(i, out _));
                }
                catch { }
            }
        });
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public long Count => _connectionCache.Values.LongCount(i => i.LastBeatTime.AddMilliseconds(_options.BeatInterval) > DateTimeOffset.Now);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="client"></param>
    public void AddOrUpdate(ClientInfo client)
    {
        if (!string.IsNullOrEmpty(client.Id))
        {
            _connectionCache.AddOrUpdate(client.Id, key => CreateItem(key, client), (k, v) => UpdateItem(v, client));
        }
    }

    private static CollectionItem CreateItem(string key, ClientInfo client)
    {
        return new CollectionItem()
        {
            Id = key,
            ConnectionTime = DateTimeOffset.Now,
            LastBeatTime = DateTimeOffset.Now,
            ClientInfo = client
        };
    }

    private static CollectionItem UpdateItem(CollectionItem item, ClientInfo val)
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
    /// <returns></returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out CollectionItem? value) => _connectionCache.TryGetValue(key, out value);

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
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
