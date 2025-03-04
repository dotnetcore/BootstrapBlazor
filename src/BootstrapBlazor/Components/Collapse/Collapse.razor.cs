// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Collapse 组件
/// </summary>
public partial class Collapse
{
    private static string? GetHeaderButtonClassString(CollapseItem item) => CssBuilder.Default("accordion-button")
        .AddClass("collapsed", item.IsCollapsed)
        .AddClass($"bg-{item.TitleColor.ToDescriptionString()}", item.TitleColor != Color.None)
        .Build();

    private static string? GetItemIconString(CollapseItem item) => CssBuilder.Default("accordion-item-icon")
        .AddClass(item.Icon)
        .Build();

    private static string? GetHeaderClassString(CollapseItem item) => CssBuilder.Default("accordion-header")
        .AddClass($"bg-{item.TitleColor.ToDescriptionString()}", item.TitleColor != Color.None)
        .AddClass(item.HeaderClass)
        .Build();

    private static string? GetClassString(bool collapsed) => CssBuilder.Default("accordion-collapse collapse")
        .AddClass("show", !collapsed)
        .Build();

    private string? ClassString => CssBuilder.Default("accordion")
        .AddClass("is-accordion", IsAccordion)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private static string? GetItemClassString(CollapseItem item) => CssBuilder.Default("accordion-item")
        .AddClass(item.Class, !string.IsNullOrEmpty(item.Class))
        .Build();

    private string GetTargetId(CollapseItem item) => ComponentIdGenerator.Generate(item);

    private string? GetTargetIdString(CollapseItem item) => $"#{GetTargetId(item)}";

    private string? ParentIdString => IsAccordion ? $"#{Id}" : null;

    /// <summary>
    /// 获得/设置 CollapseItem 集合
    /// </summary>
    protected List<CollapseItem> Items { get; } = new(10);

    /// <summary>
    /// 获得/设置 是否为手风琴效果 默认为 false
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// 获得/设置 CollapseItems 模板
    /// </summary>
    [Parameter]
    public RenderFragment? CollapseItems { get; set; }

    /// <summary>
    /// 获得/设置 CollapseItem 展开收缩时回调方法
    /// </summary>
    [Parameter]
    public Func<CollapseItem, Task>? OnCollapseChanged { get; set; }

    private async Task OnClickItem(CollapseItem item)
    {
        if (IsAccordion && item.IsCollapsed)
        {
            // 手风琴模式，设置其他项收起
            foreach (var i in Items.Where(i => i != item && !i.IsCollapsed))
            {
                i.SetCollapsed(true);
            }
        }
        item.SetCollapsed(!item.IsCollapsed);
        if (OnCollapseChanged != null)
        {
            await OnCollapseChanged(item);
        }
    }

    /// <summary>
    /// 添加 CollapseItem 方法 由 CollapseItem 方法加载时调用
    /// </summary>
    /// <param name="item">TabItemBase 实例</param>
    internal void AddItem(CollapseItem item) => Items.Add(item);

    /// <summary>
    /// 移除 CollapseItem 方法 由 CollapseItem 方法 Dispose 时调用
    /// </summary>
    /// <param name="item">TabItemBase 实例</param>
    internal void RemoveItem(CollapseItem item) => Items.Remove(item);
}
