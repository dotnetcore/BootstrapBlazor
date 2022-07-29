// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// TreeItem 组件
/// </summary>
public class TreeItem<TItem> : NodeBase<TItem>, ICheckableNode<TItem>
{
    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式名
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 获得/设置 是否被禁用 默认 false
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 是否选中当前节点 默认 false
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 是否显示正在加载动画 默认为 false
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    /// 获得/设置 是否被选中
    /// </summary>
    public bool Checked { get; set; }

    /// <summary>
    /// 获得/设置 子组件模板 默认为 null
    /// </summary>
    public RenderFragment<TItem>? Template { get; set; }

    /// <summary>
    /// 获得/设置 子节点数据源
    /// </summary>
    public List<TreeItem<TItem>> Items { get; set; } = new();

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    IEnumerable<IExpandableNode<TItem>> IExpandableNode<TItem>.Items { get => Items; set => Items = value.OfType<TreeItem<TItem>>().ToList() ?? new(); }

    /// <summary>
    /// 获得 所有子项集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TreeItem<TItem>> GetAllSubItems() => Items.Concat(GetSubItems(Items));

    private static IEnumerable<TreeItem<TItem>> GetSubItems(IEnumerable<TreeItem<TItem>> items) => items.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);

    /// <summary>
    /// 构造函数
    /// </summary>
    public TreeItem([DisallowNull] TItem item)
    {
        Value = item;
    }

    /// <summary>
    /// 级联设置复选状态
    /// </summary>
    public void CascadeSetCheck(bool isChecked)
    {
        foreach (var item in Items)
        {
            item.Checked = isChecked;
            if (item.Items.Any())
            {
                item.CascadeSetCheck(isChecked);
            }
        }
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
