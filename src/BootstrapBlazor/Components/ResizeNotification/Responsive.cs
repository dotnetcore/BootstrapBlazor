// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 响应监听 组件
/// </summary>
[JSModuleAutoLoader(JSObjectReference = true)]
public class Responsive : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private ResizeNotificationService? ResizeService { get; set; }

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
        base.OnInitialized();

        ResizeService.Subscribe(this, OnResize);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task ModuleInitAsync()
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync($"{ModuleName}.init", Id, nameof(OnResize));
            var point = await Module.InvokeAsync<BreakPoint>($"{ModuleName}.getResponsive");
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
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            ResizeService.Unsubscribe(this);
        }

        await base.DisposeAsync(disposing);
    }
}
