// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IBluetoothCharacteristic 接口定义</para>
/// <para lang="en">IBluetoothCharacteristic Interface Definition</para>
/// </summary>
public interface IBluetoothCharacteristic
{
    /// <summary>
    /// <para lang="zh">获得 上次运行错误描述信息</para>
    /// <para lang="en">Get Last Error Message</para>
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// <para lang="zh">获得 服务 UUID 值</para>
    /// <para lang="en">Get Service UUID Value</para>
    /// </summary>
    string ServiceUUID { get; }

    /// <summary>
    /// <para lang="zh">获得 特征 UUID 值</para>
    /// <para lang="en">Get Characteristic UUID Value</para>
    /// </summary>
    public string UUID { get; }

    /// <summary>
    /// <para lang="zh">获得 是否已经开始调用 <see cref="StartNotifications"/> 方法</para>
    /// <para lang="en">Get if <see cref="StartNotifications"/> method has been called</para>
    /// </summary>
    public bool IsNotify { get; }

    /// <summary>
    /// <para lang="zh">设备指定特征开始持续回调方法</para>
    /// <para lang="en">Start Notifications Method</para>
    /// </summary>
    /// <param name="notificationCallback"></param>
    /// <param name="token"></param>
    Task<bool> StartNotifications(Func<byte[], Task> notificationCallback, CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">设备指定特征结束回调方法</para>
    /// <para lang="en">Stop Notifications Method</para>
    /// </summary>
    /// <param name="token"></param>
    Task<bool> StopNotifications(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备指定值方法</para>
    /// <para lang="en">Read Value Method</para>
    /// </summary>
    /// <remarks><para lang="zh">比如获得电量方法为 ReadValue("battery_service", "battery_level")</para><para lang="en">For example, get battery level method is ReadValue("battery_service", "battery_level")</para></remarks>
    Task<byte[]?> ReadValue(CancellationToken token = default);
}
