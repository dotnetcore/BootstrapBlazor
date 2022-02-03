// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Pages;

/// <summary>
/// 
/// </summary>
public sealed partial class Coms
{
    private List<string> ComponentItems { get; set; } = new List<string>();

    private string? SearchText { get; set; }

    private Task OnSearch(string searchText)
    {
        SearchText = searchText;

        StateHasChanged();
        return Task.CompletedTask;
    }
}
