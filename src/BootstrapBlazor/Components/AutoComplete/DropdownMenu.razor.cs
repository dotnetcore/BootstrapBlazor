// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// DropdownMenu component
/// </summary>
public partial class DropdownMenu
{
    /// <summary>
    /// Get or set the dropdown menu items
    /// </summary>
    [Parameter, NotNull, EditorRequired]
    public List<string>? Rows { get; set; }

    /// <summary>
    /// Get or set the dropdown menu item template default value is null.
    /// </summary>
    [Parameter]
    public RenderFragment<string>? ItemTemplate { get; set; }

    /// <summary>
    /// Gets or sets whether to show the no matching data option, default is true
    /// </summary>
    [Parameter]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    /// Gets or sets Display prompt message when there is no matching data. The default prompt is "No Data"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NoDataTip { get; set; }

    /// <summary>
    /// Gets or sets Callback method when a candidate item is clicked
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnItemClick { get; set; }

    private Task OnClick(string val)
    {
        if (OnItemClick != null)
        {
            return OnItemClick.Invoke(val);
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Render method
    /// </summary>
    public void Render(List<string> items)
    {
        Rows = items;
        StateHasChanged();
    }
}
