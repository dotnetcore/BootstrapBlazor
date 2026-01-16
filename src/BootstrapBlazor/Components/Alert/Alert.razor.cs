// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Alert 组件</para>
/// <para lang="en">Alert component</para>
/// </summary>
public partial class Alert
{
    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Gets the class name</para>
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
    /// <para lang="zh">获得/设置 是否显示阴影 默认 false 不显示</para>
    /// <para lang="en">Gets or sets whether to show shadow. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示边框 默认 false 不显示</para>
    /// <para lang="en">Gets or sets whether to show border. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowBorder { get; set; }

    private async Task OnClick()
    {
        IsShown = !IsShown;
        if (OnDismiss != null) await OnDismiss();
    }
}
