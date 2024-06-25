// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews;

/// <summary>
/// DockViewVisible 示例
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

    private Task OnVisibleStateChangedAsync(string title, bool visible)
    {
        if (title == "标签一")
        {
            Visible = visible;
        }
        return Task.CompletedTask;
    }
}
