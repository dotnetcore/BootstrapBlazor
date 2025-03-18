﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BootstrapBlazor.Components;

/// <summary>
/// SimpleSelectBase component base class
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class SimpleSelectBase<TValue> : SelectBase<TValue>
{
    /// <summary>
    /// Gets virtualize component instrance
    /// </summary>
    [NotNull]
    protected Virtualize<SelectedItem>? _virtualizeElement = default;

    /// <summary>
    /// Gets or sets the last selected value string.
    /// </summary>
    protected string _lastSelectedValueString = string.Empty;

    /// <summary>
    /// Gets or sets the items.
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// Gets or sets the callback method for loading virtualized items.
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// Gets or sets whether the select component is editable. Default is false.
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets the selected items cache.
    /// </summary>
    protected List<SelectedItem>? _itemsCache;

    /// <summary>
    /// Triggers the search callback method.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [JSInvokable]
    public async Task TriggerOnSearch(string searchText)
    {
        _itemsCache = null;
        SearchText = searchText;
        await RefreshVirtualizeElement();
        StateHasChanged();
    }

    /// <summary>
    /// Refreshes the virtualize component.
    /// </summary>
    /// <returns></returns>
    protected async Task RefreshVirtualizeElement()
    {
        if (IsVirtualize && OnQueryAsync != null)
        {
            // 通过 ItemProvider 提供数据
            await _virtualizeElement.RefreshDataAsync();
        }
    }

    /// <summary>
    /// Clears the selected value.
    /// </summary>
    /// <returns></returns>
    protected override async Task OnClearValue()
    {
        if (ShowSearch)
        {
            ClearSearchText();
        }
        if (OnClearAsync != null)
        {
            await OnClearAsync();
        }
        CurrentValue = default;
        if (OnQueryAsync != null)
        {
            await _virtualizeElement.RefreshDataAsync();
        }
        _lastSelectedValueString = string.Empty;
    }
}
