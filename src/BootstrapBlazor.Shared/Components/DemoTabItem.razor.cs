// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Components;

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
