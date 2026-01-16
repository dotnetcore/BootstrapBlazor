// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">排序项 (高级排序使用)</para>
///  <para lang="en">排序项 (高级排序使用)</para>
/// </summary>
public class TableSortItem
{
    /// <summary>
    ///  <para lang="zh">排序字段名 默认 string.Empty</para>
    ///  <para lang="en">排序字段名 Default is string.Empty</para>
    /// </summary>
    public string SortName { get; set; } = string.Empty;

    /// <summary>
    ///  <para lang="zh">排序顺序 默认 SortOrder.Unset</para>
    ///  <para lang="en">排序顺序 Default is SortOrder.Unset</para>
    /// </summary>
    public SortOrder SortOrder { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var order = SortOrder == SortOrder.Unset ? "" : SortOrder.ToString();
        return $"{SortName} {order}";
    }
}

