// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
