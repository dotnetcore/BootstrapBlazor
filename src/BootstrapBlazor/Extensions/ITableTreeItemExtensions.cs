// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal static class ITableTreeItemExtensions
{
    /// <summary>
    /// 尝试在全部树状结构 <paramref name="items"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="target"></param>
    /// <param name="ret">查询结果 查无资料时为 null</param>
    /// <param name="equals">比较函示 为null时判断方式为位址相同</param>
    /// <returns>是否存在 <paramref name="target"/></returns>
    /// <remarks>采广度优先搜寻</remarks>
    public static bool TryFind<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, TItem target, [MaybeNullWhen(false)] out ITableTreeItem<TItem> ret, Func<TItem, TItem, bool>? equals = null) where TItem : class
    {
        ret = items.Find(target, equals);
        return ret != null;
    }

    /// <summary>
    /// 在全部树状结构 <paramref name="items"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="target"></param>
    /// <param name="equals">比较函示 为null时判断方式为位址相同</param>
    /// <returns>查询结果 查无资料时为 null</returns>
    /// <remarks>采广度优先搜寻</remarks>
    public static ITableTreeItem<TItem>? Find<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, TItem target, Func<TItem, TItem, bool>? equals = null) where TItem : class
    {
        return items.Find(target, out _, equals);
    }

    /// <summary>
    /// 在全部树状结构 <paramref name="items"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="target"></param>
    /// <param name="degree">树状阶层，起始为0</param>
    /// <param name="equals">比较函示 为null时判断方式为位址相同</param>
    /// <returns>查询结果 查无资料时为 null</returns>
    /// <remarks>采广度优先搜寻</remarks>
    public static ITableTreeItem<TItem>? Find<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, TItem target, out int degree, Func<TItem, TItem, bool>? equals = null) where TItem : class
    {
        degree = -1;
        if (equals == null)
        {
            equals = (a, b) => a == b;
        }

        var ret = items.FirstOrDefault(item => equals(item.GetValue(), target));
        var children = items.SelectMany(e => e.Children ?? Array.Empty<ITableTreeItem<TItem>>());
        ret ??= Find(children, target, out degree, equals);
        if (ret != null)
        {
            degree++;
        }

        return ret;
    }

    /// <summary>
    /// 展开树状结构
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="results"></param>
    /// <param name="expandOnly">是否要排除未展开资料</param>
    /// <returns></returns>
    public static List<TItem> GetAllRows<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, List<TItem>? results = null, bool expandOnly = true) where TItem : class
    {
        results ??= new();
        foreach (var item in items)
        {
            results.Add(item.GetValue());
            if (!expandOnly || item.IsExpand)
            {
                var children = item.Children;
                if (children != null)
                {
                    GetAllRows(children, results, expandOnly);
                }
            }
        }
        return results;
    }

    /// <summary>
    /// 取树状结构的值
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static TItem GetValue<TItem>(this ITableTreeItem<TItem> item) where TItem : class
    {
        if (item is TableTreeNode<TItem> tableTreeNode)
        {
            return tableTreeNode.Value;
        }
        else if (item is TItem t)
        {
            return t;
        }
        else
        {
            throw new InvalidOperationException($"{item.GetType()} can not be assigned to ITableTreeItem<{typeof(TItem)}>");
        }
    }

    /// <summary>
    /// 设置 子节点集合
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <param name="items"></param>
    public static void SetChildren<TItem>(this ITableTreeItem<TItem> item, IEnumerable<ITableTreeItem<TItem>> items) where TItem : class
    {
        if (item is TableTreeNode<TItem> tableTreeNode)
        {
            tableTreeNode.Children = items.OfType<TableTreeNode<TItem>>().ToList();
        }
        else
        {
            item.SetChildren(items.Select(e => e.GetValue()));
        }
    }
}
