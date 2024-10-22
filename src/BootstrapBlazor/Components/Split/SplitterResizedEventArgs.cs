// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// SplitterResizedEventArgs 类
/// </summary>
public class SplitterResizedEventArgs(string left)
{
    /// <summary>
    /// Gets the size of panel 1 (top/left) after a resize operation.
    /// </summary>
    public string FirstPanelSize => left;

    /// <summary>
    /// 获得 组件第一个面板是否折叠
    /// </summary>
    public bool IsCollapsed => left == "0%";

    /// <summary>
    /// 获得 组件第一个面板是否展开
    /// </summary>
    public bool IsExpanded => left == "100%";
}
