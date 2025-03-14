// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TreeItem 组件
/// </summary>
public class TreeViewItem<TItem> : TreeNodeBase<TItem>, ICheckableNode<TItem>
{
    /// <summary>
    /// 获得/设置 是否显示正在加载动画 默认为 false
    /// </summary>
    [Obsolete("已弃用；Deprecated")]
    [ExcludeFromCodeCoverage]
    public bool ShowLoading { get; set; }

    /// <summary>
    /// 获得/设置 是否被选中
    /// </summary>
    public CheckboxState CheckedState { get; set; }

    /// <summary>
    /// 获得/设置 子节点数据源
    /// </summary>
    public List<TreeViewItem<TItem>> Items { get; set; } = [];

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    IEnumerable<IExpandableNode<TItem>> IExpandableNode<TItem>.Items { get => Items; set => Items = [.. value.OfType<TreeViewItem<TItem>>()]; }

    /// <summary>
    /// 获得/设置 父级节点
    /// </summary>
    public TreeViewItem<TItem>? Parent { get; set; }

    /// <summary>
    /// 获得/设置 父级节点
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

    /// <summary>
    /// 构造函数
    /// </summary>
    public TreeViewItem([DisallowNull] TItem item)
    {
        Value = item;
    }

    ///// <summary>
    ///// 级联设置展开状态方法
    ///// </summary>
    //public void CollapseOtherNodes()
    //{
    //    if (!string.IsNullOrEmpty(ParentId))
    //    {
    //        var parent = Items.FirstOrDefault(i => i.Id)
    //        foreach (var node in Parent.Items.Where(p => p.IsExpanded && p != this))
    //        {
    //            node.IsExpanded = false;
    //        }
    //    }
    //}
}
