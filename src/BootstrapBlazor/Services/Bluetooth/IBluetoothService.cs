// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IBluetoothService 接口定义</para>
/// <para lang="en">IBluetoothService Interface Definition</para>
/// </summary>
public interface IBluetoothService
{
    /// <summary>
    /// <para lang="zh">获得 服务名称</para>
    /// <para lang="en">Get Service Name</para>
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// <para lang="zh">获得 服务 UUID 值</para>
    /// <para lang="en">Get Service UUID Value</para>
    /// </summary>
    public string UUID { get; }

    /// <summary>
    /// <para lang="zh">获得 上次运行错误描述信息</para>
    /// <para lang="en">Get Last Error Message</para>
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// <para lang="zh">获得设备所有支持特征</para>
    /// <para lang="en">Get All Supported Characteristics</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<IBluetoothCharacteristic>> GetCharacteristics(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得设备特定支持特征</para>
    /// <para lang="en">Get Specified Supported Characteristic</para>
    /// </summary>
    /// <param name="characteristicUUID"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IBluetoothCharacteristic?> GetCharacteristic(string characteristicUUID, CancellationToken token = default);
}
