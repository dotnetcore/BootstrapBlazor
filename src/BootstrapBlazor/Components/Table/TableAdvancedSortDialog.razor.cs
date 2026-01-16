// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 高级排序弹窗的内容组件</para>
/// <para lang="en">Table 高级排序弹窗的contentcomponent</para>
/// </summary>
public partial class TableAdvancedSortDialog : ComponentBase, IResultDialog
{
    /// <summary>
    /// <para lang="zh">获得/设置 排序列列表 实例值</para>
    /// <para lang="en">Gets or sets 排序列列表 instance值</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<TableSortItem>? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 排序列列表 回调方法 支持双向绑定</para>
    /// <para lang="en">Gets or sets 排序列列表 callback method 支持双向绑定</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<List<TableSortItem>> ValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 可排序列的列表</para>
    /// <para lang="en">Gets or sets 可排序列的列表</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 增加排序条件图标</para>
    /// <para lang="en">Gets or sets 增加排序条件icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 移除排序条件图标</para>
    /// <para lang="en">Gets or sets 移除排序条件icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 减少排序条件图标</para>
    /// <para lang="en">Gets or sets 减少排序条件icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">排序规则列表</para>
    /// <para lang="en">排序规则列表</para>
    /// </summary>
    private List<SelectedItem>? SortOrders { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableAdvancedSortDialog>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
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
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
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
