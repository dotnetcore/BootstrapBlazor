// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// ICacheEntry 扩展类
/// </summary>
[ExcludeFromCodeCoverage]
internal static class ICacheEntryExtensions
{
    /// <summary>
    /// 设置滑动过期时间
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="offset">默认 null 内部设置为 10 秒</param>
    /// <returns></returns>
    public static ICacheEntry SetSlidingExpirationForDynamicAssembly(this ICacheEntry entry, TimeSpan? offset = null)
    {
        entry.SlidingExpiration = offset ?? TimeSpan.FromSeconds(10);
        return entry;
    }

    /// <summary>
    /// 设置 动态程序集滑动过期时间 10 秒
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="type"></param>
    public static void SetDynamicAssemblyPolicy(this ICacheEntry entry, Type? type)
    {
        if (type?.Assembly.IsDynamic ?? false)
        {
            entry.SetSlidingExpiration(TimeSpan.FromSeconds(10));
        }
    }
}
