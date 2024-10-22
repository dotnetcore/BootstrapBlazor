// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器指纹服务
/// </summary>
class DefaultBrowserFingerService : IBrowserFingerService
{
    private ConcurrentDictionary<object, Func<Task<string?>>> Cache { get; } = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="target"></param>
    /// <param name="callback"></param>
    public void Subscribe(object target, Func<Task<string?>> callback) => Cache.GetOrAdd(target, k => callback);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="target"></param>
    public void Unsubscribe(object target) => Cache.TryRemove(target, out _);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetFingerCodeAsync()
    {
        string? code = null;
        var cb = Cache.LastOrDefault();
        if (cb.Value != null)
        {
            code = await cb.Value();
        }
        return code;
    }
}
