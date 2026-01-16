// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">日历组件单元格值结构体</para>
/// <para lang="en">Calendar component cell value structure</para>
/// </summary>
public class CalendarCellValue
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前单元格值</para>
    /// <para lang="en">Gets or sets the current cell value</para>
    /// </summary>
    public DateTime CellValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 日历框组件值</para>
    /// <para lang="en">Gets or sets the calendar component value</para>
    /// </summary>
    public DateTime CalendarValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 单元格默认样式</para>
    /// <para lang="en">Gets or sets the cell default style</para>
    /// </summary>
    public string? DefaultCss { get; set; }
}
