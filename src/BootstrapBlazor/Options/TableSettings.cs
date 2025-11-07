// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TableSettings 表格组件全局默认设置
/// </summary>
public class TableSettings
{
    /// <summary>
    /// 获得/设置 复选框宽度 默认 36
    /// </summary>
    public int CheckboxColumnWidth { get; set; } = 36;

    /// <summary>
    /// 获得/设置 复选框宽度 默认 28
    /// </summary>
    public int CheckboxColumnCompactWidth { get; set; } = 28;

    /// <summary>
    /// 获得/设置 明细行 Row Header 宽度 默认 24
    /// </summary>
    public int DetailColumnWidth { get; set; } = 24;

    /// <summary>
    /// 获得/设置 显示文字的复选框列宽度 默认 80
    /// </summary>
    public int ShowCheckboxTextColumnWidth { get; set; } = 80;

    /// <summary>
    /// 获得/设置 行号列宽度 默认 60
    /// </summary>
    public int LineNoColumnWidth { get; set; } = 60;

    /// <summary>
    /// 获得/设置 列最小宽度 默认 64
    /// </summary>
    public int ColumnMinWidth { get; set; } = 64;

    /// <summary>
    /// 获得/设置 表格渲染模式
    /// </summary>
    public TableRenderMode? TableRenderMode { get; set; }

    /// <summary>
    /// 获得/设置 TableExportExcelOptions 配置 默认为不为空
    /// </summary>
    public TableExportOptions TableExportOptions { get; set; } = new();
}
