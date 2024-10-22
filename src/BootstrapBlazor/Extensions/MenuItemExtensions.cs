// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// MenuItem 扩展操作类
/// </summary>
public static class MenuItemExtensions
{
    /// <summary>
    /// 级联设置 <see cref="MenuItem"/> Active 状态
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
    ///  获取全部节点
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<MenuItem>? GetAllItems(this IEnumerable<MenuItem>? source) => source == null ? null : GetAllSubItems(source).Union(source);

    /// <summary>
    /// 获取全部子节点
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<MenuItem> GetAllSubItems(this IEnumerable<MenuItem>? source) => source?.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetAllSubItems(i.Items)) : i.Items) ?? [];
}
