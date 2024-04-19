// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 节点缓存类
/// </summary>
/// <typeparam name="TNode"></typeparam>
/// <typeparam name="TItem"></typeparam>
/// <remarks>
/// 构造函数
/// </remarks>
public class ExpandableNodeCache<TNode, TItem>(Func<TItem, TItem, bool> comparer) where TNode : IExpandableNode<TItem>
{
    /// <summary>
    /// 所有已展开行集合 作为缓存使用
    /// </summary>
    protected List<TItem> ExpandedNodeCache { get; } = new(50);

    /// <summary>
    /// 所有已收缩行集合 作为缓存使用
    /// </summary>
    protected List<TItem> CollapsedNodeCache { get; } = new(50);

    /// <summary>
    /// 对象比较器
    /// </summary>
    protected IEqualityComparer<TItem> EqualityComparer { get; } = new ModelComparer<TItem>(comparer);

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
            if (!ExpandedNodeCache.Any(i => EqualityComparer.Equals(i, node.Value)))
            {
                ExpandedNodeCache.Add(node.Value);
            }

            // 收缩节点缓存移除此节点
            CollapsedNodeCache.RemoveAll(i => EqualityComparer.Equals(i, node.Value));

            // 无子项时通过回调方法延时加载
            if (!node.Items.Any())
            {
                var items = await callback(node);
                node.Items = items.ToList();
                ICheckableNode<TItem>? checkNode = null;
                if (node is ICheckableNode<TItem> c)
                {
                    checkNode = c;
                }
                foreach (var n in node.Items)
                {
                    n.Parent = node;
                    //if (checkNode != null && n is ICheckableNode<TItem> cn)
                    //{
                    //    cn.CheckedState = checkNode.CheckedState == CheckboxState.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;
                    //}
                }
            }
        }
        else
        {
            // 展开节点缓存移除此节点
            ExpandedNodeCache.RemoveAll(i => EqualityComparer.Equals(i, node.Value));

            // 收缩节点缓存添加此节点
            if (!CollapsedNodeCache.Any(i => EqualityComparer.Equals(i, node.Value)))
            {
                CollapsedNodeCache.Add(node.Value);
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
            if (CollapsedNodeCache.Contains(node.Value, EqualityComparer))
            {
                node.IsExpand = false;
            }
            else if (!ExpandedNodeCache.Contains(node.Value, EqualityComparer))
            {
                // 状态为 展开
                ExpandedNodeCache.Add(node.Value);
            }
        }
        else
        {
            var needRemove = true;
            if (ExpandedNodeCache.Any(i => EqualityComparer.Equals(i, node.Value)))
            {
                // 原来是展开状态，
                if (node.HasChildren)
                {
                    // 当前节点有子节点
                    node.IsExpand = true;
                    needRemove = false;
                    if (!node.Items.Any())
                    {
                        var items = await callback(node);
                        node.Items = items.ToList();
                        foreach (var n in node.Items)
                        {
                            n.Parent = node;
                        }
                    }
                }
            }
            if (needRemove)
            {
                ExpandedNodeCache.RemoveAll(i => EqualityComparer.Equals(i, node.Value));
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
        var ret = source.FirstOrDefault(item => EqualityComparer.Equals(item.Value, target));
        if (ret == null)
        {
            var children = source.SelectMany(e => e.Items.OfType<TNode>());
            if (children.Any())
            {
                ret = Find(children, target, out degree);
            }
        }
        if (ret != null)
        {
            degree++;
        }
        return ret;
    }
}
