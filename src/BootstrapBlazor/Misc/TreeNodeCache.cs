// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Tree 组件节点缓存类
/// </summary>
/// <typeparam name="TNode"></typeparam>
/// <typeparam name="TItem"></typeparam>
public class TreeNodeCache<TNode, TItem> : ExpandableNodeCache<TNode, TItem> where TNode : ICheckableNode<TItem>
{
    /// <summary>
    /// 获得 所有选中节点集合 作为缓存使用
    /// </summary>
    protected readonly List<TItem> checkedNodeCache = new(50);

    /// <summary>
    /// 获得 所有未选中节点集合 作为缓存使用
    /// </summary>
    protected readonly List<TItem> uncheckedNodeCache = new(50);

    /// <summary>
    /// 获得 所有未选中节点集合 作为缓存使用
    /// </summary>
    protected readonly List<TItem> indeterminateNodeCache = new(50);

    /// <summary>
    /// 构造函数
    /// </summary>
    public TreeNodeCache(Func<TItem, TItem, bool> comparer) : base(comparer)
    {

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public virtual void ToggleCheck(TNode node)
    {
        if (node.CheckedState == CheckboxState.Checked)
        {
            // 未选中节点缓存移除此节点
            uncheckedNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));
            indeterminateNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));

            // 选中节点缓存添加此节点
            if (!checkedNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                checkedNodeCache.Add(node.Value);
            }
        }
        else if (node.CheckedState == CheckboxState.UnChecked)
        {
            // 选中节点缓存添加此节点
            checkedNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));
            indeterminateNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));

            // 未选中节点缓存移除此节点
            if (!uncheckedNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                uncheckedNodeCache.Add(node.Value);
            }
        }
        else
        {
            // 不确定节点缓存添加此节点
            checkedNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));
            uncheckedNodeCache.RemoveAll(i => equalityComparer.Equals(i, node.Value));

            // 未选中节点缓存移除此节点
            if (!indeterminateNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                indeterminateNodeCache.Add(node.Value);
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
        if (node.CheckedState == CheckboxState.Checked)
        {
            // 已选中
            if (uncheckedNodeCache.Contains(node.Value, equalityComparer))
            {
                node.CheckedState = CheckboxState.UnChecked;
            }
            else if (indeterminateNodeCache.Contains(node.Value, equalityComparer))
            {
                node.CheckedState = CheckboxState.Indeterminate;
            }
            else if (!checkedNodeCache.Contains(node.Value, equalityComparer))
            {
                checkedNodeCache.Add(node.Value);
            }
        }
        else if (node.CheckedState == CheckboxState.UnChecked)
        {
            if (checkedNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                // 原来是展开状态
                node.CheckedState = CheckboxState.Checked;
            }
            else if (indeterminateNodeCache.Contains(node.Value, equalityComparer))
            {
                node.CheckedState = CheckboxState.Indeterminate;
            }
            else if (!uncheckedNodeCache.Contains(node.Value, equalityComparer))
            {
                uncheckedNodeCache.Add(node.Value);
            }
        }
        else
        {
            if (checkedNodeCache.Any(i => equalityComparer.Equals(i, node.Value)))
            {
                // 原来是展开状态
                node.CheckedState = CheckboxState.Checked;
            }
            else if (uncheckedNodeCache.Contains(node.Value, equalityComparer))
            {
                node.CheckedState = CheckboxState.UnChecked;
            }
            else if (!indeterminateNodeCache.Contains(node.Value, equalityComparer))
            {
                indeterminateNodeCache.Add(node.Value);
            }
        }
    }

    /// <summary>
    /// 
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
                IsChecked(node);

                if (node.Items.Any())
                {
                    IsChecked(node.Items.OfType<TNode>());
                }
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
            if (subNodes.Any(i => equalityComparer.Equals(i.Value, node.Value)))
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
        uncheckedNodeCache.Clear();
        checkedNodeCache.Clear();
        expandedNodeCache.Clear();
        collapsedNodeCache.Clear();
    }
}
