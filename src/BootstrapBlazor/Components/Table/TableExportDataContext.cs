// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal class TableExportDataContext<TItem> : ITableExportDataContext<TItem>
{
    public IEnumerable<TItem> Rows { get; }

    public TableExportType ExportType { get; }

    public QueryPageOptions Options { get; }

    public IEnumerable<ITableColumn> Columns { get; }

    public TableExportDataContext(TableExportType exportType, IEnumerable<TItem> rows, IEnumerable<ITableColumn> cols, QueryPageOptions options)
    {
        ExportType = exportType;
        Rows = rows;
        Columns = cols;
        Options = options;
    }
}
