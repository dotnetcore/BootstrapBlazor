// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Core.Converter;

namespace BootstrapBlazor.Components;

/// <summary>
/// 校验位枚举
/// </summary>
[JsonEnumConverter(true)]
public enum SerialPortParityType
{
    /// <summary>
    /// 每个数据字不发送奇偶校验位
    /// </summary>
    None,

    /// <summary>
    /// 数据字加上奇偶校验位具有偶奇偶校验
    /// </summary>
    Even,

    /// <summary>
    /// 数据字加奇偶校验位具有奇校验
    /// </summary>
    Odd
}
