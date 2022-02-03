// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}
