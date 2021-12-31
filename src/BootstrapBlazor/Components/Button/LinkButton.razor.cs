// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// LinkButton 组件
/// </summary>
public partial class LinkButton
{
    /// <summary>
    /// 获得/设置 显示文本 默认为 null
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 Url 默认为 #
    /// </summary>
    [Parameter]
    public string Url { get; set; } = "#";

    /// <summary>
    /// 获得/设置 Tooltip 显示文字 默认为 null
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 显示图片地址 默认为 null
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 获得/设置 Tooltip 显示位置 默认为 Top
    /// </summary>
    [Parameter]
    public Placement TooltipPlacement { get; set; } = Placement.Top;

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 点击事件回调方法
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    private bool Prevent => Url.StartsWith('#');
}
