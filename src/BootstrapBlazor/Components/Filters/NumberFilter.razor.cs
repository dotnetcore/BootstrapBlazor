// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class NumberFilter<TType>
{
    private TType? _value1;
    private FilterAction _action1 = FilterAction.GreaterThanOrEqual;
    private TType? _value2;
    private FilterAction _action2 = FilterAction.LessThanOrEqual;
    private string? _step;

    private bool HasFilter => TableFilter?.HasFilter ?? false;

    private string? FilterRowClassString => CssBuilder.Default("filter-row")
        .AddClass("active", HasFilter)
        .Build();

    private IEnumerable<SelectedItem> _items = [];

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
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _step = TableFilter.Column.Step;
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
        _value1 = default;
        _value2 = default;
        _action1 = FilterAction.GreaterThanOrEqual;
        _action2 = FilterAction.LessThanOrEqual;
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
        if (_value1 != null)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = _value1,
                FilterAction = _action1
            });
        }

        if (Count > 0 && _value2 != null)
        {
            filter.Filters.Add(new FilterKeyValueAction()
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
        var first = filter.Filters?.FirstOrDefault() ?? filter;
        if (first.FieldValue is TType value)
        {
            _value1 = value;
        }
        else
        {
            _value1 = default;
        }
        _action1 = first.FilterAction;

        if (filter.Filters != null && filter.Filters.Count == 2)
        {
            Count = 1;
            FilterKeyValueAction second = filter.Filters[1];
            if (second.FieldValue is TType value2)
            {
                _value2 = value2;
            }
            else
            {
                _value2 = default;
            }
            _action2 = second.FilterAction;
            Logic = filter.FilterLogic;
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
