// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// CacheManager 接口类
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory);

    /// <summary>
    /// 获取指定键值
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryGetValue<TItem>(object key, [NotNullWhen(true)] out TItem? value);

    /// <summary>
    /// 设置 App 开始时间
    /// </summary>
    void SetStartTime();

    /// <summary>
    /// 获取 App 开始时间
    /// </summary>
    /// <returns></returns>
    DateTimeOffset GetStartTime();

    /// <summary>
    /// 通过指定 key 清除缓存方法
    /// </summary>
    /// <param name="key"></param>
    void Clear(object? key = null);

    /// <summary>
    /// 获得 缓存数量
    /// </summary>
    long Count { get; }

#if NET9_0_OR_GREATER
    /// <summary>
    /// 获得 缓存键集合
    /// </summary>
    IEnumerable<object> Keys { get; }

    /// <summary>
    /// 通过指定 key 获取缓存项 <see cref="ICacheEntry"/> 实例
    /// </summary>
    /// <param name="key"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    bool TryGetCacheEntry(object? key, [NotNullWhen(true)] out ICacheEntry? entry);
#endif
}
