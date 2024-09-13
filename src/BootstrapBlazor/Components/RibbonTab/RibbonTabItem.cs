// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// RibbonTabItem 实体类
/// </summary>
public class RibbonTabItem
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
    /// 获得 父级菜单 默认为 null
    /// </summary>
    public RibbonTabItem? Parent { get; set; }

    /// <summary>
    /// 获得/设置 导航菜单链接地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 A 标签 target 参数 默认 null
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 图片路径
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 获得/设置 分组名称
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 按钮标识
    /// </summary>
    public string? Command { get; set; }

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
    /// 获得/设置 是否收缩 默认 true 收缩 
    /// </summary>
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否被禁用 默认 false
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 是否为默认按钮 默认 false
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// 获得/设置 是否选中当前节点 默认 false
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    public List<RibbonTabItem> Items { get; } = [];

    /// <summary>
    /// 获得/设置 子组件模板 默认为 null
    /// </summary>
    public RenderFragment? Template { get; set; }

    /// <summary>
    /// 获得/设置 动态组件实例
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }
}
