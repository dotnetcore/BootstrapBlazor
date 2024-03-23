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

    public TimeSpan ExpirationScanFrequency { get; set; } = TimeSpan.FromSeconds(2);

    public long Count => _connectionCache.Values.LongCount(i => i.LastBeatTime.AddMilliseconds(Interval) > DateTimeOffset.Now);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Interval { get; set; } = 5000;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public DefaultConnectionService()
    {
        Task.Run(() =>
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    Task.Delay(ExpirationScanFrequency, _cancellationTokenSource.Token);

                    var keys = _connectionCache.Values.Where(i => i.LastBeatTime.AddMilliseconds(Interval) < DateTimeOffset.Now).Select(i => i.Id).ToList();
                    keys.ForEach(i => _connectionCache.TryRemove(i, out _));
                }
                catch { }
            }
        });
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    public void AddOrUpdate(string key) => _connectionCache.AddOrUpdate(key, CreateItem, UpdateItem);

    private static CollectionItem CreateItem(string key)
    {
        return new CollectionItem()
        {
            Id = key,
            ConnectionTime = DateTimeOffset.Now,
            LastBeatTime = DateTimeOffset.Now
        };
    }

    private static CollectionItem UpdateItem(string key, CollectionItem item)
    {
        item.LastBeatTime = DateTimeOffset.Now;
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
