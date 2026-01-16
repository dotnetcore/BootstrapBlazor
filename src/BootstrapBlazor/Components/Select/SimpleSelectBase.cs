// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SimpleSelectBase component base class
///</para>
/// <para lang="en">SimpleSelectBase component base class
///</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class SimpleSelectBase<TValue> : SelectBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 virtualize component 实例
    ///</para>
    /// <para lang="en">Gets virtualize component instance
    ///</para>
    /// </summary>
    [NotNull]
    protected Virtualize<SelectedItem>? _virtualizeElement = default;

    /// <summary>
    /// <para lang="zh">获得/设置 the last selected value string.
    ///</para>
    /// <para lang="en">Gets or sets the last selected value string.
    ///</para>
    /// </summary>
    protected string _lastSelectedValueString = string.Empty;

    /// <summary>
    /// <para lang="zh">获得/设置 the items.
    ///</para>
    /// <para lang="en">Gets or sets the items.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 for loading virtualized items.
    ///</para>
    /// <para lang="en">Gets or sets the callback method for loading virtualized items.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 when the search text changes.
    ///</para>
    /// <para lang="en">Gets or sets the callback method when the search text changes.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the select component is editable. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether the select component is editable. Default is false.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the item 模板.
    ///</para>
    /// <para lang="en">Gets or sets the item template.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the selected items cache.
    ///</para>
    /// <para lang="en">Gets or sets the selected items cache.
    ///</para>
    /// </summary>
    protected List<SelectedItem>? _itemsCache;

    /// <summary>
    /// <para lang="zh">获得 the dropdown menu rows.
    ///</para>
    /// <para lang="en">Gets the dropdown menu rows.
    ///</para>
    /// </summary>
    protected List<SelectedItem> Rows
    {
        get
        {
            _itemsCache ??= string.IsNullOrEmpty(SearchText) ? GetRowsByItems() : GetRowsBySearch();
            return _itemsCache;
        }
    }

    /// <summary>
    /// <para lang="zh">获得 the rows by Items.
    ///</para>
    /// <para lang="en">Gets the rows by Items.
    ///</para>
    /// </summary>
    /// <returns></returns>
    protected abstract List<SelectedItem> GetRowsByItems();

    private List<SelectedItem> GetRowsBySearch()
    {
        var items = OnSearchTextChanged?.Invoke(SearchText) ?? FilterBySearchText(GetRowsByItems());
        return [.. items];
    }

    /// <summary>
    /// <para lang="zh">Filter the items by search text.
    ///</para>
    /// <para lang="en">Filter the items by search text.
    ///</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    protected IEnumerable<SelectedItem> FilterBySearchText(IEnumerable<SelectedItem> source) => string.IsNullOrEmpty(SearchText)
        ? source
        : source.Where(i => i.Text.Contains(SearchText, StringComparison));

    /// <summary>
    /// <para lang="zh">Triggers the search 回调方法.
    ///</para>
    /// <para lang="en">Triggers the search callback method.
    ///</para>
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
    /// <para lang="zh">Refreshes the virtualize component.
    ///</para>
    /// <para lang="en">Refreshes the virtualize component.
    ///</para>
    /// </summary>
    /// <returns></returns>
    protected async Task RefreshVirtualizeElement()
    {
        if (IsVirtualize && OnQueryAsync != null)
        {
            // <para lang="zh">通过 ItemProvider 提供数据</para>
            // <para lang="en">Data provided by ItemProvider</para>
            await _virtualizeElement.RefreshDataAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">Clears the selected value.
    ///</para>
    /// <para lang="en">Clears the selected value.
    ///</para>
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
