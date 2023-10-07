// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table高级排序弹窗的内容组件
/// </summary>
public partial class AdvancedSort : ComponentBase, IResultDialog
{
    /// <summary>
    /// 获得/设置 排序列列表 实例值
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<SortItem>? Value { get; set; }

    /// <summary>
    /// 获得/设置 排序列列表 回调方法 支持双向绑定
    /// </summary>
    [Parameter]
    public EventCallback<List<SortItem>> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 可排序列的列表
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? SortableFields { get; set; }

    /// <summary>
    /// 获得/设置 无排序列时文本
    /// </summary>
    [Parameter]
    public string? EmptyText { get; set; }

    /// <summary>
    /// 排序规则列表
    /// </summary>
    private List<SelectedItem>? SortOrders { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<AdvancedSort>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Value ??= new();

        EmptyText ??= Localizer[nameof(EmptyText)];
        SortOrders ??= new()
        {
            new SelectedItem("Asc", Localizer["AscText"].Value),
            new SelectedItem("Desc", Localizer["DescText"].Value)
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task OnClose(DialogResult result)
    {
        if (result == DialogResult.Yes)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}
