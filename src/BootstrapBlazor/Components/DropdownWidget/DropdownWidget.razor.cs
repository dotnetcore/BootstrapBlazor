// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DropdownWidget 组件
/// </summary>
public sealed partial class DropdownWidget
{
    private string? ClassString => CssBuilder.Default("widget")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 选项模板支持静态数据
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 挂件数据集合
    /// </summary>
    [Parameter]
    public IEnumerable<DropdownWidgetItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 下拉项关闭回调方法
    /// </summary>
    [Parameter]
    public Func<DropdownWidgetItem, Task>? OnItemCloseAsync { get; set; }

    /// <summary>
    /// 获得/设置 下拉项关闭回调方法
    /// </summary>
    [Parameter]
    public Func<DropdownWidgetItem, Task>? OnItemShownAsync { get; set; }

    private List<DropdownWidgetItem> Childs { get; } = new List<DropdownWidgetItem>(20);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { Method = nameof(OnWidgetItemClosed) });

    /// <summary>
    /// 添加 DropdownWidgetItem 方法
    /// </summary>
    /// <param name="item"></param>
    internal void Add(DropdownWidgetItem item)
    {
        Childs.Add(item);
    }

    private IEnumerable<DropdownWidgetItem> GetItems() => Items == null ? Childs : Childs.Concat(Items);

    /// <summary>
    /// Widget 下拉项关闭回调方法 由 JavaScript 调用
    /// </summary>
    /// <param name="index"></param>
    /// <param name="shown"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnWidgetItemClosed(int index, bool shown)
    {
        var items = GetItems().ToList();
        var item = index < items.Count ? items[index] : null;
        if (item != null)
        {
            if (OnItemCloseAsync != null && !shown)
            {
                await OnItemCloseAsync(item);
            }
            else if (OnItemShownAsync != null && shown)
            {
                await OnItemShownAsync(item);
            }
        }
    }
}
