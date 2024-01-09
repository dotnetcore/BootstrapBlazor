// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 单元格内按钮组件
/// </summary>
public class TableCellButtonArgs : ITableCellButton
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool AutoSelectedRowWhenClick { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool AutoRenderTableWhenClick { get; set; }
}
