// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// 获得 上次运行错误描述信息
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// 获得所有可用串口
    /// </summary>
    /// <returns></returns>
    Task<bool> GetAvailability(CancellationToken token = default);

    /// <summary>
    /// 请求蓝牙配对方法
    /// </summary>
    /// <param name="options"><see cref="BluetoothRequestOptions"/> 实例</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IBluetoothDevice?> RequestDevice(BluetoothRequestOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// 请求蓝牙配对方法
    /// </summary>
    /// <param name="optionalServices"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IBluetoothDevice?> RequestDevice(List<string> optionalServices, CancellationToken token = default);
}
