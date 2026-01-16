// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">级联选项类</para>
///  <para lang="en">Cascader Item class</para>
/// </summary>
public class CascaderItem
{
    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">Constructor</para>
    /// </summary>
    public CascaderItem() { }

    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">Constructor</para>
    /// </summary>
    public CascaderItem(string value, string text) => (Value, Text) = (value, text);

    /// <summary>
    ///  <para lang="zh">获得 父级节点</para>
    ///  <para lang="en">Get Parent node</para>
    /// </summary>
    public CascaderItem? Parent { get; private set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 子节点数据源</para>
    ///  <para lang="en">Gets or sets the child node data source</para>
    /// </summary>
    public IEnumerable<CascaderItem> Items => _items;

    private readonly List<CascaderItem> _items = new(20);

    /// <summary>
    ///  <para lang="zh">获得/设置 显示名称</para>
    ///  <para lang="en">Gets or sets the display text</para>
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    ///  <para lang="zh">获得/设置 选项值</para>
    ///  <para lang="en">Gets or sets the option value</para>
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    ///  <para lang="zh">获得 是否存在子节点</para>
    ///  <para lang="en">Get whether there are child nodes</para>
    /// </summary>
    public bool HasChildren => _items.Count > 0;

    /// <summary>
    ///  <para lang="zh">添加 CascaderItem 方法 由 CascaderItem 方法加载时调用</para>
    ///  <para lang="en">Add CascaderItem method called when CascaderItem method loads</para>
    /// </summary>
    /// <param name="item"><para lang="zh">CascaderItem 实例</para><para lang="en">CascaderItem instance</para></param>
    public void AddItem(CascaderItem item)
    {
        item.Parent = this;
        _items.Add(item);
    }
}
