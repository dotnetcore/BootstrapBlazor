// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">导出表格到 Excel 的配置类</para>
/// <para lang="en">Configuration class for exporting tables to Excel</para>
/// </summary>
public class TableExportOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否使用格式化。默认为 true。若设置了 <see cref="TableColumn{TItem, TType}.FormatString"/> 或 <see cref="TableColumn{TItem, TType}.Formatter"/> 将使用格式化后的值</para>
    /// <para lang="en">Gets or sets whether to use formatting. Default is true. If <see cref="TableColumn{TItem, TType}.FormatString"/> or <see cref="TableColumn{TItem, TType}.Formatter"/> is set, the formatted value will be used</para>
    /// </summary>
    public bool EnableFormat { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否使用 Lookup。默认为 true</para>
    /// <para lang="en">Gets or sets whether to use Lookup. Default is true</para>
    /// </summary>
    public bool EnableLookup { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否合并数组类型的值。默认为 true</para>
    /// <para lang="en">Gets or sets whether to merge array-type values. Default is true</para>
    /// </summary>
    public bool AutoMergeArray { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否对枚举类型使用 <see cref="DescriptionAttribute"/> 标记。默认为 true</para>
    /// <para lang="en">Gets or sets whether to use the <see cref="DescriptionAttribute"/> tag for enum types. Default is true</para>
    /// </summary>
    public bool UseEnumDescription { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 合并数组类型的值时使用的分隔符。默认值为逗号</para>
    /// <para lang="en">Gets or sets the delimiter used when merging array-type values. Default is a comma</para>
    /// </summary>
    public string ArrayDelimiter { get; set; } = ",";

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用 Excel 自动筛选。默认为 true</para>
    /// <para lang="en">Gets or sets whether to enable Excel auto-filtering. Default is true</para>
    /// </summary>
    public bool EnableAutoFilter { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用 Excel 自动列宽。默认为 false</para>
    /// <para lang="en">Gets or sets whether to enable Excel auto-width. Default is false</para>
    /// </summary>
    public bool EnableAutoWidth { get; set; }
}
