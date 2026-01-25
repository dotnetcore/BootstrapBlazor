// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ShowLabel 接口</para>
/// <para lang="en">IShowLabel Interface</para>
/// </summary>
public interface IShowLabel
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签 默认 null</para>
    /// <para lang="en">Gets or sets Whether to Show Label. Default is null</para>
    /// </summary>
    bool? ShowLabel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null</para>
    /// <para lang="en">Gets or sets Whether to Show Label Tooltip. Default is null</para>
    /// </summary>
    bool? ShowLabelTooltip { get; set; }
}
