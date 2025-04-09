// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Badge component
/// </summary>
public partial class Badge
{
    private string? ClassString => CssBuilder.Default("badge")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("rounded-pill", IsPill)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Gets or sets the color of the badge. Default is <see cref="Color.Primary"/>.
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// Gets or sets whether the badge should be displayed as a pill (rounded) or not. Default is false.
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsPill { get; set; }

    /// <summary>
    /// Gets or sets the child content of the component. Default is false.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
