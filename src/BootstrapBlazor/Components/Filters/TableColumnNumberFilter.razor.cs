// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class TableColumnNumberFilter<TType>
{

    private IEnumerable<SelectedItem> _items = [];

    private TType? Value1 { get; set; }

    private FilterAction Action1 { get; set; } = FilterAction.GreaterThanOrEqual;

    private TType? Value2 { get; set; }

    private FilterAction Action2 { get; set; } = FilterAction.LessThanOrEqual;

    private bool HasFilter => TableFilter?.HasFilter ?? false;

    private string? FilterRowClassString => CssBuilder.Default("filter-row")
        .AddClass("active", HasFilter)
        .Build();

    /// <summary>
    /// 获得/设置 步长 默认 0.01
    /// </summary>
    [Parameter]
    public string Step { get; set; } = "0.01";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _items = new SelectedItem[]
        {
            new("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"].Value),
            new("LessThanOrEqual", Localizer["LessThanOrEqual"].Value),
            new("GreaterThan", Localizer["GreaterThan"].Value),
            new("LessThan", Localizer["LessThan"].Value),
            new("Equal", Localizer["Equal"].Value),
            new("NotEqual", Localizer["NotEqual"].Value)
        };
    }

    /// <summary>
    /// 重置按钮回调方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClearFilter()
    {
        if (TableFilter != null)
        {
            Reset();
            await TableFilter.OnFilterAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        Value1 = default;
        Value2 = default;
        Action1 = FilterAction.GreaterThanOrEqual;
        Action2 = FilterAction.LessThanOrEqual;
        Count = 0;
        Logic = FilterLogic.And;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = [] };
        if (Value1 != null)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value1,
                FilterAction = Action1
            });
        }

        if (Count > 0 && Value2 != null)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value2,
                FilterAction = Action2,
            });
            filter.FilterLogic = Logic;
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters?.FirstOrDefault() ?? filter;
        if (first.FieldValue is TType value)
        {
            Value1 = value;
        }
        else
        {
            Value1 = default;
        }
        Action1 = first.FilterAction;

        if (filter.Filters != null && filter.Filters.Count == 2)
        {
            Count = 1;
            FilterKeyValueAction second = filter.Filters[1];
            if (second.FieldValue is TType value2)
            {
                Value2 = value2;
            }
            else
            {
                Value2 = default;
            }
            Action2 = second.FilterAction;
            Logic = filter.FilterLogic;
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
