// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuTrigger 组件
/// </summary>
public class ContextMenuTrigger : BootstrapComponentBase, IDisposable
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

    private CancellationTokenSource CancellationSource { get; } = new();

    private async Task OnTouchStart(TouchEventArgs e)
    {
        if (!IsBusy)
        {
            IsBusy = true;
            TouchStart = true;

            try
            {
                // 延时保持 TouchStart 状态
                await Task.Delay(200, CancellationSource.Token);
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
                    await Task.Delay(200, CancellationSource.Token);
                }
                IsBusy = false;
            }
            catch (TaskCanceledException) { }
        }
    }

    private void OnTouchEnd()
    {
        TouchStart = false;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            CancellationSource.Cancel();
            CancellationSource.Dispose();
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <returns></returns>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
