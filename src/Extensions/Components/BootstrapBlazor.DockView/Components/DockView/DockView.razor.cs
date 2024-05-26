// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class DockView
{
    /// <summary>
    /// 
    /// </summary>
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private List<DockContent2>? Content { get; set; } = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await InvokeVoidAsync("init", Id, Content);
        }
    }
}
