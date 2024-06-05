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
        builder.AddAttribute(1, "class", "bb-dock-view-item-title-icon");

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
            builder.AddContent(5, new MarkupString("<svg viewBox=\"0 0 1024 1024\" width=\"11\" height=\"11\"><path d=\"M64 161.9h896v49.8H64zM64 488.8h896v49.8H64zM64 812.3h896v49.8H64z\"></path></svg>"));
        }

        builder.CloseElement();
    }
}
