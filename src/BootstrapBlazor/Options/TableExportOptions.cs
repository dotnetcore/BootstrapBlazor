// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Configuration class for exporting tables to Excel</para>
/// <para lang="en">Configuration class for exporting tables to Excel</para>
/// </summary>
public class TableExportOptions
{
    /// <summary>
    /// <para lang="zh">Gets or sets whether to use formatting. Default is true. If <see cref="TableColumn{TItem, TType}.FormatString"/> or <see cref="TableColumn{TItem, TType}.Formatter"/> is set, the formatted value will be used</para>
    /// <para lang="en">Gets or sets whether to use formatting. Default is true. If <see cref="TableColumn{TItem, TType}.FormatString"/> or <see cref="TableColumn{TItem, TType}.Formatter"/> is set, the formatted value will be used</para>
    /// </summary>
    /// <remarks>Note: After formatting, the returned value is a <code>string</code>, which may change the original value type.</remarks>
    public bool EnableFormat { get; set; } = true;

    /// <summary>
    /// <para lang="zh">Gets or sets whether to use Lookup. Default is true</para>
    /// <para lang="en">Gets or sets whether to use Lookup. Default is true</para>
    /// </summary>
    public bool EnableLookup { get; set; } = true;

    /// <summary>
    /// <para lang="zh">Gets or sets whether to merge array-type values. Default is true</para>
    /// <para lang="en">Gets or sets whether to merge array-type values. Default is true</para>
    /// </summary>
    public bool AutoMergeArray { get; set; } = true;

    /// <summary>
    /// <para lang="zh">Gets or sets whether to use the <see cref="DescriptionAttribute"/> tag for enum types. Default is true</para>
    /// <para lang="en">Gets or sets whether to use the <see cref="DescriptionAttribute"/> tag for enum types. Default is true</para>
    /// </summary>
    public bool UseEnumDescription { get; set; } = true;

    /// <summary>
    /// <para lang="zh">Gets or sets the delimiter used when merging array-type values. Default is a comma</para>
    /// <para lang="en">Gets or sets the delimiter used when merging array-type values. Default is a comma</para>
    /// </summary>
    /// <remarks>Note: After formatting, the returned value is a <code>string</code>, which may change the original value type.</remarks>
    public string ArrayDelimiter { get; set; } = ",";

    /// <summary>
    /// <para lang="zh">Gets or sets whether to enable Excel auto-filtering. Default is true</para>
    /// <para lang="en">Gets or sets whether to enable Excel auto-filtering. Default is true</para>
    /// </summary>
    public bool EnableAutoFilter { get; set; } = true;

    /// <summary>
    /// <para lang="zh">Gets or sets whether to enable Excel auto-width. Default is false</para>
    /// <para lang="en">Gets or sets whether to enable Excel auto-width. Default is false</para>
    /// </summary>
    public bool EnableAutoWidth { get; set; }
}
