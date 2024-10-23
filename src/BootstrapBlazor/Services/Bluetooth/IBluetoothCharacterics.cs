﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IBluetoothCharacteristic 接口定义
/// </summary>
public interface IBluetoothCharacteristic
{
    /// <summary>
    /// 设备指定特征开始持续回调方法
    /// </summary>
    /// <param name="notificationCallback"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> StartNotifications(Func<byte[], Task> notificationCallback, CancellationToken token = default);

    /// <summary>
    /// 设备指定特征结束回调方法
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> StopNotifications(CancellationToken token = default);
}
