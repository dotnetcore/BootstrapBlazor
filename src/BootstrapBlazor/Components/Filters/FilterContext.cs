// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.CompilerServices;

namespace BootstrapBlazor.Components;

/// <summary>
/// FilterContext class
/// </summary>
public class FilterContext
{
    /// <summary>
    /// Gets or sets whether the filter is header row. Default is false.
    /// </summary>
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// Gets or sets the column field key. Default is null.
    /// </summary>
    public string? FieldKey { get; set; }

    /// <summary>
    /// Gets or sets the filter counter. Default is 0.
    /// </summary>
    public int Count { get; set; }
}
