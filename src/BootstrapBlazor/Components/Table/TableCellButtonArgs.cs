// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 单元格内按钮组件
/// </summary>
public class TableCellButtonArgs
{
    /// <summary>
    /// 获得/设置 点击按钮是否选中正行 默认 true 选中
    /// </summary>
    public bool AutoSelectedRowWhenClick { get; set; } = true;

    /// <summary>
    /// 获得/设置 点击按钮是否重新渲染 Table 组件 默认 false 不重新渲染
    /// </summary>
    /// <remarks>设置 true 时会调用 <see cref="Table{TItem}.QueryAsync(int?)"/> 方法，触发 <see cref="Table{TItem}.OnQueryAsync"/> 回调</remarks>
    public bool AutoRenderTableWhenClick { get; set; }
}
