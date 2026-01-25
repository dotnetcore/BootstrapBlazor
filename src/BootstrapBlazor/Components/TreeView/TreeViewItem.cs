// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TreeViewItem 组件</para>
/// <para lang="en">TreeViewItem Component</para>
/// </summary>
public class TreeViewItem<TItem> : TreeNodeBase<TItem>, ICheckableNode<TItem>
{
    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public TreeViewItem([DisallowNull] TItem item)
    {
        Value = item;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示正在加载动画，默认为 false</para>
    /// <para lang="en">Gets or sets whether to display the loading animation. Default is false</para>
    /// </summary>
    [Obsolete("已弃用(Deprecated)")]
    [ExcludeFromCodeCoverage]
    public bool ShowLoading { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被选中</para>
    /// <para lang="en">Gets or sets the checked state</para>
    /// </summary>
    public CheckboxState CheckedState { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子节点数据源</para>
    /// <para lang="en">Gets or sets the child node data source</para>
    /// </summary>
    public List<TreeViewItem<TItem>> Items { get; set; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 子节点集合</para>
    /// <para lang="en">Gets or sets the child node collection</para>
    /// </summary>
    IEnumerable<IExpandableNode<TItem>> IExpandableNode<TItem>.Items { get => Items; set => Items = [.. value.OfType<TreeViewItem<TItem>>()]; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点</para>
    /// <para lang="en">Gets or sets the parent node</para>
    /// </summary>
    public TreeViewItem<TItem>? Parent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点</para>
    /// <para lang="en">Gets or sets the parent node</para>
    /// </summary>
    IExpandableNode<TItem>? IExpandableNode<TItem>.Parent
    {
        get => Parent;
        set
        {
            Parent = null;
            if (value is TreeViewItem<TItem> item)
            {
                Parent = item;
            }
        }
    }
}
