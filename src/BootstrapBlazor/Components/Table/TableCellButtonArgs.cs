// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">单元格内按钮组件
///</para>
/// <para lang="en">单元格内buttoncomponent
///</para>
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
