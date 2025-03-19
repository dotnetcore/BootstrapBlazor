﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoFill component
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public partial class AutoFill<TValue>
{
    /// <summary>
    /// Gets the component style.
    /// </summary>
    private string? ClassString => CssBuilder.Default("auto-complete auto-fill")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Gets or sets the collection of items for the component.
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// Gets or sets the number of items to display when matching data. Default is null.
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// Gets or sets whether to enable fuzzy search. Default is false.
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// Gets or sets whether to ignore case when matching. Default is true.
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to expand the dropdown candidate menu when focused. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// Gets or sets the method to get the display text from the model. Default is to use the ToString override method.
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue?, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the loading icon.
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// Gets or sets the custom collection filtering rules.
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// Gets or sets whether to show the no matching data option. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    /// Gets or sets the candidate item template. Default is null.
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ItemTemplate 代替；Deprecated please use ItemTemplate parameter")]
    [ExcludeFromCodeCoverage]
    public RenderFragment<TValue>? Template { get => ItemTemplate; set => ItemTemplate = value; }

    /// <summary>
    /// Gets or sets whether virtual scrolling is enabled. Default is false.
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// Gets or sets the row height for virtual scrolling. Default is 33.
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 33f;

    /// <summary>
    /// Gets or sets the overscan count for virtual scrolling. Default is 4.
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 4;

    /// <summary>
    /// Gets or sets the callback method for loading virtualized items.
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<TValue>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// Gets or sets whether the select component is clearable. Default is false.
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private string? _displayText;

    private List<TValue>? _filterItems;

    [NotNull]
    private Virtualize<TValue>? _virtualizeElement = default;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoFillIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);

        _displayText = GetDisplayText(Value);
        Items ??= [];
    }

    /// <summary>
    /// Callback method when a candidate item is clicked.
    /// </summary>
    /// <param name="val">The value of the clicked item.</param>
    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        _displayText = GetDisplayText(val);

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    private string? GetDisplayText(TValue item) => OnGetDisplayText?.Invoke(item) ?? item?.ToString();

    private List<TValue> Rows => _filterItems ?? [.. Items];

    private int _totalCount;
    private TValue? _itemsCache;
    private ItemsProviderResult<TValue> _result;

    private async ValueTask<ItemsProviderResult<TValue>> LoadItems(ItemsProviderRequest request)
    {
        // 有搜索条件时使用原生请求数量
        // 有总数时请求剩余数量
        var count = _totalCount == 0 ? request.Count : Math.Min(request.Count, _totalCount - request.StartIndex);
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = count });

        _itemsCache = default;
        _totalCount = data.TotalCount;
        var items = data.Items ?? [];
        _result = new ItemsProviderResult<TValue>(items, _totalCount);
        return _result;
    }

    private List<TValue> GetVirtualItems()
    {
        var items = new List<TValue>();
        if (Items != null)
        {
            items.AddRange(Items);
        }
        return items;
    }

    /// <summary>
    /// Triggers the filter method.
    /// </summary>
    /// <param name="val">The value to filter by.</param>
    [JSInvokable]
    public override async Task TriggerFilter(string val)
    {
        if (OnCustomFilter != null)
        {
            var items = await OnCustomFilter(val);
            _filterItems = [.. items];
        }
        else if (string.IsNullOrEmpty(val))
        {
            _filterItems = [.. Items];
        }
        else
        {
            var comparision = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            var items = IsLikeMatch
                ? Items.Where(i => OnGetDisplayText?.Invoke(i)?.Contains(val, comparision) ?? false)
                : Items.Where(i => OnGetDisplayText?.Invoke(i)?.StartsWith(val, comparision) ?? false);
            _filterItems = [.. items];
        }

        if (DisplayCount != null)
        {
            _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
        }
        StateHasChanged();
    }

    /// <summary>
    /// Triggers the change method.
    /// </summary>
    /// <param name="val">The value to change to.</param>
    [JSInvokable]
    public override Task TriggerChange(string val)
    {
        _displayText = val;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
