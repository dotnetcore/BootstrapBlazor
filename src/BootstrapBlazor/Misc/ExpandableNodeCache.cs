// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    protected HashSet<TItem> ExpandedNodeCache { get; }

    /// <summary>
    /// 所有已收缩行集合 作为缓存使用
    /// </summary>
    protected HashSet<TItem> CollapsedNodeCache { get; }

    /// <summary>
    /// 对象比较器
    /// </summary>
    protected IEqualityComparer<TItem> EqualityComparer { get; }

    /// <remarks>
    /// 构造函数
    /// </remarks>
    public ExpandableNodeCache(IModelEqualityComparer<TItem> comparer)
    {
        EqualityComparer = new ModelHashSetComparer<TItem>(comparer);
        ExpandedNodeCache = new(50, EqualityComparer);
        CollapsedNodeCache = new(50, EqualityComparer);
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
            ExpandedNodeCache.Add(node.Value);

            // 收缩节点缓存移除此节点
            CollapsedNodeCache.RemoveWhere(x => EqualityComparer.Equals(x, node.Value));

            // 无子项时通过回调方法延时加载
            if (node.HasChildren && !node.Items.Any())
            {
                var items = await callback(node);
                node.Items = items.ToList();
                foreach (var n in node.Items)
                {
                    n.Parent = node;
                }
            }
        }
        else
        {
            // 展开节点缓存移除此节点
            ExpandedNodeCache.RemoveWhere(x => EqualityComparer.Equals(x, node.Value));

            // 收缩节点缓存添加此节点
            CollapsedNodeCache.Add(node.Value);
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

            // 状态为 展开
            ExpandedNodeCache.Add(node.Value);
        }
        else
        {
            var needRemove = true;
            if (ExpandedNodeCache.Contains(node.Value, EqualityComparer))
            {
                // 原来是展开状态，
                if (node.HasChildren || node.Items.Any())
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
                ExpandedNodeCache.RemoveWhere(x => EqualityComparer.Equals(x, node.Value));
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
    public bool TryFind(List<TNode> items, TItem target, [MaybeNullWhen(false)] out TNode ret)
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
    private TNode? Find(List<TNode> items, TItem target) => Find(items, target, out _);

    /// <summary>
    /// 在全部树状结构 <paramref name="source"/> 中寻找指定 <paramref name="target"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="degree">树状阶层，起始为0</param>
    /// <returns>查询结果 查无资料时为 null</returns>
    /// <remarks>采广度优先搜寻</remarks>
    public TNode? Find(List<TNode> source, TItem target, out int degree)
    {
        degree = -1;
        var ret = source.FirstOrDefault(item => EqualityComparer.Equals(item.Value, target));
        if (ret == null)
        {
            var children = source.SelectMany(e => e.Items.OfType<TNode>()).ToList();
            if (children.Count != 0)
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
