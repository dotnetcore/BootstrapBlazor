// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 高级排序弹窗的内容组件
/// </summary>
public partial class TableAdvancedSortDialog : ComponentBase, IResultDialog
{
    /// <summary>
    /// 获得/设置 排序列列表 实例值
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<TableSortItem>? Value { get; set; }

    /// <summary>
    /// 获得/设置 排序列列表 回调方法 支持双向绑定
    /// </summary>
    [Parameter]
    public EventCallback<List<TableSortItem>> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 可排序列的列表
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 增加排序条件图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// 获得/设置 移除排序条件图标
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// 获得/设置 减少排序条件图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// 排序规则列表
    /// </summary>
    private List<SelectedItem>? SortOrders { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableAdvancedSortDialog>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Value ??= [];

        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderPlusIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderMinusIcon);
        RemoveIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderRemoveIcon);

        SortOrders ??=
        [
            new SelectedItem("Asc", Localizer["AscText"].Value),
            new SelectedItem("Desc", Localizer["DescText"].Value)
        ];
    }

    private void OnClickAdd()
    {
        Value.Add(new TableSortItem());
    }

    private void OnClickClear()
    {
        Value.Clear();
    }

    private void OnClickRemove(TableSortItem item)
    {
        Value.Remove(item);
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
