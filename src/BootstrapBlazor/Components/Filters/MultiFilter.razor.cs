// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 表格过滤菜单组件
/// </summary>
public partial class MultiFilter
{
    /// <summary>
    /// 获得/设置 搜索栏占位符 默认 nul 使用资源文件中值
    /// </summary>
    [Parameter]
    public string? SearchPlaceHolderText { get; set; }

    /// <summary>
    /// 获得/设置 全选按钮文本 默认 nul 使用资源文件中值
    /// </summary>
    [Parameter]
    public string? SelectAllText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索栏 默认 true
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; } = true;

    /// <summary>
    /// 获得 过滤项集合回调方法 适合动态给定数据源
    /// </summary>
    [Parameter]
    public Func<Task<List<SelectedItem>>>? OnGetItemsAsync { get; set; }

    /// <summary>
    /// 获得/设置 Loading 模板
    /// </summary>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    private string? _searchText;

    private List<SelectedItem>? _source;

    private List<SelectedItem>? _items;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (TableFilter != null)
        {
            TableFilter.ShowMoreButton = false;
        }

        if (Items != null)
        {
            _source = Items.ToList();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SearchPlaceHolderText ??= Localizer["MultiFilterSearchPlaceHolderText"];
        SelectAllText ??= Localizer["MultiFilterSelectAllText"];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        if (OnGetItemsAsync != null)
        {
            await InvokeVoidAsync("init", Id, new { Invoker = Interop, Callback = nameof(TriggerGetItemsCallback) });
        }
    }

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public override void Reset()
    {
        _searchText = string.Empty;
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
    /// 生成过滤条件方法
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = [], FilterLogic = FilterLogic.Or };

        foreach (var item in GetItems().Where(i => i.Active))
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = item.Value,
                FilterAction = FilterAction.Equal
            });
        }
        return filter;
    }

    /// <summary>
    /// Javascript 回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerGetItemsCallback()
    {
        if (OnGetItemsAsync != null)
        {
            _source = await OnGetItemsAsync();
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
    /// 过滤内容搜索
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private Task OnSearchValueChanged(string? val)
    {
        _searchText = val;
        if (_source != null)
        {
            if (!string.IsNullOrEmpty(_searchText))
            {
                _items = _source.Where(i => i.Text.Contains(_searchText)).ToList();
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
}
