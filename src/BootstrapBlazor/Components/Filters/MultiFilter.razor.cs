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

    private string? _searchText;

    private bool checkAll = false;

    private readonly List<MultiFilterItem> _source = [];

    private IEnumerable<MultiFilterItem>? _items;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Items != null)
        {
            _source.AddRange(Items.Select(item => new MultiFilterItem() { Value = item.Value, Text = item.Text }));
        }
        if (TableFilter != null)
        {
            TableFilter.ShowMoreButton = false;
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
    /// 重置过滤条件方法
    /// </summary>
    public override void Reset()
    {
        checkAll = false;
        _searchText = string.Empty;
        foreach (var item in _source)
        {
            item.Checked = false;
        }
        StateHasChanged();
    }

    /// <summary>
    /// 生成过滤条件方法
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = [], FilterLogic = FilterLogic.Or };

        foreach (var item in GetItems().Where(i => i.Checked))
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

    private CheckboxState GetState() => GetItems().All(i => i.Checked)
        ? CheckboxState.Checked
        : GetItems().All(i => !i.Checked) ? CheckboxState.UnChecked : CheckboxState.Indeterminate;

    private Task OnStateChanged(CheckboxState state, bool val)
    {
        checkAll = val;
        if (state == CheckboxState.Checked)
        {
            foreach (var item in _source)
            {
                item.Checked = true;
            }
        }
        else
        {
            foreach (var item in _source)
            {
                item.Checked = false;
            }
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
        if (!string.IsNullOrEmpty(_searchText))
        {
            _items = _source.Where(i => i.Text.Contains(_searchText));
        }
        else
        {
            _items = null;
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private IEnumerable<MultiFilterItem> GetItems() => _items ?? _source;

    class MultiFilterItem
    {
        public bool Checked { get; set; }

        [NotNull]
        public string? Value { get; init; }

        [NotNull]
        public string? Text { get; init; }
    }
}
