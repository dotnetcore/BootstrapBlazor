// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 网页尺寸变化通知组件
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor/modules/resposive.js", JSObjectReference = true)]
public class ResizeNotification : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private ResizeNotificationService? ResizeService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task ModuleInitAsync()
    {
        if (Module != null)
        {
            await InvokeInitAsync(Id, nameof(OnResize));
            var point = await Module.InvokeAsync<BreakPoint>($"{ModuleName}.getResponsive");
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
}
