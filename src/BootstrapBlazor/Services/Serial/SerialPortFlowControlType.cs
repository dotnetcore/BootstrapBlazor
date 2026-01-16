// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">流量控制方法</para>
///  <para lang="en">流量控制方法</para>
/// </summary>
[JsonEnumConverter(true)]
public enum SerialPortFlowControlType
{
    /// <summary>
    ///  <para lang="zh">未启用流量控制</para>
    ///  <para lang="en">未启用流量控制</para>
    /// </summary>
    None,

    /// <summary>
    ///  <para lang="zh">启用使用 RTS 和 CTS 信号的硬件流控制</para>
    ///  <para lang="en">启用使用 RTS 和 CTS 信号的硬件流控制</para>
    /// </summary>
    Hardware,
}
