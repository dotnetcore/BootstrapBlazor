// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableSettings 表格组件全局默认设置</para>
/// <para lang="en">TableSettings component global default settings</para>
/// </summary>
public class TableSettings
{
    /// <summary>
    /// <para lang="zh">获得/设置 复选框宽度 默认 36</para>
    /// <para lang="en">Gets or sets checkbox column width default 36</para>
    /// </summary>
    public int CheckboxColumnWidth { get; set; } = 36;

    /// <summary>
    /// <para lang="zh">获得/设置 复选框宽度 默认 28</para>
    /// <para lang="en">Gets or sets checkbox column compact width default 28</para>
    /// </summary>
    public int CheckboxColumnCompactWidth { get; set; } = 28;

    /// <summary>
    /// <para lang="zh">获得/设置 明细行 Row Header 宽度 默认 24</para>
    /// <para lang="en">Gets or sets detail row header width default 24</para>
    /// </summary>
    public int DetailColumnWidth { get; set; } = 24;

    /// <summary>
    /// <para lang="zh">获得/设置 显示文字的复选框列宽度 默认 80</para>
    /// <para lang="en">Gets or sets show checkbox text column width default 80</para>
    /// </summary>
    public int ShowCheckboxTextColumnWidth { get; set; } = 80;

    /// <summary>
    /// <para lang="zh">获得/设置 行号列宽度 默认 60</para>
    /// <para lang="en">Gets or sets line no column width default 60</para>
    /// </summary>
    public int LineNoColumnWidth { get; set; } = 60;

    /// <summary>
    /// <para lang="zh">获得/设置 列最小宽度 默认 64</para>
    /// <para lang="en">Gets or sets column min width default 64</para>
    /// </summary>
    public int ColumnMinWidth { get; set; } = 64;

    /// <summary>
    /// <para lang="zh">获得/设置 表格渲染模式</para>
    /// <para lang="en">Gets or sets table render mode</para>
    /// </summary>
    public TableRenderMode? TableRenderMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 TableExportExcelOptions 配置 默认为不为空</para>
    /// <para lang="en">Gets or sets TableExportExcelOptions configuration default not null</para>
    /// </summary>
    public TableExportOptions TableExportOptions { get; set; } = new();
}
