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
    public object? Key { get; set; }

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

        if (CacheManager.TryGetCacheEntry(Key, out ICacheEntry? entry))
        {
            if (entry.Priority == CacheItemPriority.NeverRemove)
            {
                ExpirationTime = "Never Remove";
            }
            else if (entry.SlidingExpiration.HasValue)
            {
                ExpirationTime = $"Sliding: {entry.SlidingExpiration.Value}";
            }
            else if (entry.AbsoluteExpiration.HasValue)
            {
                ExpirationTime = $"Absolute: {entry.AbsoluteExpiration.Value}";
            }
            else if (entry.ExpirationTokens.Count != 0)
            {
                ExpirationTime = $"Token: {entry.ExpirationTokens.Count}";
            }
            else
            {
                ExpirationTime = "Not Set";
            }
        }
        else
        {
            ExpirationTime = "Not Found";
        }
    }
}
