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
    private readonly ConcurrentDictionary<string, ConnectionItem> _connectionCache = new();

    private readonly ConnectionHubOptions _options;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public DefaultConnectionService(IOptions<BootstrapBlazorOptions> options)
    {
        _options = options.Value.ConnectionHubOptions;

        if (_options.Enable)
        {
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(_options.ExpirationScanFrequency, _cancellationTokenSource.Token);

                        var keys = _connectionCache.Values.Where(i => i.LastBeatTime.Add(_options.TimeoutInterval) < DateTimeOffset.Now).Select(i => i.Id).ToList();
                        keys.ForEach(i => _connectionCache.TryRemove(i, out _));
                    }
                    catch { }
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
    public void AddOrUpdate(ClientInfo client)
    {
        if (!string.IsNullOrEmpty(client.Id))
        {
            _connectionCache.AddOrUpdate(client.Id, key => CreateItem(key, client), (k, v) => UpdateItem(v, client));
        }
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
    /// <returns></returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out ConnectionItem? value) => _connectionCache.TryGetValue(key, out value);

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
