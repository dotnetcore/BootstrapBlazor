// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Anchor 组件部分类
/// </summary>
public partial class Anchor
{
    /// <summary>
    /// 获得/设置 目标组件 Id
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 滚动组件 Id 默认为 null 使用最近滚动条容器元素
    /// </summary>
    [Parameter]
    public string? Container { get; set; }

    /// <summary>
    /// 获得/设置 滚动时是否开启动画 默认 true
    /// </summary>
    [Parameter]
    public bool IsAnimation { get; set; } = true;

    /// <summary>
    /// 获得 滚动动画
    /// </summary>
    protected string? AnimationString => IsAnimation ? "true" : null;

    /// <summary>
    /// 获得/设置 距离顶端偏移量 默认为 0
    /// </summary>
    [Parameter]
    public int Offset { get; set; }

    /// <summary>
    /// 获得/设置 子内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? GetTargetString => string.IsNullOrEmpty(Target) ? null : Target;

    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
