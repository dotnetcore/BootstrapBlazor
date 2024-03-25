// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

class DefaultThrottleDispatcherFactory : IThrottleDispatcherFactory
{
    private ConcurrentDictionary<string, ThrottleDispatcher> _cache = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="options"></param>
    public ThrottleDispatcher GetOrCreate(string key, ThrottleOptions options) => _cache.GetOrAdd(key, key => new ThrottleDispatcher(options));
}
