// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">骨架屏组件基类</para>
/// <para lang="en">Skeleton Base Component</para>
/// </summary>
public abstract class SkeletonBase : BootstrapComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否圆角 默认为 true</para>
    /// <para lang="en">Gets or sets Whether round. Default true</para>
    /// </summary>
    [Parameter]
    public bool Round { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示动画 默认为 true</para>
    /// <para lang="en">Gets or sets Whether active. Default true</para>
    /// </summary>
    [Parameter]
    public bool Active { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得 样式</para>
    /// <para lang="en">Get Class Name</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("skeleton-content")
        .AddClass("active", Active)
        .AddClass("round", Round)
        .Build();
}
