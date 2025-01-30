// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Data;

namespace BootstrapBlazor.Components;

/// <summary>
/// DataTable 动态类实例
/// </summary>
public class DataTableDynamicObject : DynamicObject
{
    /// <summary>
    /// 获得/设置 DataRow 实例
    /// </summary>
    internal DataRow? Row { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public override object? GetValue(string propertyName)
    {
        object? ret = null;
        if (Row != null && Row.RowState != DataRowState.Deleted && Row.RowState != DataRowState.Detached && Row.Table.Columns.Contains(propertyName))
        {
            if (Row.RowState == DataRowState.Added)
            {
                // 新建行 Model 同步到 Row 上
                if (!Row.Table.Columns[propertyName]!.AutoIncrement)
                {
                    // 自增长列
                    Row[propertyName] = Utility.GetPropertyValue(this, propertyName);
                }
            }
            ret = Row[propertyName];
        }
        return ret ?? Utility.GetPropertyValue(this, propertyName);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public override void SetValue(string propertyName, object? value)
    {
        base.SetValue(propertyName, value);

        if (Row != null && Row.Table.Columns.Contains(propertyName))
        {
            Row[propertyName] = value;
        }
    }
}
