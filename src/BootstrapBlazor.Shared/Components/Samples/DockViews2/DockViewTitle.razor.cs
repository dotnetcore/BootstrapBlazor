// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.DockViews2;

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
