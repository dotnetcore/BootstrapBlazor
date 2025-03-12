// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Nullable boolean type converter
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class NullableBoolItemsAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the display text for null value
    /// </summary>
    public string? NullValueDisplayText { get; set; }

    /// <summary>
    /// Gets or sets the display text for true value
    /// </summary>
    public string? TrueValueDisplayText { get; set; }

    /// <summary>
    /// Gets or sets the display text for false value
    /// </summary>
    public string? FalseValueDisplayText { get; set; }
}
