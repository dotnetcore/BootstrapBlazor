// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SimpleSelectBase 组件基类</para>
/// <para lang="en">SimpleSelectBase component base class</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class SimpleSelectBase<TValue> : SelectBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 虚拟化组件实例</para>
    /// <para lang="en">Gets virtualize component instance</para>
    /// </summary>
    [NotNull]
    protected Virtualize<SelectedItem>? _virtualizeElement = default;

    /// <summary>
    /// <para lang="zh">获得/设置 最后选中的值字符串</para>
    /// <para lang="en">Gets or sets the last selected value string</para>
    /// </summary>
    protected string _lastSelectedValueString = string.Empty;

    /// <summary>
    /// <para lang="zh">获得/设置 项目集合</para>
    /// <para lang="en">Gets or sets the items</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载虚拟化项目的回调方法</para>
    /// <para lang="en">Gets or sets the callback method for loading virtualized items</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索文本改变时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when the search text changes</para>
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择组件是否可编辑，默认为 false</para>
    /// <para lang="en">Gets or sets whether the select component is editable. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 项目模板</para>
    /// <para lang="en">Gets or sets the item template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中项目缓存</para>
    /// <para lang="en">Gets or sets the selected items cache</para>
    /// </summary>
    protected List<SelectedItem>? _itemsCache;

    /// <summary>
    /// <para lang="zh">获得 下拉菜单行数据</para>
    /// <para lang="en">Gets the dropdown menu rows</para>
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
    /// <para lang="zh">获得 按项目筛选的行数据</para>
    /// <para lang="en">Gets the rows by Items</para>
    /// </summary>
    protected abstract List<SelectedItem> GetRowsByItems();

    private List<SelectedItem> GetRowsBySearch()
    {
        var items = OnSearchTextChanged?.Invoke(SearchText) ?? FilterBySearchText(GetRowsByItems());
        return [.. items];
    }

    /// <summary>
    /// <para lang="zh">按搜索文本筛选项目</para>
    /// <para lang="en">Filter the items by search text</para>
    /// </summary>
    /// <param name="source"></param>
    protected IEnumerable<SelectedItem> FilterBySearchText(IEnumerable<SelectedItem> source) => string.IsNullOrEmpty(SearchText)
        ? source
        : source.Where(i => i.Text.Contains(SearchText, StringComparison));

    /// <summary>
    /// <para lang="zh">触发搜索回调方法</para>
    /// <para lang="en">Triggers the search callback method</para>
    /// </summary>
    /// <param name="searchText">The search text.</param>
    [JSInvokable]
    public async Task TriggerOnSearch(string searchText)
    {
        _itemsCache = null;
        SearchText = searchText;
        await RefreshVirtualizeElement();
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">刷新虚拟化组件</para>
    /// <para lang="en">Refreshes the virtualize component</para>
    /// </summary>
    protected async Task RefreshVirtualizeElement()
    {
        if (IsVirtualize && OnQueryAsync != null)
        {
            await _virtualizeElement.RefreshDataAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">清除选中值</para>
    /// <para lang="en">Clears the selected value</para>
    /// </summary>
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
