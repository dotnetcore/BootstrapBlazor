﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.DockViews2;

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

    private Task OnVisibleStateChangedAsync(string title, bool status)
    {
        if (title == "标签一")
        {
            Visible = status;
        }
        return Task.CompletedTask;
    }
}
