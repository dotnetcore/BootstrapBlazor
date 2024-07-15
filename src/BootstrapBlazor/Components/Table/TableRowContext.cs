// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
