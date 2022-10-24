// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 响应监听 组件
/// </summary>
public class Responsive : BootstrapComponentBase
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
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        ResizeService.Subscribe(this, OnResize);
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
}
