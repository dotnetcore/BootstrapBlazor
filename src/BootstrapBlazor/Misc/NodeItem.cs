// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 节点类组件基类
/// </summary>
public abstract class NodeItem
{
    /// <summary>
    /// 获得/设置 当前节点 Id 默认为 null
    /// </summary>
    /// <remarks>一般配合数据库使用</remarks>
    public string? Id { get; set; }

    /// <summary>
    /// 获得/设置 父级节点 Id 默认为 null
    /// </summary>
    /// <remarks>一般配合数据库使用</remarks>
    public string? ParentId { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    public string? Icon { get; set; }

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
    /// 获得/设置 是否收缩 默认 true 收缩 
    /// </summary>
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// 获得/设置 子组件模板 默认为 null
    /// </summary>
    public RenderFragment? Template { get; set; }
}
