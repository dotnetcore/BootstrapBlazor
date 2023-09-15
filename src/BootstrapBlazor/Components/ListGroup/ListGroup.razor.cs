// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ListGroup 组件
/// </summary>
public partial class ListGroup<TItem> where TItem : notnull
{
    /// <summary>
    /// 获得/设置 数据源集合
    /// </summary>
    [Parameter]
    [NotNull]
#if NET5_0_OR_GREATER
    [EditorRequired]
#endif
    public List<TItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 获得条目显示文本内容回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, string>? GetItemDisplayText { get; set; }

    private string? ClassString => CssBuilder.Default("bb-list-group")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(TItem item) => CssBuilder.Default("list-group-item")
        .AddClass("active", Value.Equals(item))
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= new();
    }

    private string? GetItemText(TItem item) => GetItemDisplayText?.Invoke(item) ?? item.ToString();
}
