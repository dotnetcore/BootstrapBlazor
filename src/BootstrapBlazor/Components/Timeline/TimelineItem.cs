// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">时间线选项
///</para>
/// <para lang="en">时间线选项
///</para>
/// </summary>
public class TimelineItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 时间线内容
    ///</para>
    /// <para lang="en">Gets or sets 时间线content
    ///</para>
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间线描述
    ///</para>
    /// <para lang="en">Gets or sets 时间线描述
    ///</para>
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间线描述模板
    ///</para>
    /// <para lang="en">Gets or sets 时间线描述template
    ///</para>
    /// </summary>
    public RenderFragment? DescriptionTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间线颜色
    ///</para>
    /// <para lang="en">Gets or sets 时间线color
    ///</para>
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间线图标
    ///</para>
    /// <para lang="en">Gets or sets 时间线icon
    ///</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义组件
    ///</para>
    /// <para lang="en">Gets or sets 自定义component
    ///</para>
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }

    /// <summary>
    /// <para lang="zh">获得 时间线节点样式
    ///</para>
    /// <para lang="en">Gets 时间线节点style
    ///</para>
    /// </summary>
    internal string? ToNodeClassString() => CssBuilder.Default("timeline-item-node-normal timeline-item-node")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None && string.IsNullOrEmpty(Icon))
        .AddClass("is-icon", !string.IsNullOrEmpty(Icon))
        .Build();

    /// <summary>
    /// <para lang="zh">获得 图标样式
    ///</para>
    /// <para lang="en">Gets iconstyle
    ///</para>
    /// </summary>
    /// <returns></returns>
    internal string? ToIconClassString() => CssBuilder.Default("timeline-item-icon")
        .AddClass(Icon, !string.IsNullOrEmpty(Icon))
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .Build();
}
