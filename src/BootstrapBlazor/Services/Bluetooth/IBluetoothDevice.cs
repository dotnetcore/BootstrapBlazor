// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IBluetoothDevice 接口
/// </summary>
public interface IBluetoothDevice
{
    /// <summary>
    /// 获得 当前设备连接状态
    /// </summary>
    bool Connected { get; }

    /// <summary>
    /// 获得 设备 Id
    /// </summary>
    string? Id { get; }

    /// <summary>
    /// 获得 设备名称
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// 获得 上次运行错误描述信息
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// 连接方法
    /// </summary>
    /// <returns></returns>
    Task<bool> Connect(CancellationToken token = default);

    /// <summary>
    /// 断开连接方法
    /// </summary>
    /// <returns></returns>
    Task<bool> Disconnect(CancellationToken token = default);

    /// <summary>
    /// 获得设备指定值方法
    /// </summary>
    /// <remarks>比如获得电量方法为 ReadValue("battery_service", "battery_level")</remarks>
    /// <returns></returns>
    Task<byte[]?> ReadValue(string serviceName, string characteristicName, CancellationToken token = default);
}
