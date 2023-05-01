// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 设备地理位置服务
/// </summary>
public interface IGeoLocationService
{
    /// <summary>
    /// 获得设备地理位置方法
    /// </summary>
    /// <returns></returns>
    Task<GeolocationPosition?> GetPositionAsync();
}
