// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// <inheritdoc/>
/// </summary>
internal class TableExportContext<TItem> : ITableExportContext<TItem>
{
    /// <summary>
    /// 获得 Table 实例
    /// </summary>
    private ITable Table { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<ITableColumn> Columns => Table.Columns;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<ITableColumn> GetVisibleColumns() => Table.GetVisibleColumns();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<TItem> Rows { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task ExportAsync() => ExportCallbackAsync();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public QueryPageOptions BuildQueryPageOptions() => OptionsBuilder();

    private Func<QueryPageOptions> OptionsBuilder { get; }

    private Func<Task> ExportCallbackAsync { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="table">ITable 实例</param>
    /// <param name="rows">行数据集合</param>
    /// <param name="optionsBuilder">ITable 实例当前条件生成方法</param>
    /// <param name="exportAsync">ITable 实例内置 Export 方法</param>
    public TableExportContext(ITable table, IEnumerable<TItem> rows, Func<QueryPageOptions> optionsBuilder, Func<Task> exportAsync)
    {
        Table = table;
        Rows = rows;
        ExportCallbackAsync = exportAsync;
        OptionsBuilder = optionsBuilder;
    }
}
