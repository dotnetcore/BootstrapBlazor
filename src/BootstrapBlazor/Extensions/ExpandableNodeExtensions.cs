// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    public static List<TItem> GetAllItems<TItem>(this IEnumerable<IExpandableNode<TItem>> items) => items.GetAllItems(new List<TItem>());

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
    public static IEnumerable<TreeItem<TItem>> GetAllTreeSubItems<TItem>(this IExpandableNode<TItem> item) => item.GetAllSubItems().OfType<TreeItem<TItem>>();

    /// <summary>
    /// 向下级联设置复选状态
    /// </summary>
    internal static void CascadeSetCheck<TNode, TItem>(this TNode node, CheckboxState state, Action<TNode> callback) where TNode : ICheckableNode<TItem>
    {
        foreach (var item in node.Items.OfType<TNode>())
        {
            item.CheckedState = state;
            callback(item);

            // 设置子节点
            if (item.Items.Any())
            {
                item.CascadeSetCheck<TNode, TItem>(state, callback);
            }
        }
    }

    /// <summary>
    /// 向上级联设置复选状态
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="node"></param>
    /// <param name="state"></param>
    public static void SetParentCheck<TItem>(this ICheckableNode<TItem> node, CheckboxState state)
    {
        if (node.Parent is ICheckableNode<TItem> p)
        {
            var nodes = p.Items.OfType<ICheckableNode<TItem>>();
            if (nodes.All(i => i.CheckedState == CheckboxState.Checked))
            {
                p.CheckedState = CheckboxState.Checked;
            }
            else if (nodes.Any(i => i.CheckedState == CheckboxState.Checked || i.CheckedState == CheckboxState.Indeterminate))
            {
                p.CheckedState = CheckboxState.Indeterminate;
            }
            else
            {
                p.CheckedState = CheckboxState.UnChecked;
            }

            SetParentCheck(p, state);
        }
    }
}
