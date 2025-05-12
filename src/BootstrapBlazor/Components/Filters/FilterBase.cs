// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class FilterBase : BootstrapModuleComponentBase, IFilterAction
{
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

    /// <summary>
    /// 获得/设置 Header 显示文字
    /// </summary>
    protected string? Title;

    /// <summary>
    /// 获得/设置 相关 Field 字段名称
    /// </summary>
    [NotNull]
    protected string? FieldKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected FilterLogic Logic { get; set; }

    /// <summary>
    /// 获得/设置 是否为 HeaderRow 模式 默认 false
    /// </summary>
    protected bool IsHeaderRow => TableFilter?.IsHeaderRow ?? false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (TableFilter != null)
        {
            TableFilter.FilterAction = this;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        FilterButtonText ??= Localizer[nameof(FilterButtonText)];
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];

        var column = TableFilter.Column;
        if (column != null)
        {
            Title = column.GetDisplayName();
            FieldKey = column.GetFieldName();
        }
    }

    /// <summary>
    /// 点击重置按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClickReset()
    {
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
    protected async Task OnClickConfirm()
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

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// 获得过滤窗口的所有条件方法
    /// </summary>
    /// <returns></returns>
    public abstract FilterKeyValueAction GetFilterConditions();

    /// <summary>
    /// 设置过滤集合方法
    /// </summary>
    /// <param name="filter"></param>
    public virtual Task SetFilterConditionsAsync(FilterKeyValueAction filter) => OnFilterValueChanged();
}
