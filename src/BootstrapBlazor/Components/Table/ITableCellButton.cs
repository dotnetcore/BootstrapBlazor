// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITableCellButton 接口
/// </summary>
public interface ITableCellButton : ITableCellComponent
{
    /// <summary>
    /// 获得/设置 点击按钮是否选中正行 默认 true 选中
    /// </summary>
    bool AutoSelectedRowWhenClick { get; set; }

    /// <summary>
    /// 获得/设置 点击按钮是否重新渲染 Table 组件 默认 false 不重新渲染
    /// 设置 true 时会调用 <see cref="Table{TItem}.QueryAsync(int?)"/> 方法，触发 <see cref="Table{TItem}.OnQueryAsync"/> 回调
    /// </summary>
    bool AutoRenderTableWhenClick { get; set; }
}
