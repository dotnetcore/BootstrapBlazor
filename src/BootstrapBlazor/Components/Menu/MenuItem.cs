// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// MenuItem 组件
/// </summary>
public class MenuItem
{
    /// <summary>
    /// 获得 父级菜单
    /// </summary>
    public MenuItem? Parent { get; set; }

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
    /// 获得/设置 组件数据源
    /// </summary>
    public IEnumerable<MenuItem> Items { get; set; } = Enumerable.Empty<MenuItem>();

    /// <summary>
    /// 获得/设置 导航菜单文本内容
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 导航菜单链接地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 是否激活 默认 false 未激活 一般不设置
    /// </summary>
    /// <value></value>
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 是否收缩 默认 true 收缩 
    /// </summary>
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// 获得/设置 A 标签 target 参数 默认 null
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认 false 未禁用
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 图标字符串
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 匹配方式 默认 NavLinkMatch.All
    /// </summary>
    public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

    /// <summary>
    /// 获得/设置 菜单内子组件 默认为 null
    /// </summary>
    public RenderFragment? Template { get; set; }

    /// <summary>
    /// 获得 当前菜单所在层次 从 0 开始
    /// </summary>
    public int Indent { get; private set; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public MenuItem() { }

    /// <summary>
    /// 带参数构造函数
    /// </summary>
    /// <param name="text">显示文本</param>
    /// <param name="url">菜单地址</param>
    /// <param name="icon">菜单图标</param>
    public MenuItem(string text, string? url = null, string? icon = null)
    {
        Text = text;
        Url = url;
        Icon = icon;
    }

    /// <summary>
    /// 设置当前节点缩进方法
    /// </summary>
    protected internal virtual void SetIndent()
    {
        if (Parent != null)
        {
            Indent = Parent.Indent + 1;
        }
    }

    /// <summary>
    /// 获得 所有子项集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<MenuItem> GetAllSubItems() => Items.Concat(GetSubItems(Items));

    private static IEnumerable<MenuItem> GetSubItems(IEnumerable<MenuItem> items) => items.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);
}
