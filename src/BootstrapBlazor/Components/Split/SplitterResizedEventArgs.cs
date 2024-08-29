// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
