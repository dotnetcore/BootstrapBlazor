﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// StringFilter component
/// </summary>
public partial class StringFilter
{
    /// <summary>
    /// Gets or sets the filter candidate items. It is recommended to use static data to avoid performance loss.
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    private string? _value1;
    private FilterAction _action1 = FilterAction.Contains;
    private string? _value2;
    private FilterAction _action2 = FilterAction.Contains;

    private string? FilterRowClassString => CssBuilder.Default("filter-row")
        .AddClass("active", TableColumnFilter.HasFilter())
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Logic = FilterLogic.Or;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??=
        [
            new SelectedItem("Contains", Localizer["Contains"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value),
            new SelectedItem("NotContains", Localizer["NotContains"].Value)
        ];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        _value1 = null;
        _value2 = null;
        _action1 = FilterAction.Contains;
        _action2 = FilterAction.Contains;
        Logic = FilterLogic.Or;
        Count = 0;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (!string.IsNullOrEmpty(_value1))
        {
            filter.Filters.Add(new FilterKeyValueAction
            {
                FieldKey = FieldKey,
                FieldValue = _value1,
                FilterAction = _action1
            });
        }

        if (Count > 0 && !string.IsNullOrEmpty(_value2))
        {
            filter.Filters.Add(new FilterKeyValueAction
            {
                FieldKey = FieldKey,
                FieldValue = _value2,
                FilterAction = _action2,
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
        FilterKeyValueAction first = filter.Filters.FirstOrDefault() ?? filter;
        if (first.FieldValue is string value)
        {
            _value1 = value;
        }
        else
        {
            _value1 = null;
        }
        _action1 = first.FilterAction;

        if (filter.Filters.Count > 1)
        {
            Count = 1;
            FilterKeyValueAction second = filter.Filters[1];
            if (second.FieldValue is string value2)
            {
                _value2 = value2;
            }
            else
            {
                _value2 = null;
            }
            _action2 = second.FilterAction;
            Logic = second.FilterLogic;
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
