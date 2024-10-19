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
    /// 获得/设置 是否支持蓝牙
    /// </summary>
    bool IsSupport { get; }

    /// <summary>
    /// 获得所有可用串口
    /// </summary>
    /// <returns></returns>
    Task<bool> GetAvailability();
}
