// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ITableCellButton 接口</para>
///  <para lang="en">ITableCellButton 接口</para>
/// </summary>
public interface ITableCellButton : ITableCellComponent
{
    /// <summary>
    ///  <para lang="zh">获得/设置 点击按钮是否选中正行 默认 true 选中</para>
    ///  <para lang="en">Gets or sets 点击buttonwhether选中正行 Default is true 选中</para>
    /// </summary>
    bool AutoSelectedRowWhenClick { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 点击按钮是否重新渲染 Table 组件 默认 false 不重新渲染 设置 true 时会调用 <see cref="Table{TItem}.QueryAsync(int?)"/> 方法，触发 <see cref="Table{TItem}.OnQueryAsync"/> 回调</para>
    ///  <para lang="en">Gets or sets 点击buttonwhether重新渲染 Table component Default is false 不重新渲染 Sets true 时会调用 <see cref="Table{TItem}.QueryAsync(int?)"/> 方法，触发 <see cref="Table{TItem}.OnQueryAsync"/> 回调</para>
    /// </summary>
    bool AutoRenderTableWhenClick { get; set; }
}
