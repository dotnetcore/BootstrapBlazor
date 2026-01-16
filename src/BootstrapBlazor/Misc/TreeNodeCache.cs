// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Tree 组件节点缓存类</para>
///  <para lang="en">Tree component node cache class</para>
/// </summary>
/// <typeparam name="TNode"></typeparam>
/// <typeparam name="TItem"></typeparam>
public class TreeNodeCache<TNode, TItem> : ExpandableNodeCache<TNode, TItem> where TNode : ICheckableNode<TItem>
{
    /// <summary>
    ///  <para lang="zh">获得 所有选中节点集合 作为缓存使用</para>
    ///  <para lang="en">Get all checked node collection used as cache</para>
    /// </summary>
    private readonly HashSet<TItem> _checkedNodeCache;

    /// <summary>
    ///  <para lang="zh">获得 所有未选中节点集合 作为缓存使用</para>
    ///  <para lang="en">Get all unchecked node collection used as cache</para>
    /// </summary>
    private readonly HashSet<TItem> _uncheckedNodeCache;

    /// <summary>
    ///  <para lang="zh">获得 所有半选中节点集合 作为缓存使用</para>
    ///  <para lang="en">Get all indeterminate node collection used as cache</para>
    /// </summary>
    private readonly HashSet<TItem> _indeterminateNodeCache;

    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="comparer"></param>
    public TreeNodeCache(IModelEqualityComparer<TItem> comparer) : base(comparer)
    {
        _checkedNodeCache = new(50, EqualityComparer);
        _uncheckedNodeCache = new(50, EqualityComparer);
        _indeterminateNodeCache = new(50, EqualityComparer);
    }

    /// <summary>
    ///  <para lang="zh">切换选中状态方法</para>
    ///  <para lang="en">Toggle checked state method</para>
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public void ToggleCheck(TNode node)
    {
        if (node.CheckedState == CheckboxState.Checked)
        {
            _uncheckedNodeCache.Remove(node.Value);
            _indeterminateNodeCache.Remove(node.Value);

            _checkedNodeCache.Add(node.Value);
        }
        else if (node.CheckedState == CheckboxState.UnChecked)
        {
            _checkedNodeCache.Remove(node.Value);
            _indeterminateNodeCache.Remove(node.Value);

            _uncheckedNodeCache.Add(node.Value);
        }
        else
        {
            _checkedNodeCache.Remove(node.Value);
            _uncheckedNodeCache.Remove(node.Value);

            _indeterminateNodeCache.Add(node.Value);
        }
    }

    /// <summary>
    ///  <para lang="zh">检查当前节点是否选中</para>
    ///  <para lang="en">Check whether current node is checked</para>
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private void IsChecked(TNode node)
    {
        var nodes = node.Items.OfType<ICheckableNode<TItem>>().ToList();
        if (_checkedNodeCache.Contains(node.Value))
        {
            node.CheckedState = CheckboxState.Checked;
        }
        else if (_uncheckedNodeCache.Contains(node.Value))
        {
            node.CheckedState = CheckboxState.UnChecked;
        }
        else if (_indeterminateNodeCache.Contains(node.Value))
        {
            node.CheckedState = CheckboxState.Indeterminate;
        }

        CheckChildren(nodes, node);
    }

    private void CheckChildren(List<ICheckableNode<TItem>> nodes, TNode node)
    {
        if (nodes.Count != 0)
        {
            _checkedNodeCache.Remove(node.Value);
            _uncheckedNodeCache.Remove(node.Value);
            _indeterminateNodeCache.Remove(node.Value);

            // 查看子节点状态
            if (nodes.All(i => i.CheckedState == CheckboxState.Checked))
            {
                node.CheckedState = CheckboxState.Checked;
                _checkedNodeCache.Add(node.Value);
            }
            else if (nodes.All(i => i.CheckedState == CheckboxState.UnChecked))
            {
                node.CheckedState = CheckboxState.UnChecked;
                _uncheckedNodeCache.Add(node.Value);
            }
            else
            {
                node.CheckedState = CheckboxState.Indeterminate;
                _indeterminateNodeCache.Add(node.Value);
            }
        }
    }

    /// <summary>
    ///  <para lang="zh">重置是否选中状态</para>
    ///  <para lang="en">Reset checked state</para>
    /// </summary>
    /// <param name="nodes"></param>
    public void IsChecked(List<TNode> nodes)
    {
        if (nodes.Count != 0)
        {
            ResetCheckNodes(nodes);
        }
    }

    private void ResetCheckNodes(List<TNode> items)
    {
        // 恢复当前节点状态
        foreach (var node in items)
        {
            // 恢复子节点
            if (node.Items.Any())
            {
                IsChecked(node.Items.OfType<TNode>().ToList());
            }

            // 设置本节点
            IsChecked(node);
        }
    }

    /// <summary>
    ///  <para lang="zh">通过指定节点查找父节点</para>
    ///  <para lang="en">Find parent node by specified node</para>
    /// </summary>
    /// <param name="nodes"><para lang="zh">数据集合</para><para lang="en">Data collection</para></param>
    /// <param name="node"><para lang="zh">指定节点</para><para lang="en">Specified node</para></param>
    /// <returns></returns>
    public TNode? FindParentNode(List<TNode> nodes, TNode node)
    {
        TNode? ret = default;
        foreach (var treeNode in nodes)
        {
            var subNodes = treeNode.Items.OfType<TNode>().ToList();
            if (subNodes.Any(i => EqualityComparer.Equals(i.Value, node.Value)))
            {
                ret = treeNode;
                break;
            }
            if (ret == null && subNodes.Count != 0)
            {
                ret = FindParentNode(subNodes, node);
            }
        }
        return ret;
    }

    /// <summary>
    ///  <para lang="zh">清除缓存方法</para>
    ///  <para lang="en">Clear cache method</para>
    /// </summary>
    public void Reset()
    {
        _uncheckedNodeCache.Clear();
        _checkedNodeCache.Clear();
        _indeterminateNodeCache.Clear();

        ExpandedNodeCache.Clear();
        CollapsedNodeCache.Clear();
    }
}
