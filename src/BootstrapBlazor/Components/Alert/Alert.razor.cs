// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Alert 组件
/// </summary>
public partial class Alert
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("alert fade show")
        .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"border-{Color.ToDescriptionString()}", ShowBorder)
        .AddClass("d-none", !IsShown)
        .AddClass("shadow", ShowShadow)
        .AddClass("alert-bar", ShowBar)
        .AddClass("alert-dismissible", ShowDismiss)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private bool IsShown { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; }

    /// <summary>
    /// 获得/设置 是否显示边框 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowBorder { get; set; }

    private async Task OnClick()
    {
        IsShown = !IsShown;
        if (OnDismiss != null) await OnDismiss();
    }
}
