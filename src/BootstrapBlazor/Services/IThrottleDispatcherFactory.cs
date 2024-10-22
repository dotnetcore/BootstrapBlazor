// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 同步限流器接口
/// </summary>
public interface IThrottleDispatcherFactory
{
    /// <summary>
    /// 获得或创建限流器
    /// </summary>
    /// <returns></returns>
    ThrottleDispatcher GetOrCreate(string key, ThrottleOptions? options = null);

    /// <summary>
    /// 获得或创建限流器
    /// </summary>
    /// <returns></returns>
    ThrottleDispatcher GetOrCreate(string key, int interval) => GetOrCreate(key, new ThrottleOptions() { Interval = TimeSpan.FromMilliseconds(interval) });

    /// <summary>
    /// 销毁限流器
    /// </summary>
    /// <param name="key">为空时销毁所有限流器</param>
    void Clear(string? key = null);
}
