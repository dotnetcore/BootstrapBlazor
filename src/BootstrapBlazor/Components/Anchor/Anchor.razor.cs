// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Anchor 组件部分类</para>
/// <para lang="en">Anchor component</para>
/// </summary>
public partial class Anchor
{
    /// <summary>
    /// <para lang="zh">获得/设置 目标组件 Id</para>
    /// <para lang="en">Gets or sets the target component Id</para>
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动组件 Id 默认为 null 使用最近滚动条容器元素</para>
    /// <para lang="en">Gets or sets the scroll component Id. Default is null, using the nearest scroll container element</para>
    /// </summary>
    [Parameter]
    public string? Container { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动时是否开启动画 默认 true</para>
    /// <para lang="en">Gets or sets whether to enable animation when scrolling. Default is true</para>
    /// </summary>
    [Parameter]
    public bool IsAnimation { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得 滚动动画</para>
    /// <para lang="en">Gets the scroll animation</para>
    /// </summary>
    protected string? AnimationString => IsAnimation ? "true" : null;

    /// <summary>
    /// <para lang="zh">获得/设置 距离顶端偏移量 默认为 0</para>
    /// <para lang="en">Gets or sets the offset from the top. Default is 0</para>
    /// </summary>
    [Parameter]
    public int Offset { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子内容</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? GetTargetString => string.IsNullOrEmpty(Target) ? null : Target;

    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
