// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IBluetoothDevice 接口
/// </summary>
public interface IBluetoothDevice : IAsyncDisposable
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
    /// 获得设备所有支持服务
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<string>> GetPrimaryServices(CancellationToken token = default);

    /// <summary>
    /// 获得设备所有支持服务
    /// </summary>
    /// <param name="serviceName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<string>> GetCharacteristics(string serviceName, CancellationToken token = default);

    /// <summary>
    /// 获得设备指定值方法
    /// </summary>
    /// <remarks>比如获得电量方法为 ReadValue("battery_service", "battery_level")</remarks>
    /// <returns></returns>
    Task<byte[]?> ReadValue(string serviceName, string characteristicName, CancellationToken token = default);

    /// <summary>
    /// 获得设备信息方法
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<BluetoothDeviceInfo?> GetDeviceInfo(CancellationToken token = default);

    /// <summary>
    /// 获得设备当前时间方法
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DateTimeOffset?> GetCurrentTime(CancellationToken token = default);
}
