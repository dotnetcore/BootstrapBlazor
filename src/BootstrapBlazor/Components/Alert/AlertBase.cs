// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Alert 警告框组件</para>
/// <para lang="en">Alert component</para>
/// </summary>
public abstract class AlertBase : BootstrapComponentBase
{
    /// <summary>
    /// <para lang="zh">获得 图标样式字符串</para>
    /// <para lang="en">Gets the icon class string</para>
    /// </summary>
    protected string? IconString => CssBuilder.Default("alert-icon")
        .AddClass(Icon)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 颜色</para>
    /// <para lang="en">Gets or sets the color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮</para>
    /// <para lang="en">Gets or sets whether to show the dismiss button</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowDismiss { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示左侧 Bar</para>
    /// <para lang="en">Gets or sets whether to show the left bar</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowBar { get; set; }

    /// <summary>
    /// <para lang="zh">子组件</para>
    /// <para lang="en">Child content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">关闭警告框回调方法</para>
    /// <para lang="en">Callback method when the alert is dismissed</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnDismiss { get; set; }
}
