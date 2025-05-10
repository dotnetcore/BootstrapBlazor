// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TableExport attribute class
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class TableExportAttribute : Attribute
{
    /// <summary>
    /// Gets or sets whether the current column is ignored when export operation. Default is false. When set to true, the UI will not generate this column.
    /// </summary>
    public bool Ignore { get; set; }
}
