// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public partial class ResultDialogDemo : ComponentBase, IResultDialog
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public int Value { get; set; } = 1;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public async Task OnClose(DialogResult result)
    {
        if (result == DialogResult.Yes)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}
