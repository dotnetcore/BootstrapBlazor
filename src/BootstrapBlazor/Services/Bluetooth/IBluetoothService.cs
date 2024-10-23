// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IBluetoothService 接口定义
/// </summary>
public interface IBluetoothService
{
    /// <summary>
    /// 获得 服务名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 获得 服务 UUID 值
    /// </summary>
    public string UUID { get; }

    /// <summary>
    /// 获得 上次运行错误描述信息
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// 获得设备所有支持特征
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<IBluetoothCharacteristic>> GetCharacteristics(CancellationToken token = default);

    /// <summary>
    /// 获得设备特定支持特征
    /// </summary>
    /// <param name="characteristicName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IBluetoothCharacteristic?> GetCharacteristic(string characteristicName, CancellationToken token = default);
}
