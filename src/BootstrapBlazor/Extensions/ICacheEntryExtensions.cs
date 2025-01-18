// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// ICacheEntry 扩展类
/// </summary>
public static class ICacheEntryExtensions
{
    /// <summary>
    /// 设置 动态程序集滑动过期时间 10 秒
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="type"></param>
    /// <param name="offset">默认 null 内部设置为 10 秒</param>
    public static void SetSlidingExpirationByType(this ICacheEntry entry, Type type, TimeSpan? offset = null)
    {
        offset ??= type.Assembly.IsDynamic ? TimeSpan.FromSeconds(10) : TimeSpan.FromMinutes(5);
        entry.SlidingExpiration = offset.Value;
    }
}
