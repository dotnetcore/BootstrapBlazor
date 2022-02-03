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
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    T GetOrCreate<T>(object key, Func<ICacheEntry, T> factory);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    Task<T> GetOrCreateAsync<T>(object key, Func<ICacheEntry, Task<T>> factory);

    /// <summary>
    /// 获取 App 开始时间
    /// </summary>
    void SetStartTime();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    DateTimeOffset GetStartTime();
}
