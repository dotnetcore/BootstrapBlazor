// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh"><inheritdoc/></para>
///  <para lang="en"><inheritdoc/></para>
/// </summary>
internal class TableExportContext<TItem> : ITableExportContext<TItem>
{
    /// <summary>
    ///  <para lang="zh">获得 Table 实例</para>
    ///  <para lang="en">Gets Table instance</para>
    /// </summary>
    private ITable Table { get; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    public IEnumerable<ITableColumn> Columns => Table.Columns;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    public IEnumerable<ITableColumn> GetVisibleColumns() => Table.GetVisibleColumns();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    public IEnumerable<TItem> Rows { get; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Task ExportAsync() => ExportCallbackAsync();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    public QueryPageOptions BuildQueryPageOptions() => OptionsBuilder();

    private Func<QueryPageOptions> OptionsBuilder { get; }

    private Func<Task> ExportCallbackAsync { get; }

    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">构造函数</para>
    /// </summary>
    /// <param name="table"><para lang="zh">ITable 实例</para><para lang="en">ITable instance</para></param>
    /// <param name="rows"><para lang="zh">行数据集合</para><para lang="en">行datacollection</para></param>
    /// <param name="optionsBuilder"><para lang="zh">ITable 实例当前条件生成方法</para><para lang="en">ITable instance当前条件生成method</para></param>
    /// <param name="exportAsync"><para lang="zh">ITable 实例内置 Export 方法</para><para lang="en">ITable instance内置 Export method</para></param>
    public TableExportContext(ITable table, IEnumerable<TItem> rows, Func<QueryPageOptions> optionsBuilder, Func<Task> exportAsync)
    {
        Table = table;
        Rows = rows;
        ExportCallbackAsync = exportAsync;
        OptionsBuilder = optionsBuilder;
    }
}
