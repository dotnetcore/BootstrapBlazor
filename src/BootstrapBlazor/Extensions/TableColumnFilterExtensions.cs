// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableFilter extensions methods</para>
/// <para lang="en">TableFilter extensions methods</para>
/// </summary>
public static class TableColumnFilterExtensions
{
    /// <summary>
    /// <para lang="zh">Whether has filter</para>
    /// <para lang="en">Whether has filter</para>
    /// </summary>
    /// <param name="filter"></param>
    public static bool HasFilter(this TableColumnFilter? filter)
    {
        if (filter == null)
        {
            return false;
        }
        return filter.Table.Filters.ContainsKey(filter.Column.GetFieldName());
    }

    /// <summary>
    /// <para lang="zh">Whether is header row</para>
    /// <para lang="en">Whether is header row</para>
    /// </summary>
    /// <param name="filter"></param>
    public static bool IsHeaderRow(this TableColumnFilter? filter)
    {
        if (filter == null)
        {
            return false;
        }
        return filter.IsHeaderRow;
    }

    /// <summary>
    /// <para lang="zh">获得 the field key for the filter</para>
    /// <para lang="en">Gets the field key for the filter</para>
    /// </summary>
    /// <param name="filter"></param>
    public static string GetFieldKey(this TableColumnFilter? filter)
    {
        if (filter == null)
        {
            return string.Empty;
        }
        return filter.Column.GetFieldName();
    }

    /// <summary>
    /// <para lang="zh">获得 the filter title</para>
    /// <para lang="en">Gets the filter title</para>
    /// </summary>
    /// <param name="filter"></param>
    public static string GetFilterTitle(this TableColumnFilter? filter)
    {
        if (filter == null)
        {
            return string.Empty;
        }
        return filter.Column.GetDisplayName();
    }
}
