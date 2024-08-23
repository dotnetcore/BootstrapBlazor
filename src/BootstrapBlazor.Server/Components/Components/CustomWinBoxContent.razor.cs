// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// CustomWinBoxContent 组件
/// </summary>
public partial class CustomWinBoxContent
{
    [Inject, NotNull]
    private WinBoxService? WinBoxService { get; set; }

    /// <summary>
    /// WinBoxOption 实例
    /// </summary>
    [Parameter, NotNull]
    public WinBoxOption? Option { get; set; }

    private Task StackWinBox() => WinBoxService.Stack();

    private async Task MinWinBox()
    {
        if (Option != null)
        {
            await WinBoxService.Minimize(Option);
        }
    }

    private async Task MaxWinBox()
    {
        if (Option != null)
        {
            await WinBoxService.Maximize(Option);
        }
    }

    private async Task RestoreWinBox()
    {
        if (Option != null)
        {
            await WinBoxService.Restore(Option);
        }
    }
}
