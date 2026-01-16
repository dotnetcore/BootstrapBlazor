// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">CacheManager 接口类</para>
/// <para lang="en">CacheManager Interface Class</para>
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
    /// <para lang="zh">获取指定键值</para>
    /// <para lang="en">Get specified key value</para>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryGetValue<TItem>(object key, [NotNullWhen(true)] out TItem? value);

    /// <summary>
    /// <para lang="zh">设置 App 开始时间</para>
    /// <para lang="en">Set App Start Time</para>
    /// </summary>
    void SetStartTime();

    /// <summary>
    /// <para lang="zh">获取 App 开始时间</para>
    /// <para lang="en">Get App Start Time</para>
    /// </summary>
    /// <returns></returns>
    DateTimeOffset GetStartTime();

    /// <summary>
    /// <para lang="zh">通过指定 key 清除缓存方法</para>
    /// <para lang="en">Clear cache method by specified key</para>
    /// </summary>
    /// <param name="key"></param>
    void Clear(object? key = null);

    /// <summary>
    /// <para lang="zh">获得 缓存数量</para>
    /// <para lang="en">Get Cache Count</para>
    /// </summary>
    long Count { get; }

#if NET9_0_OR_GREATER
    /// <summary>
    /// <para lang="zh">获得 缓存键集合</para>
    /// <para lang="en">Get Cache Keys</para>
    /// </summary>
    IEnumerable<object> Keys { get; }

    /// <summary>
    /// <para lang="zh">通过指定 key 获取缓存项 <see cref="ICacheEntry"/> 实例</para>
    /// <para lang="en">Get <see cref="ICacheEntry"/> instance by specified key</para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    bool TryGetCacheEntry(object? key, [NotNullWhen(true)] out ICacheEntry? entry);
#endif
}
