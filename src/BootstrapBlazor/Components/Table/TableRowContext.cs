﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TableRow 上下文类
/// </summary>
/// <param name="model"></param>
/// <param name="columns"></param>
public class TableRowContext<TItem>(TItem model, IEnumerable<ITableColumn> columns)
{
    /// <summary>
    /// 获得/设置 行数据实例
    /// </summary>
    [NotNull]
    public TItem Row { get; } = model ?? throw new ArgumentNullException(nameof(model));

    /// <summary>
    /// 获得/设置 当前绑定字段数据实例
    /// </summary>
    public IEnumerable<ITableColumn> Columns => columns;
}
