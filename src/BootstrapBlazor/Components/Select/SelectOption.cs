// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// SelectOption component
/// </summary>
public class SelectOption : ComponentBase
{
    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the option value.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the option is selected. Default is <c>false</c>.
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the option is disabled. Default is <c>false</c>.
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets the group name.
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    [CascadingParameter]
    private ISelect? Container { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Container?.Add(ToSelectedItem());
    }

    /// <summary>
    /// Converts the current instance to a <see cref="SelectedItem"/>.
    /// </summary>
    /// <returns>A <see cref="SelectedItem"/> instance.</returns>
    private SelectedItem ToSelectedItem() => new()
    {
        Active = Active,
        GroupName = GroupName ?? "",
        Text = Text ?? "",
        Value = Value ?? "",
        IsDisabled = IsDisabled
    };
}
