// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
