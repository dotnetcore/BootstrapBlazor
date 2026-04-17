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
public class DataTableDynamicObject(DataRow row) : DynamicObject
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyName"></param>
    public override object? GetValue(string propertyName)
    {
        if (row.IsDeletedOrDetached())
        {
            return null;
        }

        object? ret = null;
        if (row.Table.Columns.Contains(propertyName))
        {
            ret = row[propertyName];
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void SetValue(string propertyName, object? value)
    {
        if (row.IsDeletedOrDetached())
        {
            return;
        }

        if (row.Table.Columns.Contains(propertyName))
        {
            row[propertyName] = value;
        }
    }
}
