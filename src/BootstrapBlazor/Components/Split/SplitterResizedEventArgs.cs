// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SplitterResizedEventArgs 类</para>
/// <para lang="en">SplitterResizedEventArgs Class</para>
/// </summary>
public class SplitterResizedEventArgs(string left)
{
    /// <summary>
    /// <para lang="zh">获得 the size of panel 1 (top/left) after a resize operation</para>
    /// <para lang="en">Gets the size of panel 1 (top/left) after a resize operation</para>
    /// </summary>
    public string FirstPanelSize => left;

    /// <summary>
    /// <para lang="zh">获得 组件第一个面板是否折叠</para>
    /// <para lang="en">Get Whether the first panel of the component is collapsed</para>
    /// </summary>
    public bool IsCollapsed => left == "0%";

    /// <summary>
    /// <para lang="zh">获得 组件第一个面板是否展开</para>
    /// <para lang="en">Get Whether the first panel of the component is expanded</para>
    /// </summary>
    public bool IsExpanded => left == "100%";
}
