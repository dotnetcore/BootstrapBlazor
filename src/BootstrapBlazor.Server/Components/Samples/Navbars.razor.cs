// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Navbar 示例
/// </summary>
public partial class Navbars
{
    private List<SelectedItem> _dropdownItems = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _dropdownItems.AddRange([
            new SelectedItem() { Text="Item1", Value="0"},
            new SelectedItem() { Text="Item2", Value="1"},
            new SelectedItem() { Text="Item3", Value="2"}
        ]);
    }
}
