// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewTitle 组件
/// </summary>
public partial class DockViewTitleBar
{
    /// <summary>
    /// 获得/设置 标题前置图标点击回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickBarCallback { get; set; }

    /// <summary>
    /// 获得/设置 标题前置图标 默认 null 未设置使用默认图标
    /// </summary>
    [Parameter]
    public string? BarIcon { get; set; }

    /// <summary>
    /// 获得/设置 标题前置图标 Url 默认 null 未设置使用默认图标
    /// </summary>
    [Parameter]
    public string? BarIconUrl { get; set; }

    private async Task OnClickBar()
    {
        if (OnClickBarCallback != null)
        {
            await OnClickBarCallback();
        }
    }
}
