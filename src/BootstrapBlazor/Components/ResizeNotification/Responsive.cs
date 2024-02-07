// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 响应监听 组件
/// </summary>
public class Responsive : BootstrapComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private ResizeNotificationService? ResizeService { get; set; }

    /// <summary>
    /// 获得/设置 浏览器断点阈值改变时触发 默认 null
    /// </summary>
    [Parameter]
    public Func<BreakPoint, Task>? OnBreakPointChanged { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        ResizeService.Subscribe(this, OnResize);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (OnBreakPointChanged != null)
            {
                await OnBreakPointChanged(ResizeService.CurrentValue);
            }
        }
    }

    /// <summary>
    /// 客户端通知断点已改变
    /// </summary>
    /// <param name="point">断点名称</param>
    /// <returns></returns>
    private async Task OnResize(BreakPoint point)
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
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
