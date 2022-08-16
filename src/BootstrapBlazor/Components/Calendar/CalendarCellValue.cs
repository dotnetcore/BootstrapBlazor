// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 日历组件单元格值结构体
/// </summary>
public struct CalendarCellValue
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
