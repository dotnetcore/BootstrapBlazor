// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 树状结构获取所有项目扩展方法类
/// </summary>
public static class ExpandableNodeExtensions
{
    /// <summary>
    /// 获得所有节点集合
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public static List<TItem> GetAllItems<TItem>(this IEnumerable<IExpandableNode<TItem>> items) => items.GetAllItems([]);

    /// <summary>
    /// 获得所有节点集合
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="results"></param>
    /// <returns></returns>
    private static List<TItem> GetAllItems<TItem>(this IEnumerable<IExpandableNode<TItem>> items, List<TItem> results)
    {
        foreach (var item in items)
        {
            if (item.Value != null)
            {
                results.Add(item.Value);
                if (item.Items.Any())
                {
                    if (item.IsExpand)
                    {
                        GetAllItems(item.Items, results);
                    }
                }
            }
        }
        return results;
    }

    /// <summary>
    /// 获得 所有子项集合
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<IExpandableNode<TItem>> GetAllSubItems<TItem>(this IExpandableNode<TItem> item) => item.Items.Concat(GetSubItems(item.Items));

    private static IEnumerable<IExpandableNode<TItem>> GetSubItems<TItem>(IEnumerable<IExpandableNode<TItem>> items) => items.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);

    /// <summary>
    /// 获得 所有 TreeItem 子项集合
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public static IEnumerable<TreeViewItem<TItem>> GetAllTreeSubItems<TItem>(this IExpandableNode<TItem> item) => item.GetAllSubItems().OfType<TreeViewItem<TItem>>();

    /// <summary>
    /// 向下级联设置复选状态
    /// </summary>
    public static void SetChildrenCheck<TNode, TItem>(this TNode node, TreeNodeCache<TNode, TItem> cache) where TNode : ICheckableNode<TItem>
    {
        if (node.CheckedState == CheckboxState.Indeterminate)
        {
            return;
        }

        foreach (var item in node.Items.OfType<TNode>())
        {
            item.CheckedState = node.CheckedState;
            cache.ToggleCheck(item);

            // 设置子节点
            if (item.Items.Any())
            {
                item.SetChildrenCheck(cache);
            }
        }
    }

    /// <summary>
    /// 向上级联设置复选状态
    /// </summary>
    public static void SetParentCheck<TNode, TItem>(this TNode node, TreeNodeCache<TNode, TItem> cache) where TNode : ICheckableNode<TItem>
    {
        var parent = node.Parent;
        while (parent is TNode p)
        {
            var items = p.Items.OfType<TNode>().ToList();
            var checkedCount = 0;
            var uncheckedCount = 0;
            foreach (var item in items)
            {
                if (item.CheckedState == CheckboxState.Checked)
                {
                    checkedCount++;
                    if (uncheckedCount > 0)
                    {
                        break;
                    }
                }
                else if (item.CheckedState == CheckboxState.UnChecked)
                {
                    uncheckedCount++;
                    if (checkedCount > 0)
                    {
                        break;
                    }
                }
            }

            if (checkedCount == items.Count)
            {
                p.CheckedState = CheckboxState.Checked;
            }
            else if (uncheckedCount == items.Count)
            {
                p.CheckedState = CheckboxState.UnChecked;
            }
            else
            {
                p.CheckedState = CheckboxState.Indeterminate;
            }

            cache.ToggleCheck(p);
            parent = p.Parent;
        }
    }

    /// <summary>
    /// 向上级联设置展开状态
    /// </summary>
    public static void SetParentExpand<TNode, TItem>(this TNode node, bool expand) where TNode : IExpandableNode<TItem>
    {
        var parent = node.Parent;
        while (parent is TNode p)
        {
            p.IsExpand = expand;
            parent = p.Parent;
        }
    }

    /// <summary>
    /// 树状数据层次化方法
    /// </summary>
    /// <param name="items">数据集合</param>
    /// <param name="parent">父级节点</param>
    /// <param name="predicate">查找子节点 Lambda 表达式</param>
    /// <param name="valueFactory"></param>
    /// <param name="treeViewItemCallback">节点是否展开回调方法 默认 null 未设置</param>
    public static List<TreeViewItem<TItem>> CascadingTree<TItem>(this IEnumerable<TItem> items, TreeViewItem<TItem>? parent, Func<TItem, TreeViewItem<TItem>?, bool> predicate, Func<TItem, TreeViewItem<TItem>> valueFactory, Action<TreeViewItem<TItem>>? treeViewItemCallback = null) => [.. items.Where(i => predicate(i, parent)).Select(i =>
    {
        var item = valueFactory(i);
        item.Items = items.CascadingTree(item, predicate, valueFactory, treeViewItemCallback);
        item.Parent = parent;
        if (treeViewItemCallback != null)
        {
            treeViewItemCallback(item);
        }
        return item;
    })];
}
