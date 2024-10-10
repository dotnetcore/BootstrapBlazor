﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using System.Reflection;

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// 获得 高级搜索样式
    /// </summary>
    protected string? AdvanceSearchClass => CssBuilder.Default("btn btn-secondary")
        .AddClass("btn-info", IsAdvanceSearch)
        .Build();

    /// <summary>
    /// 获得/设置 是否已经处理高级搜索 默认 false
    /// </summary>
    protected bool IsAdvanceSearch { get; set; }

    /// <summary>
    /// 获得/设置 SearchTemplate 实例
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? SearchTemplate { get; set; }

    /// <summary>
    /// 获得/设置 SearchModel 实例
    /// </summary>
    [Parameter, NotNull]
    public TItem? SearchModel { get; set; }

    /// <summary>
    /// 获得/设置 自定义搜索模型 <see cref="CustomerSearchTemplate"/>
    /// </summary>
    [Parameter]
    public ITableSearchModel? CustomerSearchModel { get; set; }

    /// <summary>
    /// 获得/设置 自定义搜索模型模板 <see cref="CustomerSearchModel"/>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableSearchModel>? CustomerSearchTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框 默认为 false 不显示搜索框
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 是否收缩顶部搜索框 默认为 false 不收缩搜索框 是否显示搜索框请设置 <see cref="SearchMode"/> 值 Top
    /// </summary>
    [Parameter]
    public bool CollapsedTopSearch { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索文本框 默认为 true 显示搜索文本框  <see cref="ShowSearch" />
    /// </summary>
    [Parameter]
    public bool ShowSearchText { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示搜索框提示文本 默认 true
    /// </summary>
    [Parameter]
    public bool ShowSearchTextTooltip { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示清空搜索按钮 默认 true 显示 <see cref="ShowSearch" />
    /// </summary>
    [Parameter]
    public bool ShowResetButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示搜索按钮 默认 true 显示 <see cref="ShowSearch" />
    /// </summary>
    [Parameter]
    public bool ShowSearchButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示高级搜索按钮 默认 true 显示 <see cref="ShowSearch" />
    /// </summary>
    [Parameter]
    public bool ShowAdvancedSearch { get; set; } = true;

    /// <summary>
    /// 获得/设置 搜索关键字 通过列设置的 Searchable 自动生成搜索拉姆达表达式
    /// </summary>
    [Parameter]
    public string? SearchText { get; set; }

    /// <summary>
    /// 获得/设置 搜索栏渲染方式 默认 Popup 弹窗模式
    /// </summary>
    [Parameter]
    public SearchMode SearchMode { get; set; }

    /// <summary>
    /// 获得/设置 每行显示组件数量 默认为 2
    /// </summary>
    [Parameter]
    public int SearchDialogItemsPerRow { get; set; } = 2;

    /// <summary>
    /// 获得/设置 设置行内组件布局格式 默认 Inline 布局
    /// </summary>
    [Parameter]
    public RowType SearchDialogRowType { get; set; } = RowType.Inline;

    /// <summary>
    /// 获得/设置 设置 <see cref="SearchDialogRowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐
    /// </summary>
    [Parameter]
    public Alignment SearchDialogLabelAlign { get; set; }

    /// <summary>
    /// 重置搜索按钮异步回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnResetSearchAsync { get; set; }

    /// <summary>
    /// 重置查询方法
    /// </summary>
    protected async Task ResetSearchClick()
    {
        await ToggleLoading(true);
        if (CustomerSearchModel != null)
        {
            CustomerSearchModel.Reset();
        }
        else if (OnResetSearchAsync != null)
        {
            await OnResetSearchAsync(SearchModel);
        }
        else if (SearchTemplate == null)
        {
            Utility.Reset(SearchModel, CreateSearchModel());
        }

        PageIndex = 1;
        await QueryAsync();
        await ToggleLoading(false);
    }

    /// <summary>
    /// 查询方法
    /// </summary>
    protected async Task SearchClick()
    {
        PageIndex = 1;
        await QueryAsync();
    }

    /// <summary>
    /// 获得/设置 搜索框的大小
    /// </summary>
    [Parameter]
    public Size SearchDialogSize { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    /// 获得/设置 搜索框是否可以拖拽 默认 false 不可以拖拽
    /// </summary>
    [Parameter]
    public bool SearchDialogIsDraggable { get; set; }

    /// <summary>
    /// 获得/设置 搜索框是否显示最大化按钮 默认 true 不显示
    /// </summary>
    [Parameter]
    public bool SearchDialogShowMaximizeButton { get; set; } = true;

    /// <summary>
    /// 高级查询按钮点击时调用此方法
    /// </summary>
    protected async Task ShowSearchDialog()
    {
        if (CustomerSearchModel != null && CustomerSearchTemplate != null)
        {
            await DialogService.ShowSearchDialog(CreateCustomerModelDialog());
        }
        else
        {
            await DialogService.ShowSearchDialog(CreateModelDialog());
        }

        SearchDialogOption<TItem> CreateModelDialog() => new()
        {
            Class = "modal-dialog-table",
            IsScrolling = ScrollingDialogContent,
            Title = SearchModalTitle,
            Model = SearchModel,
            DialogBodyTemplate = SearchTemplate,
            OnResetSearchClick = ResetSearchClick,
            OnSearchClick = SearchClick,
            RowType = SearchDialogRowType,
            ItemsPerRow = SearchDialogItemsPerRow,
            LabelAlign = SearchDialogLabelAlign,
            Size = SearchDialogSize,
            Items = Columns.Where(i => i.GetSearchable()),
            IsDraggable = SearchDialogIsDraggable,
            ShowMaximizeButton = SearchDialogShowMaximizeButton,
            ShowUnsetGroupItemsOnTop = ShowUnsetGroupItemsOnTop
        };

        SearchDialogOption<ITableSearchModel> CreateCustomerModelDialog() => new()
        {
            IsScrolling = ScrollingDialogContent,
            Title = SearchModalTitle,
            Model = CustomerSearchModel,
            DialogBodyTemplate = CustomerSearchTemplate,
            OnResetSearchClick = ResetSearchClick,
            OnSearchClick = SearchClick,
            RowType = SearchDialogRowType,
            ItemsPerRow = SearchDialogItemsPerRow,
            Size = SearchDialogSize,
            LabelAlign = SearchDialogLabelAlign,
            IsDraggable = SearchDialogIsDraggable,
            ShowMaximizeButton = SearchDialogShowMaximizeButton
        };
    }

    /// <summary>
    /// 获得 <see cref="CustomerSearchModel"/> 中过滤条件 <see cref="SearchTemplate"/> 模板中的条件无法获得
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<IFilterAction> GetCustomerSearches()
    {
        var searches = new List<IFilterAction>();
        // 处理自定义 SearchModel 条件
        if (CustomerSearchModel != null)
        {
            searches.AddRange(CustomerSearchModel.GetSearches());
        }
        return searches;
    }

    /// <summary>
    /// 获得 <see cref="SearchModel"/> 中过滤条件
    /// </summary>
    /// <returns></returns>
    protected List<IFilterAction> GetAdvanceSearches()
    {
        var searches = new List<IFilterAction>();
        if (ShowAdvancedSearch && CustomerSearchModel == null)
        {
            var callback = GetAdvancedSearchFilterCallback ?? new Func<PropertyInfo, TItem, List<SearchFilterAction>?>((p, model) =>
            {
                var ret = new List<SearchFilterAction>();
                var v = p.GetValue(model);
                if (v != null && v.ToString() != string.Empty)
                {
                    ret.Add(new SearchFilterAction(p.Name, v, FilterAction.Equal));
                }
                return ret;
            });

            var searchColumns = Columns.Where(i => i.GetSearchable());
            foreach (var property in SearchModel.GetType().GetProperties().Where(i => searchColumns.Any(col => col.GetFieldName() == i.Name)))
            {
                var filters = callback(property, SearchModel);
                if (filters != null && filters.Count != 0)
                {
                    searches.AddRange(filters);
                }
            }
        }
        return searches;
    }

    /// <summary>
    /// 通过列集合中的 <see cref="ITableColumn.Searchable"/> 列与 <see cref="SearchText"/> 拼装 IFilterAction 集合
    /// </summary>
    /// <returns></returns>
    protected List<IFilterAction> GetSearches() => Columns.Where(col => col.GetSearchable()).ToSearches(SearchText);

    private async Task OnSearchKeyUp(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            await SearchClick();
        }
        else if (args.Key == "Escape")
        {
            await ClearSearchClick();
        }
    }

    /// <summary>
    /// 重置搜索按钮调用此方法
    /// </summary>
    protected async Task ClearSearchClick()
    {
        SearchText = null;
        await ResetSearchClick();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerable<ITableColumn> GetSearchColumns() => Columns.Where(c => c.GetSearchable());
}
