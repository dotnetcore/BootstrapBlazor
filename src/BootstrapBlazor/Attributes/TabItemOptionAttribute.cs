// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TabItem configuration attribute class
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TabItemOptionAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the text of the tab item.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets whether the current TabItem is closable. Default is true.
    /// </summary>
    public bool Closable { get; set; } = true;

    /// <summary>
    /// Gets or sets the icon string.
    /// </summary>
    public string? Icon { get; set; }
}
