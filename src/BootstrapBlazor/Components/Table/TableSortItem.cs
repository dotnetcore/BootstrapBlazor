// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">排序项（高级排序使用）</para>
/// <para lang="en">Sort item (used in advanced sorting)</para>
/// </summary>
public class TableSortItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 排序字段名称，默认为空字符串</para>
    /// <para lang="en">Gets or sets the sort field name. Default value is empty string</para>
    /// </summary>
    public string SortName { get; set; } = string.Empty;

    /// <summary>
    /// <para lang="zh">获得/设置 排序顺序，默认为 SortOrder.Unset</para>
    /// <para lang="en">Gets or sets the sort order. Default value is SortOrder.Unset</para>
    /// </summary>
    public SortOrder SortOrder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string ToString()
    {
        var order = SortOrder == SortOrder.Unset ? "" : SortOrder.ToString();
        return $"{SortName} {order}";
    }
}

