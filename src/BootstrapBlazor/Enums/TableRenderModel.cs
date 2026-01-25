// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 视图枚举类型</para>
/// <para lang="en">Table 视图enumtype</para>
/// </summary>
public enum TableRenderMode
{
    /// <summary>
    /// <para lang="zh">自动</para>
    /// <para lang="en">自动</para>
    /// </summary>
    [Description("自动")]
    Auto,

    /// <summary>
    /// <para lang="zh">Table 布局适用于大屏幕</para>
    /// <para lang="en">Table 布局适用于大屏幕</para>
    /// </summary>
    [Description("表格布局")]
    Table,

    /// <summary>
    /// <para lang="zh">卡片式布局适用于小屏幕</para>
    /// <para lang="en">卡片式布局适用于小屏幕</para>
    /// </summary>
    [Description("卡片布局")]
    CardView
}
