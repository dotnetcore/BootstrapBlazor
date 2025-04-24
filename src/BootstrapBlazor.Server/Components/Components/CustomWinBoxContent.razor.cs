// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    private async Task SetIconWinBox()
    {
        if (Option != null)
        {
            Option.Icon = $"{WebsiteOption.CurrentValue.AssetRootPath}images/Argo-C.png";
            await WinBoxService.SetIcon(Option);
        }
    }

    private async Task SetTitleWinBox()
    {
        if (Option != null)
        {
            Option.Title = $"{DateTime.Now}";
            await WinBoxService.SetTitle(Option);
        }
    }
}
