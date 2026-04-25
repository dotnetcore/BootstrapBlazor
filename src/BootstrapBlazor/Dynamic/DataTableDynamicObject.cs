// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Data;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DataTable 动态类实例</para>
/// <para lang="en">DataTable dynamic class instance</para>
/// </summary>
public class DataTableDynamicObject : DynamicObject
{
    /// <summary>
    /// <para lang="zh">获得 其关联 DataRow 实例</para>
    /// <para lang="en">Gets the associated DataRow instance</para>
    /// </summary>
    [NotNull]
    internal DataRow? Row { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    public override object? GetValue(string propertyName)
    {
        if (Row.IsDeletedOrDetached())
        {
            return null;
        }

        object? ret = null;
        var column = Row.Table.Columns[propertyName];
        if (column != null)
        {
            ret = Row[propertyName];

            // 判断 DBNull 情况
            if (ret is DBNull)
            {
                // 此处如果是 DBNull 应该返回当前列数据类型的默认值
                var columnType = column.DataType;
                if (Nullable.GetUnderlyingType(columnType) != null)
                {
                    // 如果当前列数据类型是可为空的值类型，则返回 null，否则返回当前列数据类型的默认值
                    ret = null;
                }
                else
                {
                    ret = columnType.IsValueType ? Activator.CreateInstance(columnType) : null;
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void SetValue(string propertyName, object? value)
    {
        if (Row.IsDeletedOrDetached())
        {
            return;
        }

        if (Row.Table.Columns.Contains(propertyName))
        {
            Row[propertyName] = value;
        }
    }

    /// <summary>
    /// <para lang="zh">撤销数据更改</para>
    /// <para lang="en">Cancels data changes</para>
    /// </summary>
    public override void Cancel()
    {
        row.Table.RejectChanges();
    }

    /// <summary>
    /// <para lang="zh">接受数据更改</para>
    /// <para lang="en">Accepts data changes</para>
    /// </summary>
    public override void Accept()
    {
        row.Table.AcceptChanges();
    }
}
