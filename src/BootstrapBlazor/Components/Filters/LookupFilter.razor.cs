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
    private Type _type = null!;
    private string? _value;
    private bool _isShowSearch;
    private ILookup _lookup = null!;

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

            if (string.IsNullOrEmpty(_value))
            {
                var service = _lookup.LookupService;
                if (service != null)
                {
                    var items = await _lookup.GetItemsAsync(service, _lookup.LookupServiceKey, _lookup.LookupServiceData);
                    if (items != null)
                    {
                        _value = items.FirstOrDefault()?.Value;
                    }
                }
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        _value = null;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction();
        if (!string.IsNullOrEmpty(_value))
        {
            var type = Nullable.GetUnderlyingType(_type) ?? _type;
            var val = Convert.ChangeType(_value, type);
            filter.Filters.Add(new FilterKeyValueAction
            {
                FieldKey = FieldKey,
                FieldValue = val,
                FilterAction = FilterAction.Equal
            });
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters.FirstOrDefault() ?? filter;
        var type = Nullable.GetUnderlyingType(_type) ?? _type;
        if (first.FieldValue != null && first.FieldValue.GetType() == type)
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
