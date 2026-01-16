// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">MenuItem 组件</para>
/// <para lang="en">MenuItem Component</para>
/// </summary>
public class MenuItem : NodeItem
{
    /// <summary>
    /// <para lang="zh">获得 父级菜单</para>
    /// <para lang="en">Get Parent Menu</para>
    /// </summary>
    public MenuItem? Parent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件数据源</para>
    /// <para lang="en">Get/Set Component Data Source</para>
    /// </summary>
    public IEnumerable<MenuItem> Items { get; set; } = Enumerable.Empty<MenuItem>();

    /// <summary>
    /// <para lang="zh">获得/设置 导航菜单链接地址</para>
    /// <para lang="en">Get/Set Navigation Menu Link Address</para>
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 A 标签 target 参数 默认 null</para>
    /// <para lang="en">Get/Set Anchor target parameter. Default null</para>
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 匹配方式 默认 NavLinkMatch.Prefix</para>
    /// <para lang="en">Get/Set Match mode. Default NavLinkMatch.Prefix</para>
    /// </summary>
    public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;

    /// <summary>
    /// <para lang="zh">获得 当前菜单所在层次 从 0 开始</para>
    /// <para lang="en">Get Current menu level. Starting from 0</para>
    /// </summary>
    public int Indent { get; private set; }

    /// <summary>
    /// <para lang="zh">默认构造函数</para>
    /// <para lang="en">Default Constructor</para>
    /// </summary>
    public MenuItem() { }

    /// <summary>
    /// <para lang="zh">带参数构造函数</para>
    /// <para lang="en">Parameterized Constructor</para>
    /// </summary>
    /// <param name="text"><para lang="zh">显示文本</para><para lang="en">Display Text</para></param>
    /// <param name="url"><para lang="zh">菜单地址</para><para lang="en">Menu Url</para></param>
    /// <param name="icon"><para lang="zh">菜单图标</para><para lang="en">Menu Icon</para></param>
    public MenuItem(string text, string? url = null, string? icon = null)
    {
        Text = text;
        Url = url;
        Icon = icon;
    }

    /// <summary>
    /// <para lang="zh">设置当前节点缩进方法</para>
    /// <para lang="en">Set current node indent method</para>
    /// </summary>
    protected internal virtual void SetIndent()
    {
        if (Parent != null)
        {
            Indent = Parent.Indent + 1;
        }
    }

    /// <summary>
    /// <para lang="zh">设置当前节点父节点展开</para>
    /// <para lang="en">Set current node parent node expand</para>
    /// </summary>
    protected internal virtual void SetCollapse(bool collapsed)
    {
        var parent = Parent;
        while (parent != null)
        {
            parent.IsCollapsed = collapsed;
            parent = parent.Parent;
        }
    }

    /// <summary>
    /// <para lang="zh">获得 所有子项集合</para>
    /// <para lang="en">Get All sub-items collection</para>
    /// </summary>
    /// <returns></returns>
    public IEnumerable<MenuItem> GetAllSubItems() => Items.Concat(GetSubItems(Items));

    private static IEnumerable<MenuItem> GetSubItems(IEnumerable<MenuItem> items) => items.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);
}
