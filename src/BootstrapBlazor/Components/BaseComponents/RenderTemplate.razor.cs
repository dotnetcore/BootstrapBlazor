// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// RenderTemplate component
/// </summary>
public partial class RenderTemplate
{
    /// <summary>
    /// Gets or sets the child component
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the callback delegate for the first load
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnRenderAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (OnRenderAsync != null)
        {
            await OnRenderAsync(firstRender);
        }
    }

    /// <summary>
    /// Render method
    /// </summary>
    public void Render()
    {
        StateHasChanged();
    }
}
