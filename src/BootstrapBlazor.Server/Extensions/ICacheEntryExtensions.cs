// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace BootstrapBlazor.Server.Extensions;

/// <summary>
/// <see cref="ICacheEntry"/> 扩展方法
/// </summary>
public static class ICacheEntryExtensions
{
    /// <summary>
    /// 获取缓存项过期时间
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static string GetExpiration(this ICacheEntry entry)
    {
        string? ret;
        if (entry.Priority == CacheItemPriority.NeverRemove)
        {
            ret = "Never Remove";
        }
        else if (entry.SlidingExpiration.HasValue)
        {
            ret = $"Sliding: {entry.GetSlidingLeftTime().TotalSeconds:###}/{entry.SlidingExpiration.Value.TotalSeconds}";
        }
        else if (entry.AbsoluteExpiration.HasValue)
        {
            ret = $"Absolute: {entry.AbsoluteExpiration.Value}";
        }
        else if (entry.ExpirationTokens.Count != 0)
        {
            ret = $"Token: {entry.ExpirationTokens.Count}";
        }
        else
        {
            ret = "Not Set";
        }
        return ret;
    }

    private static TimeSpan GetSlidingLeftTime(this ICacheEntry entry)
    {
        var ret = TimeSpan.Zero;
        var lastAccessed = entry.GetLastAccessed();
        if (lastAccessed != null)
        {
            ret = entry.SlidingExpiration!.Value - (DateTime.UtcNow - lastAccessed.Value);
        }
        return ret;
    }
}
