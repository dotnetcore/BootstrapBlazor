// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DropdownWidget 组件</para>
/// <para lang="en">DropdownWidget Component</para>
/// </summary>
public sealed partial class DropdownWidget
{
    private string? ClassString => CssBuilder.Default("widget")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 选项模板支持静态数据</para>
    /// <para lang="en">Get/Set Child Content (Static Data)</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 挂件数据集合</para>
    /// <para lang="en">Get/Set Widget Items</para>
    /// </summary>
    [Parameter]
    public IEnumerable<DropdownWidgetItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉项关闭回调方法</para>
    /// <para lang="en">Get/Set Item Close Callback</para>
    /// </summary>
    [Parameter]
    public Func<DropdownWidgetItem, Task>? OnItemCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉项显示回调方法</para>
    /// <para lang="en">Get/Set Item Shown Callback</para>
    /// </summary>
    [Parameter]
    public Func<DropdownWidgetItem, Task>? OnItemShownAsync { get; set; }

    private List<DropdownWidgetItem> Childs { get; } = new List<DropdownWidgetItem>(20);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { Method = nameof(TriggerStateChanged) });

    /// <summary>
    /// <para lang="zh">添加 DropdownWidgetItem 方法</para>
    /// <para lang="en">Add DropdownWidgetItem Method</para>
    /// </summary>
    /// <param name="item"></param>
    internal void Add(DropdownWidgetItem item)
    {
        Childs.Add(item);
    }

    private IEnumerable<DropdownWidgetItem> GetItems() => Items == null ? Childs : Childs.Concat(Items);

    /// <summary>
    /// <para lang="zh">Widget 下拉项关闭回调方法 由 JavaScript 调用</para>
    /// <para lang="en">Widget Item State Changed Callback. Called by JavaScript</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="shown"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerStateChanged(int index, bool shown)
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
