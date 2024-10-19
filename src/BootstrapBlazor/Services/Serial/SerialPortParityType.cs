// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
