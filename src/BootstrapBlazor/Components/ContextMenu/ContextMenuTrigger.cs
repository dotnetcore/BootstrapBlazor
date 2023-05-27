// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuTrigger 组件
/// </summary>
public class ContextMenuTrigger : IdComponentBase
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 包裹组件 TagName 默认为 div
    /// </summary>
    [Parameter]
    public string WrapperTag { get; set; } = "div";

    /// <summary>
    /// 获得/设置 是否停止广播 默认 true
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; } = true;

    /// <summary>
    /// 获得/设置 上下文数据
    /// </summary>
    [Parameter]
    public object? ContextItem { get; set; }

    [CascadingParameter]
    [NotNull]
    private ContextMenuZone? ContextMenuZone { get; set; }

    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, WrapperTag);
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "id", Id);
        builder.AddAttribute(3, "class", ClassString);
        builder.AddAttribute(4, "oncontextmenu", EventCallback.Factory.Create<MouseEventArgs>(this, OnContextMenu));
        builder.AddEventPreventDefaultAttribute(5, "oncontextmenu", true);
        builder.AddContent(6, ChildContent);
        builder.CloseElement();
    }

    /// <summary>
    /// 点击 ContextMenu 菜单项时触发
    /// </summary>
    /// <returns></returns>
    public Task OnContextMenu(MouseEventArgs args) => ContextMenuZone.OnContextMenu(Id, args, ContextItem);
}
