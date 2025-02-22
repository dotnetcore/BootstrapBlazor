// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;
using System.Collections;

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// CacheManager 管理组件
/// </summary>
public partial class CacheList
{
    [Inject, NotNull]
    private ICacheManager? CacheManager { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<CacheList>? Localizer { get; set; }

    private List<object?> _cacheList = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        UpdateCacheList();
    }

    private void OnDelete(object key)
    {
        if (key is ICacheEntry entry)
        {
            CacheManager.Clear(entry.Key);
            UpdateCacheList();
        }
    }

    private void OnDeleteAll()
    {
        CacheManager.Clear();
        UpdateCacheList();
    }

    private void OnRefresh()
    {
        UpdateCacheList();
    }

    private void UpdateCacheList()
    {
        _cacheList = [.. CacheManager.Keys.OrderBy(i => i.ToString()).Select(key =>
        {
            ICacheEntry? entry = null;
            if (CacheManager.TryGetCacheEntry(key, out var val))
            {
                entry = val;
            }
            return entry;
        }).Where(i => i != null)];
    }

    private static string GetKey(object data) => data is ICacheEntry entry ? entry.Key.ToString()! : "-";

    private static string GetValue(object data) => data is ICacheEntry entry ? GetCacheEntryValue(entry) : "-";

    private static string GetCacheEntryValue(ICacheEntry entry)
    {
        var value = entry.Value;
        if (value is string stringValue)
        {
            return stringValue;
        }

        return value is IEnumerable ? $"{LambdaExtensions.ElementCount(value)}" : value?.ToString() ?? "-";
    }
}
