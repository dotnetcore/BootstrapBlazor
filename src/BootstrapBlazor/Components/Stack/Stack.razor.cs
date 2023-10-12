// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Utils;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Stack
{
    private string? ClassString => CssBuilder.Default("d-flex")
        .AddClass("flex-row", IsRow)
        .AddClass("flex-row flex-row-reverse", IsRow && IsReverse)
        .AddClass("flex-column", !IsRow)
        .AddClass("flex-column flex-column-reverse", !IsRow && IsReverse)
        .AddClass("flex-nowrap", !IsWrap)
        .AddClass("flex-wrap", IsWrap)
        .AddClass("flex-wrap flex-wrap-reverse", IsWrap && IsReverse)
        .AddClass(Justify.ToDescriptionString())
        .AddClass(AlignItems.ToDescriptionString())
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

    private List<StackItem> _items = new();

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
