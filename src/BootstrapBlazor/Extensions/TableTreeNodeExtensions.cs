// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// TableTreeNode 扩展方法
/// </summary>
internal static class TableTreeNodeExtensions
{
    /// <summary>
    /// 尝试在全部树状结构 <paramref name="items"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="target"></param>
    /// <param name="ret">查询结果 查无资料时为 null</param>
    /// <param name="equalityComparer">比较函示 为null时判断方式为位址相同</param>
    /// <returns>是否存在 <paramref name="target"/></returns>
    /// <remarks>采广度优先搜寻</remarks>
    public static bool TryFind<TItem>(this IEnumerable<TableTreeNode<TItem>> items, TItem target, [MaybeNullWhen(false)] out TableTreeNode<TItem> ret, IEqualityComparer<TItem> equalityComparer)
    {
        ret = items.Find(target, equalityComparer);
        return ret != null;
    }

    /// <summary>
    /// 在全部树状结构 <paramref name="items"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer">比较函示 为null时判断方式为位址相同</param>
    /// <returns>查询结果 查无资料时为 null</returns>
    /// <remarks>采广度优先搜寻</remarks>
    public static TableTreeNode<TItem>? Find<TItem>(this IEnumerable<TableTreeNode<TItem>> items, TItem target, IEqualityComparer<TItem> equalityComparer) => items.Find(target, out _, equalityComparer);

    /// <summary>
    /// 在全部树状结构 <paramref name="source"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="degree">树状阶层，起始为0</param>
    /// <param name="equalityComparer">比较函示 为null时判断方式为位址相同</param>
    /// <returns>查询结果 查无资料时为 null</returns>
    /// <remarks>采广度优先搜寻</remarks>
    public static TableTreeNode<TItem>? Find<TItem>(this IEnumerable<TableTreeNode<TItem>> source, TItem target, out int degree, IEqualityComparer<TItem> equalityComparer)
    {
        degree = -1;
        var ret = source.FirstOrDefault(item => equalityComparer.Equals(item.Value, target));
        if (ret == null)
        {
            var children = source.SelectMany(e => e.Items);
            ret = Find(children, target, out degree, equalityComparer);
        }
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
    /// <returns></returns>
    public static List<TItem> GetAllRows<TItem>(this IEnumerable<TableTreeNode<TItem>> items) => items.GetAllRows(new List<TItem>());

    /// <summary>
    /// 展开树状结构
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="results"></param>
    /// <returns></returns>
    private static List<TItem> GetAllRows<TItem>(this IEnumerable<TableTreeNode<TItem>> items, List<TItem> results)
    {
        foreach (var item in items)
        {
            results.Add(item.Value);
            if (item.IsExpand && item.Items.Any())
            {
                GetAllRows(item.Items, results);
            }
        }
        return results;
    }
}
