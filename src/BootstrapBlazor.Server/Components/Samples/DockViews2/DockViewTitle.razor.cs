// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// DockViewTitle 示例
/// </summary>
public partial class DockViewTitle
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DockViewTitle>? Localizer { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private Task OnClickTitleBarCallback() => ToastService.Success("事件回调", "点击标题图标回调方法");
}
