// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">设备地理位置服务</para>
/// <para lang="en">Device Geo-Location Service</para>
/// </summary>
public interface IGeoLocationService : IAsyncDisposable
{
    /// <summary>
    /// <para lang="zh">获得设备地理位置方法</para>
    /// <para lang="en">Get Device Geo-Location Method</para>
    /// </summary>
    /// <returns></returns>
    Task<GeolocationPosition?> GetPositionAsync();

    /// <summary>
    /// <para lang="zh">注册 WatchPositionAsync 监控地理位置变化方法</para>
    /// <para lang="en">Register WatchPositionAsync to monitor location changes</para>
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    ValueTask<long> WatchPositionAsync(Func<GeolocationPosition, Task> callback);

    /// <summary>
    /// <para lang="zh">清除 WatchPositionAsync 方法</para>
    /// <para lang="en">Clear WatchPositionAsync Method</para>
    /// </summary>
    /// <param name="id"><see cref="WatchPositionAsync"/> 方法返回值</param>
    /// <returns></returns>
    ValueTask<bool> ClearWatchPositionAsync(long id);
}
