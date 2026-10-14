// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Lookup 过滤器</para>
/// <para lang="en">Lookup Filter</para>
/// </summary>
public partial class LookupFilter
{
    [Inject]
    [NotNull]
    private ILookupService? InjectLookupService { get; set; }

    private Type _type = null!;
    private string? _value;
    private bool _isShowSearch;
    private ILookup _lookup = null!;
    private IEnumerable<SelectedItem>? _items;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (TableColumnFilter != null)
        {
            var column = TableColumnFilter.Column;
            _isShowSearch = column.ShowSearchWhenSelect;
            _type = column.PropertyType;
            _lookup = column;

            var service = _lookup.GetLookupService(InjectLookupService);
            var items = await _lookup.GetItemsAsync(service, _lookup.LookupServiceKey, _lookup.LookupServiceData);
            _items = AddNullFilterValues(items ?? []);
            if (string.IsNullOrEmpty(_value))
            {
                _value = _items.FirstOrDefault(item => item.Value != NullFilterValue && item.Value != NotNullFilterValue)?.Value;
            }
        }
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.Reset"/>
    /// </summary>
    public override void Reset()
    {
        _value = null;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.GetFilterConditions"/>
    /// </summary>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (GetNullFilterAction(_value) is { } nullAction)
        {
            filter.Filters.Add(CreateCondition(null, nullAction));
        }
        else if (!string.IsNullOrEmpty(_value))
        {
            var type = Nullable.GetUnderlyingType(_type) ?? _type;
            var val = Convert.ChangeType(_value, type);
            filter.Filters.Add(CreateCondition(val, FilterAction.Equal));
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.SetFilterConditionsAsync(FilterKeyValueAction)"/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters.FirstOrDefault() ?? filter;
        var type = Nullable.GetUnderlyingType(_type) ?? _type;
        if (IsNullFilterAction(first.FilterAction))
        {
            _value = first.FilterAction == FilterAction.IsNull ? NullFilterValue : NotNullFilterValue;
        }
        else if (first.FieldValue != null && first.FieldValue.GetType() == type)
        {
            _value = first.FieldValue.ToString();
        }
        else
        {
            _value = null;
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
