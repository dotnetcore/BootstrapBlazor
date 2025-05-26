﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 多项选择下拉框过滤组件
/// </summary>
public partial class MultiSelectFilter<TType>
{
    private string? FilterRowClassString => CssBuilder.Default("filter-row")
        .AddClass("active", TableColumnFilter.HasFilter())
        .Build();

    private TType? _value1;
    private FilterAction _action1 = FilterAction.Equal;

    /// <summary>
    /// Gets or sets the filter items.
    /// </summary>
    [Parameter]
    public List<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        _value1 = default;
        _action1 = FilterAction.Equal;
        Count = 0;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { FilterLogic = FilterLogic.Or };
        if (_value1 is string v)
        {
            var items = v.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var item in items)
            {
                filter.Filters.Add(new FilterKeyValueAction
                {
                    FieldKey = FieldKey,
                    FieldValue = item,
                    FilterAction = _action1
                });
            }
        }
        else if (_value1 is IEnumerable<string> values)
        {
            foreach (var item in values)
            {
                filter.Filters.Add(new FilterKeyValueAction
                {
                    FieldKey = FieldKey,
                    FieldValue = item,
                    FilterAction = _action1
                });
            }
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters.FirstOrDefault() ?? filter;
        if (first.FieldValue is TType value)
        {
            _value1 = value;
        }
        else
        {
            _value1 = default;
        }
        _action1 = first.FilterAction;

        await base.SetFilterConditionsAsync(filter);
    }
}
