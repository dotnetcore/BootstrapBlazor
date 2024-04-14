// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components.Extensions;
using System.Data;

namespace BootstrapBlazor.Components;

class ExportDataReader<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn> cols, TableExportOptions options) : IDataReader
{
    private int _rowIndex = -1;
    private readonly IEnumerable<TModel> _rows = items;
    private readonly IEnumerable<ITableColumn> _columns = cols;
    private readonly TableExportOptions _options = options;
    private readonly int _rowCount = items.Count();

    public object this[int i]
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

    public object this[string name]
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

    public int Depth { get; }

    public bool IsClosed { get; }

    public int RecordsAffected { get; }

    public int FieldCount { get; } = cols.Count();

    public bool GetBoolean(int i)
    {
        throw new NotImplementedException();
    }

    public byte GetByte(int i)
    {
        throw new NotImplementedException();
    }

    public long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferOffset, int length)
    {
        throw new NotImplementedException();
    }

    public char GetChar(int i)
    {
        throw new NotImplementedException();
    }

    public long GetChars(int i, long fieldOffset, char[]? buffer, int bufferOffset, int length)
    {
        throw new NotImplementedException();
    }

    public IDataReader GetData(int i)
    {
        throw new NotImplementedException();
    }

    public string GetDataTypeName(int i)
    {
        throw new NotImplementedException();
    }

    public DateTime GetDateTime(int i)
    {
        throw new NotImplementedException();
    }

    public decimal GetDecimal(int i)
    {
        throw new NotImplementedException();
    }

    public double GetDouble(int i)
    {
        throw new NotImplementedException();
    }

    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)]
    public Type GetFieldType(int i)
    {
        throw new NotImplementedException();
    }

    public float GetFloat(int i)
    {
        throw new NotImplementedException();
    }

    public Guid GetGuid(int i)
    {
        throw new NotImplementedException();
    }

    public short GetInt16(int i)
    {
        throw new NotImplementedException();
    }

    public int GetInt32(int i)
    {
        throw new NotImplementedException();
    }

    public long GetInt64(int i)
    {
        throw new NotImplementedException();
    }

    public int GetOrdinal(string name)
    {
        throw new NotImplementedException();
    }

    public DataTable? GetSchemaTable()
    {
        throw new NotImplementedException();
    }

    public string GetString(int i)
    {
        throw new NotImplementedException();
    }

    public int GetValues(object[] values)
    {
        throw new NotImplementedException();
    }

    public bool IsDBNull(int i)
    {
        throw new NotImplementedException();
    }

    public bool NextResult()
    {
        throw new NotImplementedException();
    }

    public string GetName(int i)
    {
        var col = _columns.ElementAtOrDefault(i);
        return col?.GetDisplayName() ?? string.Empty;
    }

    public object GetValue(int i)
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

    public bool Read()
    {
        _rowIndex++;
        return _rowIndex < _rowCount;
    }

    public void Close()
    {

    }

    public void Dispose()
    {

    }
}
