// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class TreeNodeBase<TItem> : NodeBase<TItem>
{
    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 展开节点状态下的 Icon 图标
    /// </summary>
    public string? ExpandIcon { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式名
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 获得/设置 是否被禁用 默认 false
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 是否选中当前节点 默认 false
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 子组件模板 默认为 null
    /// </summary>
    public RenderFragment<TItem>? Template { get; set; }
}
