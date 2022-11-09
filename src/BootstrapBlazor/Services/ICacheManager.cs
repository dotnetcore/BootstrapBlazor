// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 获取 App 开始时间
    /// </summary>
    void SetStartTime();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    DateTimeOffset GetStartTime();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    void Clear(string? key = null);
}
