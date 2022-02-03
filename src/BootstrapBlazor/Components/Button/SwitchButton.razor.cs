// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class SwitchButton
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? OnText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? OffText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public bool ToggleState { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<bool> ToggleStateChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    private async Task Toggle()
    {
        ToggleState = !ToggleState;
        if (ToggleStateChanged.HasDelegate)
        {
            await ToggleStateChanged.InvokeAsync(ToggleState);
        }
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }

    private string? GetText() => ToggleState ? OnText : OffText;
}
