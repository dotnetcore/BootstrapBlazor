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
    Task<bool> GetAvailability(CancellationToken token = default);

    /// <summary>
    /// 请求蓝牙配对方法
    /// </summary>
    /// <param name="optionalServices">请求服务列表 请参考 https://github.com/WebBluetoothCG/registries/blob/master/gatt_assigned_services.txt</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IBluetoothDevice?> RequestDevice(string[] optionalServices, CancellationToken token = default);
}
