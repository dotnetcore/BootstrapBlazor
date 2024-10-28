// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 日历组件单元格值结构体
/// </summary>
public class CalendarCellValue
{
    /// <summary>
    /// 获得/设置 当前单元格值
    /// </summary>
    public DateTime CellValue { get; set; }

    /// <summary>
    /// 获得/设置 日历框组件值
    /// </summary>
    public DateTime CalendarValue { get; set; }

    /// <summary>
    /// 获得/设置 单元格默认样式
    /// </summary>
    public string? DefaultCss { get; set; }
}
