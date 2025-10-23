// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// Contributor
/// </summary>
public sealed class Contributor
{
    /// <summary>
    /// Gets or sets Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets Avatar
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Gets or sets Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets Sheet data
    /// </summary>
    public UniverSheetData? Data { get; set; }
}
