// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙服务接口
/// </summary>
public interface IBluetoothService
{
    /// <summary>
    /// 获得 浏览器是否支持蓝牙
    /// </summary>
    bool IsSupport { get; }

    /// <summary>
    /// 获得 是否有蓝牙模块
    /// </summary>
    bool IsAvailable { get; }

    /// <summary>
    /// 获得所有可用串口
    /// </summary>
    /// <returns></returns>
    Task<bool> GetAvailability();

    /// <summary>
    /// 请求蓝牙配对方法
    /// </summary>
    /// <returns></returns>
    Task RequestDevice();

    /// <summary>
    /// 获得蓝牙设备方法
    /// </summary>
    /// <returns></returns>
    Task GetDevices();
}
