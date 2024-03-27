// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

class DefaultThrottleDispatcherFactory : IThrottleDispatcherFactory
{
    private readonly ConcurrentDictionary<string, ThrottleDispatcher> _cache = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="options"></param>
    public ThrottleDispatcher GetOrCreate(string key, ThrottleOptions? options = null) => _cache.GetOrAdd(key, key => new ThrottleDispatcher(options ?? new()));

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    public void Clear(string? key = null)
    {
        if (string.IsNullOrEmpty(key))
        {
            _cache.Clear();
        }
        else
        {
            _cache.TryRemove(key, out _);
        }
    }
}
