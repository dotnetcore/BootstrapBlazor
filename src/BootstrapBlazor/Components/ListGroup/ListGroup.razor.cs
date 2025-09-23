// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ListGroup 组件
/// </summary>
public partial class ListGroup<TItem>
{
    /// <summary>
    /// 获得/设置 数据源集合
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<TItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Header 文字 默认 null
    /// </summary>
    [Parameter]
    public string? HeaderText { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 点击 List 项目回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnClickItem { get; set; }

    /// <summary>
    /// 获得/设置 双击 List 项目回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnDoubleClickItem { get; set; }

    /// <summary>
    /// 获得/设置 获得条目显示文本内容回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, string>? GetItemDisplayText { get; set; }

    private string? ClassString => CssBuilder.Default("list-group")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(TItem item) => CssBuilder.Default("list-group-item")
        .AddClass("active", Value != null && Value.Equals(item))
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
    }

    private string? GetItemText(TItem item) => GetItemDisplayText?.Invoke(item) ?? item?.ToString();

    private async Task OnClick(TItem item)
    {
        if (OnClickItem != null)
        {
            await OnClickItem(item);
        }
        CurrentValue = item;
    }

    private async Task OnDoubleClick(TItem item)
    {
        if (OnDoubleClickItem != null)
        {
            await OnDoubleClickItem(item);
        }
        CurrentValue = item;
    }
}
