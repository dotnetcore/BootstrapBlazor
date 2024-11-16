// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone


namespace BootstrapBlazor.Components;

/// <summary>
/// Tree 组件节点缓存类
/// </summary>
/// <typeparam name="TNode"></typeparam>
/// <typeparam name="TItem"></typeparam>
public class TreeNodeCache<TNode, TItem> : ExpandableNodeCache<TNode, TItem> where TNode : ICheckableNode<TItem>
{
    /// <remarks>
    /// 构造函数
    /// </remarks>
    public TreeNodeCache(Func<TItem, TItem, bool> comparer) : base(comparer)
    {
        CheckedNodeCache = new(50, EqualityComparer);
        UncheckedNodeCache = new(50, EqualityComparer);
        IndeterminateNodeCache = new(50, EqualityComparer);
    }

    /// <summary>
    /// 获得 所有选中节点集合 作为缓存使用
    /// </summary>
    protected HashSet<TItem> CheckedNodeCache { get; }

    /// <summary>
    /// 获得 所有未选中节点集合 作为缓存使用
    /// </summary>
    protected HashSet<TItem> UncheckedNodeCache { get; }

    /// <summary>
    /// 获得 所有未选中节点集合 作为缓存使用
    /// </summary>
    protected HashSet<TItem> IndeterminateNodeCache { get; }

    /// <summary>
    /// 切换选中状态方法
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public virtual void ToggleCheck(TNode node)
    {
        if (node.CheckedState == CheckboxState.Checked)
        {
            // 未选中节点缓存移除此节点
            UncheckedNodeCache.Remove(node.Value);
            IndeterminateNodeCache.Remove(node.Value);

            // 选中节点缓存添加此节点
            if (!CheckedNodeCache.Contains(node.Value))
            {
                CheckedNodeCache.Add(node.Value);
            }
        }
        else if (node.CheckedState == CheckboxState.UnChecked)
        {
            // 选中节点缓存添加此节点
            CheckedNodeCache.Remove(node.Value);
            IndeterminateNodeCache.Remove(node.Value);

            // 未选中节点缓存移除此节点
            if (!UncheckedNodeCache.Contains(node.Value))
            {
                UncheckedNodeCache.Add(node.Value);
            }
        }
        else
        {
            // 不确定节点缓存添加此节点
            CheckedNodeCache.Remove(node.Value);
            UncheckedNodeCache.Remove(node.Value);

            // 未选中节点缓存移除此节点
            if (!IndeterminateNodeCache.Contains(node.Value))
            {
                IndeterminateNodeCache.Add(node.Value);
            }
        }
    }

    /// <summary>
    /// 检查当前节点是否选中
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private void IsChecked(TNode node)
    {
        // 当前节点状态为未确定状态
        var nodes = node.Items.OfType<ICheckableNode<TItem>>();
        if (CheckedNodeCache.Contains(node.Value))
        {
            node.CheckedState = CheckboxState.Checked;
        }
        else if (UncheckedNodeCache.Contains(node.Value, EqualityComparer))
        {
            node.CheckedState = CheckboxState.UnChecked;
        }
        else if (IndeterminateNodeCache.Contains(node.Value, EqualityComparer))
        {
            node.CheckedState = CheckboxState.Indeterminate;
        }
        CheckChildren(nodes);

        void CheckChildren(IEnumerable<ICheckableNode<TItem>> nodes)
        {
            if (nodes.Any())
            {
                CheckedNodeCache.Remove(node.Value);
                UncheckedNodeCache.Remove(node.Value);
                IndeterminateNodeCache.Remove(node.Value);

                // 查看子节点状态
                if (nodes.All(i => i.CheckedState == CheckboxState.Checked))
                {
                    node.CheckedState = CheckboxState.Checked;
                    CheckedNodeCache.Add(node.Value);
                }
                else if (nodes.All(i => i.CheckedState == CheckboxState.UnChecked))
                {
                    node.CheckedState = CheckboxState.UnChecked;
                    UncheckedNodeCache.Add(node.Value);
                }
                else
                {
                    node.CheckedState = CheckboxState.Indeterminate;
                    IndeterminateNodeCache.Add(node.Value);
                }
            }
        }
    }

    /// <summary>
    /// 重置是否选中状态
    /// </summary>
    /// <param name="nodes"></param>
    public void IsChecked(IEnumerable<TNode> nodes)
    {
        if (nodes.Any())
        {
            ResetCheckNodes(nodes);
        }

        void ResetCheckNodes(IEnumerable<TNode> items)
        {
            // 恢复当前节点状态
            foreach (var node in items)
            {
                // 恢复子节点
                if (node.Items.Any())
                {
                    IsChecked(node.Items.OfType<TNode>());
                }

                // 设置本节点
                IsChecked(node);
            }
        }
    }

    /// <summary>
    /// 通过指定节点查找父节点
    /// </summary>
    /// <param name="nodes">数据集合</param>
    /// <param name="node">指定节点</param>
    /// <returns></returns>
    public TNode? FindParentNode(IEnumerable<TNode> nodes, TNode node)
    {
        TNode? ret = default;
        foreach (var treeNode in nodes)
        {
            var subNodes = treeNode.Items.OfType<TNode>();
            if (subNodes.Any(i => EqualityComparer.Equals(i.Value, node.Value)))
            {
                ret = treeNode;
                break;
            }
            if (ret == null && subNodes.Any())
            {
                ret = FindParentNode(subNodes, node);
            }
        }
        return ret;
    }

    /// <summary>
    /// 清除缓存方法
    /// </summary>
    public void Reset()
    {
        UncheckedNodeCache.Clear();
        CheckedNodeCache.Clear();
        IndeterminateNodeCache.Clear();
        ExpandedNodeCache.Clear();
        CollapsedNodeCache.Clear();
    }
}
