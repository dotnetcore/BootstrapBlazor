// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Configuration class for exporting tables to Excel
/// </summary>
public class TableExportOptions
{
    /// <summary>
    /// Gets or sets whether to use formatting. Default is true. 
    /// If <see cref="TableColumn{TItem, TType}.FormatString"/> or <see cref="TableColumn{TItem, TType}.Formatter"/> is set, the formatted value will be used.
    /// </summary>
    /// <remarks>Note: After formatting, the returned value is a <code>string</code>, which may change the original value type.</remarks>
    public bool EnableFormat { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use Lookup. Default is true.
    /// </summary>
    public bool EnableLookup { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to merge array-type values. Default is true.
    /// </summary>
    public bool AutoMergeArray { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use the <see cref="DescriptionAttribute"/> tag for enum types. Default is true.
    /// </summary>
    public bool UseEnumDescription { get; set; } = true;

    /// <summary>
    /// Gets or sets the delimiter used when merging array-type values. Default is a comma.
    /// </summary>
    /// <remarks>Note: After formatting, the returned value is a <code>string</code>, which may change the original value type.</remarks>
    public string ArrayDelimiter { get; set; } = ",";

    /// <summary>
    /// Gets or sets whether to enable Excel auto-filtering. Default is true.
    /// </summary>
    public bool EnableAutoFilter { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to enable Excel auto-width. Default is false.
    /// </summary>
    public bool EnableAutoWidth { get; set; }
}
