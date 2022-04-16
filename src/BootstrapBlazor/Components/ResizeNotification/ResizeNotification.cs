// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 网页尺寸变化通知组件
/// </summary>
public class ResizeNotification : BootstrapComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private ResizeNotificationService? ResizeService { get; set; }

    private JSInterop<ResizeNotification>? Interop { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Interop = new JSInterop<ResizeNotification>(JSRuntime);
            var point = await Interop.InvokeAsync<BreakPoint>(this, null, "bb_resize_monitor", nameof(OnResize));
            await OnResize(point);
        }
    }

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task OnResize(BreakPoint point) => ResizeService.InvokeAsync(point);

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
            }
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <returns></returns>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
