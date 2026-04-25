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
    public override object? GetValue(string propertyName) => Utility.GetPropertyValue(this, propertyName);

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
}
