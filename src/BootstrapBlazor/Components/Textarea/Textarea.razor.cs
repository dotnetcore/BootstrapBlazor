// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Textarea component
/// </summary>
public partial class Textarea
{
    /// <summary>
    /// Scroll to the top
    /// </summary>
    /// <returns></returns>
    public Task ScrollToTop() => InvokeVoidAsync("execute", Id, "toTop");

    /// <summary>
    /// Scroll to a specific value
    /// </summary>
    /// <returns></returns>
    public Task ScrollTo(int value) => InvokeVoidAsync("execute", Id, "to", value);

    /// <summary>
    /// Scroll to the bottom
    /// </summary>
    /// <returns></returns>
    public Task ScrollToBottom() => InvokeVoidAsync("execute", Id, "toBottom");

    /// <summary>
    /// Gets or sets whether auto-scroll is enabled. Default is false.
    /// </summary>
    [Parameter]
    public bool IsAutoScroll { get; set; }

    /// <summary>
    /// Gets or sets whether Shift + Enter replaces the default Enter key behavior. Default is false.
    /// </summary>
    [Parameter]
    public bool UseShiftEnter { get; set; }

    /// <summary>
    /// Gets the client-side auto-scroll identifier.
    /// </summary>
    private string? AutoScrollString => IsAutoScroll ? "auto" : null;

    private string? ShiftEnterString => UseShiftEnter ? "true" : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("execute", Id, "update");
        }
    }
}
