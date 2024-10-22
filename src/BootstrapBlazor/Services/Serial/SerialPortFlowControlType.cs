// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Core.Converter;

namespace BootstrapBlazor.Components;

/// <summary>
/// 流量控制方法
/// </summary>
[JsonEnumConverter(true)]
public enum SerialPortFlowControlType
{
    /// <summary>
    /// 未启用流量控制
    /// </summary>
    None,

    /// <summary>
    /// 启用使用 RTS 和 CTS 信号的硬件流控制
    /// </summary>
    Hardware,
}
