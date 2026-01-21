// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">MenuItem 扩展操作类</para>
/// <para lang="en">MenuItem extension operation class</para>
/// </summary>
public static class MenuItemExtensions
{
    /// <summary>
    /// <para lang="zh">级联设置 <see cref="MenuItem"/> Active 状态</para>
    /// <para lang="en">Cascade set <see cref="MenuItem"/> Active state</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="active"></param>
    public static void CascadingSetActive(this MenuItem item, bool active = true)
    {
        item.IsActive = active;
        var current = item;
        while (current.Parent != null)
        {
            current.Parent.IsActive = active;
            current.Parent.IsCollapsed = false;
            current = current.Parent;
        }
    }

    /// <summary>
    /// <para lang="zh">获取全部节点</para>
    /// <para lang="en">Get all items</para>
    /// </summary>
    /// <param name="source"></param>
    public static IEnumerable<MenuItem>? GetAllItems(this IEnumerable<MenuItem>? source) => source == null ? null : GetAllSubItems(source).Union(source);

    /// <summary>
    /// <para lang="zh">获取全部子节点</para>
    /// <para lang="en">Get all sub items</para>
    /// </summary>
    /// <param name="source"></param>
    public static IEnumerable<MenuItem> GetAllSubItems(this IEnumerable<MenuItem>? source) => source?.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetAllSubItems(i.Items)) : i.Items) ?? [];
}
