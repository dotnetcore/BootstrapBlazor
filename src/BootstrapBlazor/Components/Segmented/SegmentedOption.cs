// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SegmentedOption 类</para>
/// <para lang="en">SegmentedOption Class</para>
/// </summary>
public class SegmentedOption<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示名称</para>
    /// <para lang="en">Gets or sets Display Text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项值</para>
    /// <para lang="en">Gets or sets Value</para>
    /// </summary>
    public TValue? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中</para>
    /// <para lang="en">Gets or sets Whether Active</para>
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认 false</para>
    /// <para lang="en">Gets or sets Whether disabled. Default false</para>
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets Icon</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">组件内容</para>
    /// <para lang="en">Child Content</para>
    /// </summary>
    public RenderFragment? ChildContent { get; set; }
}
