// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class GotoNavigator
{
    /// <summary>
    /// 获得/设置 跳转页码 默认 null
    /// </summary>
    [Parameter]
    public int Index { get; set; }

    /// <summary>
    /// 获得/设置 跳转文本 默认 null
    /// </summary>
    [Parameter]
    public string? GotoText { get; set; }

    /// <summary>
    /// 获得/设置 导航回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnNavigation { get; set; }

    private async Task OnValueChanged(int val)
    {
        Index = val;
        if (OnNavigation != null)
        {
            await OnNavigation(Index);
        }
    }

    private async Task OnKeyup(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            await OnValueChanged(Index);
        }
    }
}
