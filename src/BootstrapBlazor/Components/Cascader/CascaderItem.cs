// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 级联选项类
/// </summary>
public class CascaderItem
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public CascaderItem() { }

    /// <summary>
    /// 构造函数
    /// </summary>
    public CascaderItem(string value, string text) => (Value, Text) = (value, text);

    /// <summary>
    /// 获得 父级节点
    /// </summary>
    public CascaderItem? Parent { get; private set; }

    /// <summary>
    /// 获得/设置 子节点数据源
    /// </summary>
    public IEnumerable<CascaderItem> Items => _items;

    private readonly List<CascaderItem> _items = new(20);

    /// <summary>
    /// 获得/设置 显示名称
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// 获得/设置 选项值
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    /// 获得 是否存在子节点
    /// </summary>
    public bool HasChildren => _items.Count > 0;

    /// <summary>
    /// 添加 CascaderItem 方法 由 CascaderItem 方法加载时调用
    /// </summary>
    /// <param name="item">CascaderItem 实例</param>
    public void AddItem(CascaderItem item)
    {
        item.Parent = this;
        _items.Add(item);
    }
}
