// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewIcon 组件
/// </summary>
public partial class DockViewIcon
{
    [Inject, NotNull]
    private IStringLocalizer<DockViewIcon>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 图标名称
    /// </summary>
    [Parameter, NotNull]
    [EditorRequired]
    public string? IconName { get; set; }

    private string? ClassString => CssBuilder.Default("bb-dockview-control-icon")
        .AddClass($"bb-dockview-control-icon-{IconName}")
        .Build();

    private string _href => $"./_content/BootstrapBlazor.DockView/icon/dockview.svg#{IconName}";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        IconName ??= "close";
    }
}
