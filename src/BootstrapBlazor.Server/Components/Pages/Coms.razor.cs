// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// Coms 组件
/// </summary>
public sealed partial class Coms
{
    private List<string> ComponentItems { get; } = [];

    private string? SearchText { get; set; }

    private Task<IEnumerable<string?>> OnSearch(string searchText)
    {
        SearchText = searchText;
        return Task.FromResult<IEnumerable<string?>>(ComponentItems.Where(i => i.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList());
    }

    private Task OnClear(string searchText)
    {
        SearchText = "";
        StateHasChanged();
        return Task.CompletedTask;
    }
}
