// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

class DefaultThrottleDispatcherFactory : IThrottleDispatcherFactory
{
    private readonly ConcurrentDictionary<string, ThrottleDispatcher> _cache = new();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="options"></param>
    public ThrottleDispatcher GetOrCreate(string key, ThrottleOptions? options = null) => _cache.GetOrAdd(key, key => new ThrottleDispatcher(options ?? new()));

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
