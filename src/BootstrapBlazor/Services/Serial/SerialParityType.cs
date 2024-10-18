// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 校验位枚举
/// </summary>
public enum SerialParityType
{
    /// <summary>
    /// 每个数据字不发送奇偶校验位
    /// </summary>
    [Description("未启用")]
    None,

    /// <summary>
    /// 数据字加上奇偶校验位具有偶奇偶校验
    /// </summary>
    [Description("偶校验")]
    Even,

    /// <summary>
    /// 数据字加奇偶校验位具有奇校验
    /// </summary>
    [Description("奇校验")]
    Odd
}
