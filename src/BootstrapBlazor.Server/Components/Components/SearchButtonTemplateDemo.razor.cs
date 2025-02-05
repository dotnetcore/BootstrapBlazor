// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// SearchButtonTemplateDemo 示例组件
/// </summary>
public partial class SearchButtonTemplateDemo
{
    [CascadingParameter(Name = "OnSearch"), NotNull]
    private Func<Task>? OnSearch { get; set; }

    [CascadingParameter(Name = "OnClear"), NotNull]
    private Func<Task>? OnClear { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private async Task OnClickSearch()
    {
        if (OnSearch != null)
        {
            await OnSearch();
        }

        await ToastService.Information("Search-ButtonTemplate", "Click Search1 Button");
    }

    private async Task OnClickClear()
    {
        if (OnClear != null)
        {
            await OnClear();
        }

        await ToastService.Information("Search-ButtonTemplate", "Click Clear1 Button");
    }
}
