// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Stack
{
    private string? ClassString => CssBuilder.Default("bb-stack d-flex")
        .AddClass("flex-row", IsRow)
        .AddClass("flex-row flex-row-reverse", IsRow && IsReverse)
        .AddClass("flex-column", !IsRow)
        .AddClass("flex-column flex-column-reverse", !IsRow && IsReverse)
        .AddClass("flex-nowrap", !IsWrap)
        .AddClass("flex-wrap", IsWrap)
        .AddClass("flex-wrap flex-wrap-reverse", IsWrap && IsReverse)
        .AddClass(Justify.ToDescriptionString())
        .AddClass(AlignItems.ToDescriptionString())
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private static string? ItemClassString(StackItem item) => CssBuilder.Default("bb-stack-item")
        .AddClass("flex-fill", item.IsFill)
        .AddClass(item.AlignSelf.ToDescriptionString().Replace("align-items", "align-self"), item.AlignSelf != StackAlignItems.Stretch)
        .AddClassFromAttributes(item.AdditionalAttributes)
       .Build();

    /// <summary>
    /// 获得/设置 内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否为行布局 默认 false
    /// </summary>
    [Parameter]
    public bool IsRow { get; set; }

    /// <summary>
    /// 获得/设置 是否反向布局 默认 false
    /// </summary>
    [Parameter]
    public bool IsReverse { get; set; }

    /// <summary>
    /// 获得/设置 是否允许折行 默认 false
    /// </summary>
    [Parameter]
    public bool IsWrap { get; set; }

    /// <summary>
    /// 获得/设置 垂直布局模式 默认 StackAlignItems.Stretch
    /// </summary>
    [Parameter]
    public StackAlignItems AlignItems { get; set; }

    /// <summary>
    /// 获得/设置 水平布局调整 默认 StackJustifyContent.Start
    /// </summary>
    [Parameter]
    public StackJustifyContent Justify { get; set; }

    private readonly List<StackItem> _items = [];

    /// <summary>
    /// 添加子项
    /// </summary>
    public void AddItem(StackItem item)
    {
        _items.Add(item);
    }

    /// <summary>
    /// 移除子项
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(StackItem item)
    {
        _items.Remove(item);
    }
}
