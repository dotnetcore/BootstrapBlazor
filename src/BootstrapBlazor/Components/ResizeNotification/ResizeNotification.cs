// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 网页尺寸变化通知组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "responsive", JSObjectReference = true)]
public class ResizeNotification : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private ResizeNotificationService? ResizeService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnResize));

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <param name="pointString"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task OnResize(string pointString)
    {
        var point = BreakPoint.ExtraExtraLarge;
        if (Enum.TryParse<BreakPoint>(pointString, true, out var p))
        {
            point = p;
        }
        return ResizeService.InvokeAsync(point);
    }
}
