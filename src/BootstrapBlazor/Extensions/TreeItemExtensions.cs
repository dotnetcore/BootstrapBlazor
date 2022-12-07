// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    public static IEnumerable<TreeViewItem<TItem>> GetAllSubItems<TItem>(this IEnumerable<TreeViewItem<TItem>> source) => source.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetAllSubItems(i.Items)) : i.Items);
}
