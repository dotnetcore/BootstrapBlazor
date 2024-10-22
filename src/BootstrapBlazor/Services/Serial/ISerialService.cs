// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ISerialService 串口通讯服务
/// </summary>
public interface ISerialService
{
    /// <summary>
    /// 获得/设置 是否支持串口通讯
    /// </summary>
    bool IsSupport { get; }

    /// <summary>
    /// 获得所有可用串口
    /// </summary>
    /// <returns></returns>
    Task<ISerialPort?> GetPort(CancellationToken token = default);
}
