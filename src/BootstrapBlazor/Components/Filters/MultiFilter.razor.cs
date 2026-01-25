// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">多选过滤器组件</para>
/// <para lang="en">Multi-Select Filter Component</para>
/// </summary>
public partial class MultiFilter
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索栏占位符 默认 nul 使用资源文件中值</para>
    /// <para lang="en">Gets or sets Search Placeholder Default null Use Resource File Value</para>
    /// </summary>
    [Parameter]
    public string? SearchPlaceHolderText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 全选按钮文本 默认 nul 使用资源文件中值</para>
    /// <para lang="en">Gets or sets Select All Button Text Default null Use Resource File Value</para>
    /// </summary>
    [Parameter]
    public string? SelectAllText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索栏 默认 true</para>
    /// <para lang="en">Gets or sets Whether to Show Search Bar Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得 过滤项集合回调方法 适合动态给定数据源</para>
    /// <para lang="en">Get Filter Items Callback Method Suitable for Dynamic Data Source</para>
    /// </summary>
    [Parameter]
    public Func<Task<List<SelectedItem>>>? OnGetItemsAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得 是否每次弹窗时均调用 <see cref="OnGetItemsAsync"/> 回调方法，多用于动态填装过滤条件</para>
    /// <para lang="en">Get Whether to Call <see cref="OnGetItemsAsync"/> Callback Method Every Time Popup, Mostly Used for Dynamic Filling Filter Conditions</para>
    /// </summary>
    [Parameter]
    public bool AlwaysTriggerGetItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Loading 模板</para>
    /// <para lang="en">Gets or sets Loading Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the string comparison option used for filtering operations. 默认为 <see cref="StringComparison.OrdinalIgnoreCase"/></para>
    /// <para lang="en">Gets or sets the string comparison option used for filtering operations. Default is <see cref="StringComparison.OrdinalIgnoreCase"/></para>
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    private string? _searchText;
    private List<SelectedItem>? _source;
    private List<SelectedItem>? _items;

    /// <summary>
    /// <para lang="zh">获得/设置 the filter candidate items. It is recommended to use static 数据 to avoid performance loss.</para>
    /// <para lang="en">Gets or sets the filter candidate items. It is recommended to use static data to avoid performance loss.</para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Items != null && OnGetItemsAsync != null)
        {
            throw new InvalidOperationException($"{GetType()} can only accept one item source from its parameters. Do not supply both '{nameof(Items)}' and '{nameof(OnGetItemsAsync)}'.");
        }

        SearchPlaceHolderText ??= Localizer["MultiFilterSearchPlaceHolderText"];
        SelectAllText ??= Localizer["MultiFilterSelectAllText"];

        if (Items != null)
        {
            var selectedItems = _source?.Where(x => x.Active).ToList();
            _source = [.. Items];
            ResetActiveItems(_source, selectedItems);
        }
    }

    private static void ResetActiveItems(List<SelectedItem> source, List<SelectedItem>? activeItems)
    {
        if (activeItems != null)
        {
            foreach (var active in activeItems)
            {
                var item = source.Find(i => i.Value == active.Value);
                if (item != null)
                {
                    item.Active = true;
                }
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task InvokeInitAsync()
    {
        if (OnGetItemsAsync != null)
        {
            await InvokeVoidAsync("init", Id, new
            {
                Invoker = Interop,
                Callback = nameof(TriggerGetItemsCallback),
                AlwaysTrigger = AlwaysTriggerGetItems
            });
        }
    }

    /// <summary>
    /// <para lang="zh">JavaScript 回调方法</para>
    /// <para lang="en">JavaScript Callback Method</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerGetItemsCallback()
    {
        if (OnGetItemsAsync != null)
        {
            var items = await OnGetItemsAsync();
            if (_source != null)
            {
                var selectedItems = _source.Where(i => i.Active).ToList();
                if (selectedItems.Count > 0)
                {
                    foreach (var item in items)
                    {
                        if (selectedItems.Find(i => item.Value == i.Value) != null)
                        {
                            item.Active = true;
                        }
                    }
                }
            }
            _source = items;
            StateHasChanged();
        }
    }

    private CheckboxState _selectAllState = CheckboxState.UnChecked;

    private CheckboxState GetState()
    {
        var state = CheckboxState.UnChecked;
        var items = GetItems();
        if (items.Count > 0)
        {
            state = items.All(i => i.Active)
                ? CheckboxState.Checked
                : items.Any(i => i.Active)
                    ? CheckboxState.Indeterminate
                    : CheckboxState.UnChecked;
        }
        return state;
    }

    private bool GetAllState()
    {
        _selectAllState = GetState();
        return _selectAllState == CheckboxState.Checked;
    }

    private Task OnStateChanged(CheckboxState state, bool val)
    {
        foreach (var item in GetItems())
        {
            item.Active = state == CheckboxState.Checked;
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">过滤内容搜索</para>
    /// <para lang="en">Filter Content Search</para>
    /// </summary>
    /// <param name="val"></param>
    private Task OnSearchValueChanged(string? val)
    {
        _searchText = val;
        if (_source != null)
        {
            if (!string.IsNullOrEmpty(_searchText))
            {
                _items = [.. _source.Where(i => i.Text.Contains(_searchText, StringComparison))];
            }
            else
            {
                _items = null;
            }
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private List<SelectedItem> GetItems() => _items ?? _source ?? [];

    /// <summary>
    /// <para lang="zh">重置过滤条件方法</para>
    /// <para lang="en">Reset Filter Conditions Method</para>
    /// </summary>
    public override void Reset()
    {
        _searchText = null;
        if (_source != null)
        {
            foreach (var item in _source)
            {
                item.Active = false;
            }
        }
        _items = null;
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">生成过滤条件方法</para>
    /// <para lang="en">Generate Filter Conditions Method</para>
    /// </summary>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction { FilterLogic = FilterLogic.Or };
        foreach (var item in GetItems().Where(i => i.Active))
        {
            filter.Filters.Add(new FilterKeyValueAction
            {
                FieldKey = FieldKey,
                FieldValue = item.Value,
                FilterAction = FilterAction.Equal
            });
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="filter"></param>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var items = GetItems();
        if (items.Count > 0)
        {
            foreach (var f in filter.Filters)
            {
                var val = f.FieldValue?.ToString();
                var item = items.Find(i => i.Value == val);
                if (item != null)
                {
                    item.Active = true;
                }
            }
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
