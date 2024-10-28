// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 表格 thead 样式枚举
/// </summary>
public enum TableHeaderStyle
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 浅色
    /// </summary>
    [Description("table-light")]
    Light,

    /// <summary>
    /// 深色
    /// </summary>
    [Description("table-dark")]
    Dark
}
