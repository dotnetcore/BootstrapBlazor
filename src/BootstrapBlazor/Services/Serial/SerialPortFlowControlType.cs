// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
