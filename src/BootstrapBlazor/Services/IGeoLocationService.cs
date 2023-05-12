// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
