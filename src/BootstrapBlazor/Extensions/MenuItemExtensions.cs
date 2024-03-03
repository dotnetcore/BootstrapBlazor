// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
