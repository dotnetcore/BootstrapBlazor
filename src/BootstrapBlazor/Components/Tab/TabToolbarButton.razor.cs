// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TabToolbarButton component
/// </summary>
public partial class TabToolbarButton
{
    /// <summary>
    /// Gets or sets the button icon string. Default is null.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the button click event handler. Default is null.
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickAsync { get; set; }

    /// <summary>
    /// Gets or sets the tooltip text. Default is null.
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    private string? ClassString => CssBuilder.Default("tabs-nav-toolbar-button")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnClick()
    {
        if (OnClickAsync != null)
        {
            await OnClickAsync();
        }
    }
}
