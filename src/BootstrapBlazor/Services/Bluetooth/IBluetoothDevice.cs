// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IBluetoothDevice 接口</para>
/// <para lang="en">IBluetoothDevice Interface</para>
/// </summary>
public interface IBluetoothDevice : IAsyncDisposable
{
    /// <summary>
    /// <para lang="zh">获得 当前设备连接状态</para>
    /// <para lang="en">Get Current Device Connection Status</para>
    /// </summary>
    bool Connected { get; }

    /// <summary>
    /// <para lang="zh">获得 设备 Id</para>
    /// <para lang="en">Get Device Id</para>
    /// </summary>
    string? Id { get; }

    /// <summary>
    /// <para lang="zh">获得 设备名称</para>
    /// <para lang="en">Get Device Name</para>
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// <para lang="zh">获得 上次运行错误描述信息</para>
    /// <para lang="en">Get Last Error Message</para>
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// <para lang="zh">连接方法</para>
    /// <para lang="en">Connect Method</para>
    /// </summary>
    /// <returns></returns>
    Task<bool> Connect(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">断开连接方法</para>
    /// <para lang="en">Disconnect Method</para>
    /// </summary>
    /// <returns></returns>
    Task<bool> Disconnect(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备所有支持服务</para>
    /// <para lang="en">Get All Supported Services of Device</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<IBluetoothService>> GetPrimaryServices(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备指定服务</para>
    /// <para lang="en">Get Specified Service of Device</para>
    /// </summary>
    /// <param name="serviceUUID"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IBluetoothService?> GetPrimaryService(string serviceUUID, CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备指定值方法</para>
    /// <para lang="en">Read Value Method</para>
    /// </summary>
    /// <remarks><para lang="zh">比如获得电量方法为 ReadValue("battery_service", "battery_level")</para><para lang="en">For example, get battery level method is ReadValue("battery_service", "battery_level")</para></remarks>
    /// <returns></returns>
    Task<byte[]?> ReadValue(string serviceUUID, string characteristicUUID, CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备信息方法</para>
    /// <para lang="en">Get Device Info Method</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<BluetoothDeviceInfo?> GetDeviceInfo(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备当前时间方法</para>
    /// <para lang="en">Get Device Current Time Method</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DateTimeOffset?> GetCurrentTime(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备当前时间方法</para>
    /// <para lang="en">Get Device Battery Level Method</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<byte?> GetBatteryValue(CancellationToken token = default);
}
