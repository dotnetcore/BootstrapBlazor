// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// TooltipContent 组件用于显示 Tooltip 的内容
/// </summary>
public partial class TooltipContent
{
    [CascadingParameter]
    private Tooltip? Tooltip { get; set; }

    private async Task ToggleShow()
    {
        if (Tooltip == null)
        {
            return;
        }

        await Tooltip.Toggle();
    }
}
