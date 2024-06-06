// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewTitle 组件
/// </summary>
public class DockViewTitle : ComponentBase
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "dv-default-tab-action bb-dock-view-item-title-icon");

        if (OnClickBarCallback != null)
        {
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, OnClickBarCallback));
        }

        if (!string.IsNullOrEmpty(BarIconUrl))
        {
            builder.AddContent(3, new MarkupString($"<img alt=\"bar-icon\" src=\"{BarIconUrl}\" />"));
        }
        else if (!string.IsNullOrEmpty(BarIcon))
        {
            builder.AddContent(4, new MarkupString($"<i class=\"{BarIcon}\"></i>"));
        }
        else
        {
            builder.AddContent(5, new MarkupString("<svg viewBox=\"0 0 48 48\" fill=\"none\" class=\"bb-dockview-control-icon\"><path d=\"M7.94971 11.9497H39.9497\" stroke=\"#333\" stroke-width=\"3\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/><path d=\"M7.94971 23.9497H39.9497\" stroke=\"#333\" stroke-width=\"3\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/><path d=\"M7.94971 35.9497H39.9497\" stroke=\"#333\" stroke-width=\"3\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/></svg>"));
        }

        builder.CloseElement();
    }
}
