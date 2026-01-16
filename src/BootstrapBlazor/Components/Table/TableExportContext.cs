// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <inheritdoc/>
/// </summary>
internal class TableExportContext<TItem> : ITableExportContext<TItem>
{
    /// <summary>
    /// <para lang="zh">获得 Table 实例
    ///</para>
    /// <para lang="en">Gets Table instance
    ///</para>
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
    /// <para lang="zh">构造函数
    ///</para>
    /// <para lang="en">构造函数
    ///</para>
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
