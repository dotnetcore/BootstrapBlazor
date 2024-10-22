﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuTrigger 组件
/// </summary>
public class ContextMenuTrigger : BootstrapComponentBase
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
        builder.AddMultipleAttributes(10, AdditionalAttributes);
        builder.AddAttribute(20, "class", ClassString);
        builder.AddAttribute(30, "oncontextmenu", EventCallback.Factory.Create<MouseEventArgs>(this, OnContextMenu));
        builder.AddAttribute(35, "ontouchstart", EventCallback.Factory.Create<TouchEventArgs>(this, OnTouchStart));
        builder.AddAttribute(36, "ontouchend", EventCallback.Factory.Create<TouchEventArgs>(this, OnTouchEnd));
        builder.AddEventPreventDefaultAttribute(40, "oncontextmenu", true);
        builder.AddContent(50, ChildContent);
        builder.CloseElement();
    }

    /// <summary>
    /// 点击 ContextMenu 菜单项时触发
    /// </summary>
    /// <returns></returns>
    public Task OnContextMenu(MouseEventArgs args) => ContextMenuZone.OnContextMenu(args, ContextItem);

    /// <summary>
    /// 是否触摸
    /// </summary>
    private bool TouchStart { get; set; }

    /// <summary>
    /// 触摸定时器工作指示
    /// </summary>
    private bool IsBusy { get; set; }

    private async Task OnTouchStart(TouchEventArgs e)
    {
        if (!IsBusy)
        {
            IsBusy = true;
            TouchStart = true;

            // 延时保持 TouchStart 状态
            await Task.Delay(200);
            if (TouchStart)
            {
                var args = new MouseEventArgs()
                {
                    ClientX = e.Touches[0].ClientX,
                    ClientY = e.Touches[0].ClientY,
                    ScreenX = e.Touches[0].ScreenX,
                    ScreenY = e.Touches[0].ScreenY,
                };
                // 弹出关联菜单
                await OnContextMenu(args);

                //延时防止重复激活菜单功能
                await Task.Delay(200);
            }
            IsBusy = false;
        }
    }

    private void OnTouchEnd()
    {
        TouchStart = false;
    }
}
