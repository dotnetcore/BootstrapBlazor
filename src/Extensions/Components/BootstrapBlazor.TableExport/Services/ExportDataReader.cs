// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components.Extensions;
using MiniExcelLibs;
using System.Data;

namespace BootstrapBlazor.Components;

class ExportDataReader<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn> cols, TableExportOptions options) : MiniExcelDataReaderBase
{
    private int _rowIndex = -1;
    private readonly IEnumerable<TModel> _rows = items;
    private readonly IEnumerable<ITableColumn> _columns = cols;
    private readonly TableExportOptions _options = options;
    private readonly int _rowCount = items.Count();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public override object this[int i]
    {
        get
        {
            object? ret = null;
            var row = _rows.ElementAtOrDefault(_rowIndex);
            if (row != null)
            {
                ret = _columns.ElementAtOrDefault(i);
            }
            return ret!;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override object this[string name]
    {
        get
        {
            object? ret = null;
            var row = _rows.ElementAtOrDefault(_rowIndex);
            if (row != null)
            {
                ret = _columns.FirstOrDefault(i => i.GetFieldName() == name);
            }
            return ret!;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override int FieldCount { get; } = cols.Count();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public override string GetName(int i)
    {
        var col = _columns.ElementAtOrDefault(i);
        return col?.GetDisplayName() ?? string.Empty;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public override object GetValue(int i)
    {
        object? v = null;
        var row = _rows.ElementAtOrDefault(_rowIndex);
        var col = _columns.ElementAtOrDefault(i);
        if (row != null && col != null)
        {
            v = Utility.GetPropertyValue(row, col.GetFieldName());
            if (v != null)
            {
                var task = col.FormatValue(v, _options);
                if (task.Wait(1000))
                {
                    v = task.Result;
                }
            }
        }
        return v!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override bool Read()
    {
        _rowIndex++;
        return _rowIndex < _rowCount;
    }
}
