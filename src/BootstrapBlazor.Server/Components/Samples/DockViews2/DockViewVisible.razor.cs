// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// 可视化示例代码
/// </summary>
public partial class DockViewVisible
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DockViewVisible>? Localizer { get; set; }

    private bool Visible { get; set; } = true;

    private void OnToggleVisible()
    {
        Visible = !Visible;
    }

    private Task OnPanelClosedCallbackAsync(string title)
    {
        if (title == "标签一")
        {
            Visible = false;
        }
        return Task.CompletedTask;
    }
}
