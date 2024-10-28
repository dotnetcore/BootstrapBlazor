// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 单元格数据类
/// </summary>
public class TableCellArgs
{
    /// <summary>
    /// 获得 当前单元格行数据 请自行转化为绑定模型
    /// </summary>
    [NotNull]
    public object? Row { get; internal set; }

    /// <summary>
    /// 获得 当前单元格绑定列名称
    /// </summary>
    [NotNull]
    public string? ColumnName { get; internal set; }

    /// <summary>
    /// 获得/设置 合并单元格数量 默认 0
    /// </summary>
    public int Colspan { get; set; }

    /// <summary>
    /// 获得/设置 当前单元格样式 默认 null
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 当前单元格显示内容
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 当前单元格内容模板
    /// </summary>
    public RenderFragment? ValueTemplate { get; set; }
}
