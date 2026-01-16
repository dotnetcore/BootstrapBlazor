// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Stack 组件</para>
///  <para lang="en">Stack Component</para>
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
    ///  <para lang="zh">获得/设置 内容</para>
    ///  <para lang="en">Get/Set Content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否为行布局 默认 false</para>
    ///  <para lang="en">Get/Set Is Row Layout. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsRow { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否反向布局 默认 false</para>
    ///  <para lang="en">Get/Set Is Reverse Layout. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsReverse { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否允许折行 默认 false</para>
    ///  <para lang="en">Get/Set Is Wrap. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsWrap { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 垂直布局模式 默认 StackAlignItems.Stretch</para>
    ///  <para lang="en">Get/Set Align Items. Default StackAlignItems.Stretch</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public StackAlignItems AlignItems { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 水平布局调整 默认 StackJustifyContent.Start</para>
    ///  <para lang="en">Get/Set Justify Content. Default StackJustifyContent.Start</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public StackJustifyContent Justify { get; set; }

    private readonly List<StackItem> _items = [];

    /// <summary>
    ///  <para lang="zh">添加子项</para>
    ///  <para lang="en">Add Item</para>
    /// </summary>
    public void AddItem(StackItem item)
    {
        _items.Add(item);
    }

    /// <summary>
    ///  <para lang="zh">移除子项</para>
    ///  <para lang="en">Remove Item</para>
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(StackItem item)
    {
        _items.Remove(item);
    }
}
