// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">同步限流器接口</para>
///  <para lang="en">Throttle Dispatcher Interface</para>
/// </summary>
public interface IThrottleDispatcherFactory
{
    /// <summary>
    ///  <para lang="zh">获得或创建限流器</para>
    ///  <para lang="en">Gets或创建限流器</para>
    /// </summary>
    /// <returns></returns>
    ThrottleDispatcher GetOrCreate(string key, ThrottleOptions? options = null);

    /// <summary>
    ///  <para lang="zh">获得或创建限流器</para>
    ///  <para lang="en">Get or Create Throttle Dispatcher</para>
    /// </summary>
    /// <returns></returns>
    ThrottleDispatcher GetOrCreate(string key, int interval) => GetOrCreate(key, new ThrottleOptions() { Interval = TimeSpan.FromMilliseconds(interval) });

    /// <summary>
    ///  <para lang="zh">销毁限流器</para>
    ///  <para lang="en">Destroy Throttle Dispatcher</para>
    /// </summary>
    /// <param name="key"><para lang="zh">为空时销毁所有限流器</para><para lang="en">If empty, destroy all throttle dispatchers</para></param>
    void Clear(string? key = null);
}
