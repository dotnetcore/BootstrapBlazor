// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TableFilter extensions methods
/// </summary>
public static class TableFilterExtensions
{
    /// <summary>
    /// Whether has filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static bool HasFilter(this TableFilter? filter)
    {
        if (filter == null)
        {
            return false;
        }
        return filter.Table.Filters.ContainsKey(filter.Column.GetFieldName());
    }

    /// <summary>
    /// Whether is header row
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static bool IsHeaderRow(this TableFilter? filter)
    {
        if (filter == null)
        {
            return false;
        }
        return filter.IsHeaderRow;
    }

    /// <summary>
    /// Gets the field key for the filter.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static string GetFieldKey(this TableFilter? filter)
    {
        if (filter == null)
        {
            return string.Empty;
        }
        return filter.Column.GetFieldName();
    }
}
