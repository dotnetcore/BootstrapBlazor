// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// CacaheExpiration 组件
/// </summary>
public partial class CacaheExpiration
{
    [Inject, NotNull]
    private ICacheManager? CacheManager { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="TableColumnContext{TItem, TValue}"/> 实例
    /// </summary>
    [Parameter, NotNull]
    public TableColumnContext<object, object>? TableColumnContext { get; set; }

    private string? ExpirationTime { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await GetCacheEntryExpiration();
    }

    private async Task GetCacheEntryExpiration()
    {
        ExpirationTime = "loading ...";
        await Task.Yield();

        var key = TableColumnContext.Row;
        ExpirationTime = CacheManager.TryGetCacheEntry(key, out ICacheEntry? entry)
            ? entry.GetExpiration()
            : "Not Found";
    }
}
