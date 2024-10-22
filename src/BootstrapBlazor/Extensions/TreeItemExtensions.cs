﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 树状结构获取所有项目扩展方法类
/// </summary>
public static class TreeItemExtensions
{
    /// <summary>
    /// 在全部树状结构 <paramref name="source"/> 中寻找第一个 Active 节点/>
    /// </summary>
    /// <param name="source"></param>
    public static TreeViewItem<TItem>? FirstOrDefaultActiveItem<TItem>(this IEnumerable<TreeViewItem<TItem>> source)
    {
        var ret = source.FirstOrDefault(item => item.IsActive);
        if (ret == null)
        {
            var items = source.SelectMany(e => e.Items);
            if (items.Any())
            {
                ret = FirstOrDefaultActiveItem(items);
            }
        }
        return ret;
    }

    /// <summary>
    ///  获取全部节点
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<TreeViewItem<TItem>> GetAllItems<TItem>(this IEnumerable<TreeViewItem<TItem>> source) => GetAllSubItems(source).Union(source);

    /// <summary>
    /// 获取全部子节点
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<TreeViewItem<TItem>> GetAllSubItems<TItem>(this IEnumerable<TreeViewItem<TItem>> source) => source.SelectMany(i => i.Items.Count > 0 ? i.Items.Concat(GetAllSubItems(i.Items)) : i.Items);
}
