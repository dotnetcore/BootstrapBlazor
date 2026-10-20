// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Components;

/// <summary>
/// DemoTabItem 组件
/// </summary>
public partial class DemoTabItem
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DemoTabItem>? Localizer { get; set; }

    /// <summary>
    /// OnSetTitle 回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnSetTitle { get; set; }

    private async Task OnClick()
    {
        if (OnSetTitle != null)
        {
            await OnSetTitle(DateTime.Now.ToString("mm:ss"));
        }
    }
}
