// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.DockViews;

/// <summary>
/// DockView 列布局示例代码
/// </summary>
public partial class DockViewCol
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DockViewCol>? Localizer { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private void OnClickGear()
    {
        ToastService.Success("DockView", "Trigger click event from gear icon");
    }
}
