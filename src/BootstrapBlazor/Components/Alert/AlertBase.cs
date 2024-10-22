// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Alert 警告框组件
/// </summary>
public abstract class AlertBase : BootstrapComponentBase
{
    /// <summary>
    /// 获得 图标样式字符串
    /// </summary>
    protected string? IconString => CssBuilder.Default("alert-icon")
        .AddClass(Icon)
        .Build();

    /// <summary>
    /// 获得/设置 颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示关闭按钮
    /// </summary>
    [Parameter]
    public bool ShowDismiss { get; set; }

    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示左侧 Bar
    /// </summary>
    [Parameter]
    public bool ShowBar { get; set; }

    /// <summary>
    /// 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 关闭警告框回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnDismiss { get; set; }
}
