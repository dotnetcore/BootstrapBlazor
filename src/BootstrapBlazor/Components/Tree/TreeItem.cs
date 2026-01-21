// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TreeItem 组件</para>
/// <para lang="en">TreeItem Component</para>
/// </summary>
[ExcludeFromCodeCoverage]
public class TreeItem : NodeItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示正在加载动画 默认为 false</para>
    /// <para lang="en">Gets or sets whether display loading backdrop Default is false</para>
    /// </summary>
    public bool ShowLoading { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子节点数据源</para>
    /// <para lang="en">Gets or sets 子节点data源</para>
    /// </summary>
    public List<TreeItem> Items { get; set; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 TreeItem 标识</para>
    /// <para lang="en">Gets or sets TreeItem 标识</para>
    /// </summary>
    public object? Key { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 TreeItem 相关额外信息</para>
    /// <para lang="en">Gets or sets TreeItem 相关额外信息</para>
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被选中</para>
    /// <para lang="en">Gets or sets whether被选中</para>
    /// </summary>
    public bool Checked { get; set; }

    /// <summary>
    /// <para lang="zh">获取/设置 是否有子节点 默认 false</para>
    /// <para lang="en">获取/Sets whether有子节点 Default is false</para>
    /// </summary>
    public bool HasChildNode { get; set; }

    /// <summary>
    /// <para lang="zh">获得 所有子项集合</para>
    /// <para lang="en">Gets 所有子项collection</para>
    /// </summary>
    public IEnumerable<TreeItem> GetAllSubItems() => Items.Concat(GetSubItems(Items));

    private static IEnumerable<TreeItem> GetSubItems(List<TreeItem> items) => items.SelectMany(i => i.Items.Count > 0 ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);

    /// <summary>
    /// <para lang="zh">级联设置复选状态</para>
    /// <para lang="en">级联Sets复选状态</para>
    /// </summary>
    public void CascadeSetCheck(bool isChecked)
    {
        foreach (var item in Items)
        {
            item.Checked = isChecked;
            if (item.Items.Count > 0)
            {
                item.CascadeSetCheck(isChecked);
            }
        }
    }

    ///// <summary>
    ///// <para lang="zh">级联设置展开状态方法</para>
    ///// <para lang="en">级联Sets展开状态方法</para>
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
