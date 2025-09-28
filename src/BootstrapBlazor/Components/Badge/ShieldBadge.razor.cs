// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ShieldBadge component
/// </summary>
public partial class ShieldBadge
{
    /// <summary>
    /// Gets or sets the icon. Default is null.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the icon color. Default is null.
    /// </summary>
    [Parameter]
    public string? IconColor { get; set; }

    /// <summary>
    /// Gets or sets the text of badge. Default is null.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the text color. Default is null.
    /// </summary>
    [Parameter]
    public string? TextColor { get; set; }

    /// <summary>
    /// Gets or sets the text background color. Default is null.
    /// </summary>
    [Parameter]
    public string? TextBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the label of badge. Default is null.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the label color of badge. Default is null.
    /// </summary>
    [Parameter]
    public string? LabelColor { get; set; }

    /// <summary>
    /// Gets or sets the label background color. Default is null.
    /// </summary>
    [Parameter]
    public string? LabelBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the badge radius. Default is 3.
    /// </summary>
    [Parameter]
    public int Radius { get; set; } = 3;

    private string? ClassString => CssBuilder.Default("shield-badge")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-shield-badge-icon-color: {IconColor};", !string.IsNullOrEmpty(IconColor))
        .AddClass($"--bb-shield-badge-label-color: {LabelColor};", !string.IsNullOrEmpty(LabelColor))
        .AddClass($"--bb-shield-badge-label-bg: {LabelBackgroundColor};", !string.IsNullOrEmpty(LabelBackgroundColor))
        .AddClass($"--bb-shield-badge-text-color: {TextColor};", !string.IsNullOrEmpty(TextColor))
        .AddClass($"--bb-shield-badge-text-bg: {TextBackgroundColor};", !string.IsNullOrEmpty(TextBackgroundColor))
        .AddClass($"--bb-shield-badge-radius: {Math.Max(0, Radius)}px;", Radius != 3)
        .Build();

    private string? IconString => CssBuilder.Default("shield-badge-icon")
        .AddClass(Icon, !string.IsNullOrEmpty(Icon))
        .Build();
}
