// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Collapse 组件
/// </summary>
public partial class Collapse
{
    private static string? GetButtonClassString(CollapseItem item) => CssBuilder.Default("accordion-button")
        .AddClass("collapsed", item.IsCollapsed)
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
    protected List<CollapseItem> Children { get; } = new(10);

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
    internal void AddItem(CollapseItem item) => Children.Add(item);

    /// <summary>
    /// 移除 CollapseItem 方法 由 CollapseItem 方法 Dispose 时调用
    /// </summary>
    /// <param name="item">TabItemBase 实例</param>
    internal void RemoveItem(CollapseItem item) => Children.Remove(item);
}
