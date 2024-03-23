// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// 当前链接服务
/// </summary>
class DefaultConnectionService() : IConnectionService
{
    private readonly ConcurrentDictionary<string, CollectionItem> _connectionCache = new();

    public long Count => _connectionCache.Values.LongCount(i => i.LastBeatTime.HasValue && i.LastBeatTime.Value.AddMilliseconds(Interval) > DateTimeOffset.Now);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Interval { get; set; } = 5000;

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
}
