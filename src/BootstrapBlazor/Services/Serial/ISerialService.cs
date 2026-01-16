// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ISerialService 串口通讯服务
///</para>
/// <para lang="en">ISerialService 串口通讯服务
///</para>
/// </summary>
public interface ISerialService
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否支持串口通讯
    ///</para>
    /// <para lang="en">Gets or sets whether支持串口通讯
    ///</para>
    /// </summary>
    bool IsSupport { get; }

    /// <summary>
    /// <para lang="zh">获得所有可用串口
    ///</para>
    /// <para lang="en">Gets所有可用串口
    ///</para>
    /// </summary>
    /// <returns></returns>
    Task<ISerialPort?> GetPort(CancellationToken token = default);
}
