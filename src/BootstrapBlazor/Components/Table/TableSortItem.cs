// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 排序项 (高级排序使用)
/// </summary>
public class TableSortItem
{
    /// <summary>
    /// 排序字段名 默认 string.Empty
    /// </summary>
    public string SortName { get; set; } = string.Empty;

    /// <summary>
    /// 排序顺序 默认 SortOrder.Unset
    /// </summary>
    public SortOrder SortOrder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var order = SortOrder == SortOrder.Unset ? "" : SortOrder.ToString();
        return $"{SortName} {order}";
    }
}

