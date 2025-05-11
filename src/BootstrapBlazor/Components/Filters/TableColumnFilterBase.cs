// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class TableColumnFilterBase : BootstrapModuleComponentBase
{
    /// <summary>
    /// 获得/设置 增加过滤条件图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// 获得/设置 减少过滤条件图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// 重置按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 过滤按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FilterButtonText { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    protected IStringLocalizer<TableFilter>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 所属 TableFilter 实例
    /// </summary>
    [CascadingParameter, NotNull]
    protected TableFilter? TableFilter { get; set; }

    /// <summary>
    /// 获得/设置 是否显示增加减少条件按钮
    /// </summary>
    public bool ShowMoreButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 Header 显示文字
    /// </summary>
    protected string? Title;

    /// <summary>
    /// 获得/设置 组件步长
    /// </summary>
    protected string? Step { get; set; }

    /// <summary>
    /// 获得/设置 条件数量
    /// </summary>
    protected int Count { get; set; }

    /// <summary>
    /// 获得/设置 相关 Field 字段名称
    /// </summary>
    [NotNull]
    protected string? FieldKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected virtual FilterLogic Logic { get; set; }

    /// <summary>
    /// 获得/设置 是否为 HeaderRow 模式 默认 false
    /// </summary>
    protected bool IsHeaderRow => TableFilter?.IsHeaderRow ?? false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        FilterButtonText ??= Localizer[nameof(FilterButtonText)];
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];

        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterPlusIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterMinusIcon);

        var column = TableFilter.Column;
        if (column != null)
        {
            Title = column.GetDisplayName();
            Step = column.Step;
            FieldKey = column.GetFieldName();
        }
    }

    /// <summary>
    /// 点击重置按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickReset()
    {
        Count = 0;

        var table = TableFilter.Table;
        if (table != null)
        {
            TableFilter.FilterAction.Reset();
            table.Filters.Remove(FieldKey);

            if (table.OnFilterAsync != null)
            {
                await table.OnFilterAsync();
            }
        }
    }

    /// <summary>
    /// 点击确认时回调此方法
    /// </summary>
    /// <returns></returns>
    protected Task OnClickConfirm() => OnFilterAsync();

    /// <summary>
    /// 过滤数据方法
    /// </summary>
    /// <returns></returns>
    private async Task OnFilterAsync()
    {
        var table = TableFilter.Table;
        if (table != null)
        {
            var f = TableFilter.FilterAction.GetFilterConditions();
            if (f.Filters != null && f.Filters.Count > 0)
            {
                table.Filters[FieldKey] = TableFilter.FilterAction;
            }
            else
            {
                table.Filters.Remove(FieldKey);
            }
            if (table.OnFilterAsync != null)
            {
                await table.OnFilterAsync();
            }
        }
    }

    /// <summary>
    /// 点击增加按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected void OnClickPlus()
    {
        if (Count == 0)
        {
            Count++;
        }
    }

    /// <summary>
    /// 点击减少按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected void OnClickMinus()
    {
        if (Count == 1)
        {
            Count--;
        }
    }

    /// <summary>
    /// 过滤按钮回调方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnFilterValueChanged()
    {
        if (TableFilter != null)
        {
            await TableFilter.OnFilterAsync();
            StateHasChanged();
        }
    }
}
