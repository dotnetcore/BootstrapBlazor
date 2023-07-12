// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class TableExportDataContext<TItem> : ITableExportDataContext<TItem>
{
    public IEnumerable<TItem> Rows { get; }

    public string ExportType { get; }

    public QueryPageOptions Options { get; }

    public IEnumerable<ITableColumn> Columns { get; }

    public TableExportDataContext(string exportType, IEnumerable<TItem> rows, IEnumerable<ITableColumn> cols, QueryPageOptions options)
    {
        ExportType = exportType;
        Rows = rows;
        Columns = cols;
        Options = options;
    }
}
