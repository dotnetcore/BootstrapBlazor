// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 节点缓存类
/// </summary>
/// <typeparam name="TNode"></typeparam>
/// <typeparam name="TItem"></typeparam>
public class ExpandableNodeCache<TNode, TItem> where TNode : IExpandableNode<TItem>
{
    /// <summary>
    /// 所有已展开行集合 作为缓存使用
    /// </summary>
    protected readonly List<TItem> expandedNodeCache = new(50);

    /// <summary>
    /// 所有已收缩行集合 作为缓存使用
    /// </summary>
    protected readonly List<TItem> collapsedNodeCache = new(50);

    /// <summary>
    /// 对象比较器
    /// </summary>
    protected readonly IEqualityComparer<TItem> equalityComparer;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ExpandableNodeCache(Func<TItem, TItem, bool> comparer)
    {
        equalityComparer = new ModelComparer<TItem>(comparer);
    }

    /// <summary>
    /// 节点展开收缩状态切换方法
    /// </summary>
    /// <param name="node"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task ToggleNodeAsync(TNode node, Func<TNode, Task<IEnumerable<IExpandableNode<TItem>>>> callback)
    {
        if (node.IsExpand)
        {
            // 展开节点缓存增加此节点
            if (!expandedNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                expandedNodeCache.Add(node.Value);
            }

            // 收缩节点缓存移除此节点
            collapsedNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));

            // 无子项时通过回调方法延时加载
            if (!node.Items.Any())
            {
                node.Items = await callback(node);
                ICheckableNode<TItem>? checkNode = null;
                if (node is ICheckableNode<TItem> c)
                {
                    checkNode = c;
                }
                foreach (var n in node.Items)
                {
                    n.Parent = node;
                    if (checkNode != null && n is ICheckableNode<TItem> cn)
                    {
                        cn.CheckedState = checkNode.CheckedState == CheckboxState.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;
                    }
                }
            }
        }
        else
        {
            // 展开节点缓存移除此节点
            expandedNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));

            // 收缩节点缓存添加此节点
            if (!collapsedNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                collapsedNodeCache.Add(node.Value);
            }
        }
    }

    /// <summary>
    /// 检查当前节点是否展开
    /// </summary>
    /// <param name="node"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public async Task CheckExpandAsync(TNode node, Func<TNode, Task<IEnumerable<IExpandableNode<TItem>>>> callback)
    {
        if (node.IsExpand)
        {
            // 已收缩
            if (collapsedNodeCache.Contains(node.Value, equalityComparer))
            {
                node.IsExpand = false;
            }
            else if (!expandedNodeCache.Contains(node.Value, equalityComparer))
            {
                // 状态为 展开
                expandedNodeCache.Add(node.Value);
            }
        }
        else
        {
            if (expandedNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                // 原来是展开状态
                node.IsExpand = true;

                if (!node.Items.Any())
                {
                    node.Items = await callback(node);
                    foreach (var n in node.Items)
                    {
                        n.Parent = node;
                    }
                }
            }
            else
            {
                expandedNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));
            }
        }
    }

    /// <summary>
    /// 尝试在全部树状结构 <paramref name="items"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <param name="items"></param>
    /// <param name="target"></param>
    /// <param name="ret">查询结果 查无资料时为 null</param>
    /// <returns>是否存在 <paramref name="target"/></returns>
    /// <remarks>采广度优先搜寻</remarks>
    public bool TryFind(IEnumerable<TNode> items, TItem target, [MaybeNullWhen(false)] out TNode ret)
    {
        ret = Find(items, target);
        return ret != null;
    }

    /// <summary>
    /// 在全部树状结构 <paramref name="items"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <param name="items"></param>
    /// <param name="target"></param>
    /// <returns>查询结果 查无资料时为 null</returns>
    /// <remarks>采广度优先搜寻</remarks>
    private TNode? Find(IEnumerable<TNode> items, TItem target) => Find(items, target, out _);

    /// <summary>
    /// 在全部树状结构 <paramref name="source"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="degree">树状阶层，起始为0</param>
    /// <returns>查询结果 查无资料时为 null</returns>
    /// <remarks>采广度优先搜寻</remarks>
    public TNode? Find(IEnumerable<TNode> source, TItem target, out int degree)
    {
        degree = -1;
        var ret = source.FirstOrDefault(item => equalityComparer.Equals(item.Value, target));
        if (ret == null)
        {
            var children = source.SelectMany(e => e.Items.OfType<TNode>());
            ret = Find(children, target, out degree);
        }
        if (ret != null)
        {
            degree++;
        }
        return ret;
    }
}
