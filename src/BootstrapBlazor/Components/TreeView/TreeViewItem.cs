// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// TreeItem 组件
/// </summary>
public class TreeViewItem<TItem> : TreeNodeBase<TItem>, ICheckableNode<TItem>
{
    /// <summary>
    /// 获得/设置 是否显示正在加载动画 默认为 false
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    /// 获得/设置 是否被选中
    /// </summary>
    public CheckboxState CheckedState { get; set; }

    /// <summary>
    /// 获得/设置 子节点数据源
    /// </summary>
    public List<TreeViewItem<TItem>> Items { get; set; } = new();

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    IEnumerable<IExpandableNode<TItem>> IExpandableNode<TItem>.Items { get => Items; set => Items = value.OfType<TreeViewItem<TItem>>().ToList(); }

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
