// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TreeItem 组件
/// </summary>
[ExcludeFromCodeCoverage]
public class TreeItem : NodeItem
{
    /// <summary>
    /// 获得/设置 是否显示正在加载动画 默认为 false
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    /// 获得/设置 子节点数据源
    /// </summary>
    public List<TreeItem> Items { get; set; } = [];

    /// <summary>
    /// 获得/设置 TreeItem 标识
    /// </summary>
    public object? Key { get; set; }

    /// <summary>
    /// 获得/设置 TreeItem 相关额外信息
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// 获得/设置 是否被选中
    /// </summary>
    public bool Checked { get; set; }

    /// <summary>
    /// 获取/设置 是否有子节点 默认 false 
    /// </summary>
    public bool HasChildNode { get; set; }

    /// <summary>
    /// 获得 所有子项集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TreeItem> GetAllSubItems() => Items.Concat(GetSubItems(Items));

    private static IEnumerable<TreeItem> GetSubItems(List<TreeItem> items) => items.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);

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
