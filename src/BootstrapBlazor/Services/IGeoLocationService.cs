// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 设备地理位置服务
/// </summary>
public interface IGeoLocationService : IAsyncDisposable
{
    /// <summary>
    /// 获得设备地理位置方法
    /// </summary>
    /// <returns></returns>
    Task<GeolocationPosition?> GetPositionAsync();

    /// <summary>
    /// 注册 WatchPositionAsync 监控地理位置变化方法
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    ValueTask<long> WatchPositionAsync(Func<GeolocationPosition, Task> callback);

    /// <summary>
    /// 清除 WatchPositionAsync 方法
    /// </summary>
    /// <param name="id"><see cref="WatchPositionAsync"/> 方法返回值</param>
    /// <returns></returns>
    ValueTask<bool> ClearWatchPositionAsync(long id);
}
