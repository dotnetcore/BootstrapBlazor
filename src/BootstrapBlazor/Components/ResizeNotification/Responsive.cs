// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 响应监听 组件
/// </summary>
public class Responsive : ComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private ResizeNotificationService? ResizeService { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    /// <summary>
    /// 获得/设置 是否触发内容刷新 返回 true 时刷新
    /// </summary>
    [Parameter]
    public Func<BreakPoint, Task>? OnBreakPointChanged { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        ResizeService.Subscribe(this, OnResize);
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var point = await JSRuntime.InvokeAsync<BreakPoint>(func: "bb_get_responsive");
            await OnResize(point);
        }
    }

    /// <summary>
    /// 客户端通知断点已改变
    /// </summary>
    /// <param name="point">断点名称</param>
    /// <returns></returns>
    [JSInvokable()]
    public async Task OnResize(BreakPoint point)
    {
        if (OnBreakPointChanged != null)
        {
            await OnBreakPointChanged(point);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ResizeService.Unsubscribe(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
