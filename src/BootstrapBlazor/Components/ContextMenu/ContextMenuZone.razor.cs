// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuZone 组件
/// </summary>
public partial class ContextMenuZone : IDisposable
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 上下文菜单组件集合
    /// </summary>
    private ContextMenu? ContextMenu { get; set; }

    private string? ClassString => CssBuilder.Default("bb-cm-zone")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 是否触摸
    /// </summary>
    private bool TouchStart { get; set; }

    /// <summary>
    /// 触摸定时器工作指示
    /// </summary>
    private bool IsBusy { get; set; }

    private CancellationTokenSource CancellationSource { get; } = new();

    /// <summary>
    /// Trigger 调用
    /// </summary>
    /// <param name="args"></param>
    /// <param name="contextItem"></param>
    /// <returns></returns>
    internal async Task OnContextMenu(MouseEventArgs args, object? contextItem)
    {
        // 弹出关联菜单
        if (ContextMenu != null)
        {
            await ContextMenu.Show(args, contextItem);
        }
    }

    /// <summary>
    /// ContextMenu 组件调用
    /// </summary>
    /// <param name="contextMenu"></param>
    internal void RegisterContextMenu(ContextMenu contextMenu) => ContextMenu = contextMenu;

    /// <summary>
    /// 触摸事件
    /// </summary>
    /// <param name="e"></param>
    private async Task OnTouchStart(TouchEventArgs e)
    {
        if (!IsBusy)
        {
            IsBusy = true;
            TouchStart = true;
            var args = new MouseEventArgs()
            {
                ClientX = e.Touches[0].ClientX,
                ClientY = e.Touches[0].ClientY,
                ScreenX = e.Touches[0].ScreenX,
                ScreenY = e.Touches[0].ScreenY,
            };

            try
            {
                await Task.Delay(200, CancellationSource.Token);
                if (TouchStart)
                {
                    // 弹出关联菜单
                    if (ContextMenu != null)
                    {
                        await ContextMenu.Show(args, null);
                    }
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
