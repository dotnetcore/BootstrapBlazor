// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableRow 上下文类
///</para>
/// <para lang="en">TableRow 上下文类
///</para>
/// </summary>
/// <param name="model"></param>
/// <param name="columns"></param>
/// <param name="renderMode"></param>
public class TableRowContext<TItem>(TItem model, IEnumerable<ITableColumn> columns, TableRenderMode renderMode)
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前渲染模式
    ///</para>
    /// <para lang="en">Gets or sets 当前渲染模式
    ///</para>
    /// </summary>
    public TableRenderMode RenderMode { get; } = renderMode;

    /// <summary>
    /// <para lang="zh">获得/设置 行数据实例
    ///</para>
    /// <para lang="en">Gets or sets 行datainstance
    ///</para>
    /// </summary>
    [NotNull]
    public TItem Row { get; } = model ?? throw new ArgumentNullException(nameof(model));

    /// <summary>
    /// <para lang="zh">获得/设置 当前绑定字段数据实例
    ///</para>
    /// <para lang="en">Gets or sets 当前绑定字段datainstance
    ///</para>
    /// </summary>
    public IEnumerable<ITableColumn> Columns { get; } = columns;
}
