// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;


public interface ITableTreeItem<TItem>
{
    public static ITableTreeItem<TNew> New<TNew>(TNew item) where TNew : class
    {
        if (item is ITableTreeItem<TNew> ret)
            return ret;
        else
            return new TableTreeNode<TNew>(item);
    }

    /// <summary>
    /// 获得 子节点集合
    /// </summary>
    public IEnumerable<ITableTreeItem<TItem>>? Children { get; }

    /// <summary>
    /// 设置 子节点集合
    /// </summary>
    public void SetChildren(IEnumerable<TItem> items);

    /// <summary>
    /// 获得/设置 是否展开
    /// </summary>
    public bool IsExpand { get; set; }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class TableTreeNode<TItem> : ITableTreeItem<TItem> where TItem : class
{
    /// <summary>
    /// 获得/设置 当前节点值
    /// </summary>
    public TItem Value { get; }

    /// <summary>
    /// 获得/设置 是否展开
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    public List<TableTreeNode<TItem>>? Children { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public TableTreeNode(TItem item)
    {
        Value = item;
    }

    IEnumerable<ITableTreeItem<TItem>>? ITableTreeItem<TItem>.Children => Children;

    /// <inheritdoc/>
    public void SetChildren(IEnumerable<TItem> items)
    {
        Children = items.Select(item => new TableTreeNode<TItem>(item)).ToList();
    }
}

public static class TreeRowsExtensions
{
    public static bool TryFind<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, TItem item, [MaybeNullWhen(false)] out ITableTreeItem<TItem> ret, Func<TItem, TItem, bool>? equals = null) where TItem : class
    {
        ret = items.Find(item, equals);
        return ret != null;
    }

    public static ITableTreeItem<TItem>? Find<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, TItem target, Func<TItem, TItem, bool>? equals = null) where TItem : class
        => Find(items, target, out _, equals);

    public static ITableTreeItem<TItem>? Find<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, TItem target, out int degree, Func<TItem, TItem, bool>? equals = null) where TItem : class
    {
        degree = -1;
        if (equals == null)
            equals = (a, b) => a == b;
        var ret = items.FirstOrDefault(item => equals(item.GetValue(), target));
        var children = items.SelectMany(e => e.Children ?? Array.Empty<ITableTreeItem<TItem>>());
        ret ??= Find(children, target, out degree, equals);
        if (ret != null) degree++;
        return ret;
    }

    public static IEnumerable<TItem> GetAllRows<TItem>(this IEnumerable<ITableTreeItem<TItem>> items, bool expandOnly = true) where TItem : class
    {
        foreach (var item in items)
        {
            yield return item.GetValue();
            if (expandOnly && !item.IsExpand)
            {
                yield break;
            }
            var children = item.Children;
            if (children != null)
            {
                foreach (var child in GetAllRows(children))
                {
                    yield return child;
                }
            }
        }
    }

    public static TItem GetValue<TItem>(this ITableTreeItem<TItem> item) where TItem : class
    {
        if (item is TableTreeNode<TItem> tableTreeNode)
            return tableTreeNode.Value;
        else if (item is TItem t)
            return t;
        else
            throw new InvalidOperationException(item.GetType() + "is not ");
    }

    public static void SetChildren<TItem>(this ITableTreeItem<TItem> item, IEnumerable<ITableTreeItem<TItem>> items) where TItem : class
    {
        if (item is TableTreeNode<TItem> tableTreeNode)
            tableTreeNode.Children = items.OfType<TableTreeNode<TItem>>().ToList();
        else
        {
            item.SetChildren(items.Select(e => e.GetValue()));
        }
    }
}

