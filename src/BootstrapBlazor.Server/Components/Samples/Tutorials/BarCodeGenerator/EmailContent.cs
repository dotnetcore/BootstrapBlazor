// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// EmailContent
/// </summary>
public class EmailContent
{
    /// <summary>
    /// Gets or sets Email
    /// </summary>
    [Required]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets Subject
    /// </summary>
    [Required]
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets Message
    /// </summary>
    [Required]
    [AutoGenerateColumn(Rows = 3)]
    public string? Message { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"mailto:{Email}?subject={Uri.EscapeDataString(Subject ?? "")}&body={Uri.EscapeDataString(Message ?? "")}";
    }
}
