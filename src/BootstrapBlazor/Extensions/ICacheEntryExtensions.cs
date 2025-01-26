﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="ICacheEntry"/> 扩展方法
/// </summary>
public static class ICacheEntryExtensions
{
    /// <summary>
    /// 获得缓存项 <see cref="ICacheEntry"/> 最后访问时间
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static DateTime? GetLastAccessed(this ICacheEntry entry)
    {
        _lastAccessedProperty ??= entry.GetType().GetProperty("LastAccessed", BindingFlags.Instance | BindingFlags.NonPublic);

        DateTime? ret = null;
        if (_lastAccessedProperty != null)
        {
            var v = _lastAccessedProperty.GetValue(entry);
            if (v is DateTime val)
            {
                ret = val;
            }
        }
        return ret;
    }

    private static PropertyInfo? _lastAccessedProperty = null;
}
