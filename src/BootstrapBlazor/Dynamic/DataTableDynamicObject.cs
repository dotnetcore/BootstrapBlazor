// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Data;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">DataTable 动态类实例</para>
///  <para lang="en">DataTable 动态类instance</para>
/// </summary>
public class DataTableDynamicObject : DynamicObject
{
    /// <summary>
    ///  <para lang="zh">获得/设置 DataRow 实例</para>
    ///  <para lang="en">Gets or sets DataRow instance</para>
    /// </summary>
    internal DataRow? Row { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
