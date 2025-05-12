// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class TableColumnLookupFilter : ILookup
{
    [Inject]
    [NotNull]
    private ILookupService? InjectLookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public StringComparison LookupStringComparison { get; set; }

    private Type _type = default!;
    private string? _value;
    private bool _isShowSearch;

    private List<SelectedItem>? _items;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var column = TableFilter.Column;
        Lookup = column.Lookup;
        LookupService = column.LookupService;
        LookupServiceKey = column.LookupServiceKey;
        LookupServiceData = column.LookupServiceData;
        LookupStringComparison = column.LookupStringComparison;

        _isShowSearch = column.ShowSearchWhenSelect;
        _type = column.PropertyType;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var items = new List<SelectedItem>
        {
            new("", Localizer["EnumFilter.AllText"].Value)
        };
        var lookup = await this.GetItemsAsync(InjectLookupService, LookupServiceKey, LookupServiceData);
        if (lookup != null)
        {
            items.AddRange(lookup);
        }
        _items = items;
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
        var filter = new FilterKeyValueAction() { Filters = [] };
        if (!string.IsNullOrEmpty(_value))
        {
            var type = Nullable.GetUnderlyingType(_type) ?? _type;
            var val = Convert.ChangeType(_value, type);
            filter.Filters.Add(new FilterKeyValueAction()
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
        var first = filter.Filters?.FirstOrDefault() ?? filter;
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
