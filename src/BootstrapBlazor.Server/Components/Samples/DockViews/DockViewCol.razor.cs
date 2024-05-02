// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews;

/// <summary>
/// DockView 列布局示例代码
/// </summary>
public partial class DockViewCol
{
    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private void OnClickGear()
    {
        ToastService.Success("DockView", "Trigger click event from gear icon");
    }
}
