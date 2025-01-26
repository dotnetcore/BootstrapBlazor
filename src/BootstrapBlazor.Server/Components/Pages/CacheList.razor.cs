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

    private List<object>? _cacheList;

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
        CacheManager.Clear(key);
        UpdateCacheList();
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
        _cacheList = [.. CacheManager.Keys.OrderBy(i => i.ToString())];
    }

    private string GetValue(object key)
    {
        string ret = "-";
        if (CacheManager.TryGetValue(key, out object? value))
        {
            if (value is string stringValue)
            {
                ret = stringValue;
                return ret;
            }

            if (value is IEnumerable)
            {
                ret = $"{LambdaExtensions.ElementCount(value)}";
            }
            else
            {
                ret = value?.ToString() ?? "-";
            }
        }

        return ret;
    }
}
