// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 视图枚举类型
/// </summary>
public enum TableRenderMode
{
    /// <summary>
    /// 自动
    /// </summary>
    [Description("自动")]
    Auto,

    /// <summary>
    /// Table 布局适用于大屏幕
    /// </summary>
    [Description("表格布局")]
    Table,

    /// <summary>
    /// 卡片式布局适用于小屏幕
    /// </summary>
    [Description("卡片布局")]
    CardView
}
