// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewIcon 组件
/// </summary>
public partial class DockViewIcon
{
    /// <summary>
    /// 获得/设置 图标名称
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? IconName { get; set; }

    private string? ClassString => CssBuilder.Default("bb-dockview-control-icon")
        .AddClass($"bb-dockview-control-icon-{IconName}", !string.IsNullOrEmpty(IconName))
        .Build();

    private string _href => $"./_content/BootstrapBlazor.DockView/icon/dockview.svg#{IconName}";
}
