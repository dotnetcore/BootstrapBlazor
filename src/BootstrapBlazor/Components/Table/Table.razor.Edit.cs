// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// 获得/设置 编辑弹窗 Title 文字
    /// </summary>
    [NotNull]
    protected string? EditModalTitleString { get; set; }

    /// <summary>
    /// 获得/设置 被选中数据集合
    /// </summary>
    [Parameter]
    public List<TItem> SelectedRows { get; set; } = [];

    /// <summary>
    /// 获得/设置 选中行变化回调方法
    /// </summary>
    [Parameter]
    public EventCallback<List<TItem>> SelectedRowsChanged { get; set; }

    /// <summary>
    /// 获得/设置 新建行位置枚举 默认为 Last 最后
    /// </summary>
    [Parameter]
    public InsertRowMode InsertRowMode { get; set; } = InsertRowMode.Last;

    /// <summary>
    /// 获得/设置 是否正在查询数据
    /// </summary>
    private bool IsLoading { get; set; }

    /// <summary>
    /// 获得 渲染模式
    /// </summary>
    protected TableRenderMode ActiveRenderMode => RenderMode switch
    {
        TableRenderMode.Auto => ScreenSize < RenderModeResponsiveWidth ? TableRenderMode.CardView : TableRenderMode.Table,
        _ => RenderMode
    };

    /// <summary>
    /// 获得/设置 客户端屏幕宽度
    /// </summary>
    protected BreakPoint ScreenSize { get; set; }

    /// <summary>
    /// 获得/设置 组件编辑模式 默认为弹窗编辑行数据 PopupEditForm
    /// </summary>
    [Parameter]
    public EditMode EditMode { get; set; }

    /// <summary>
    /// 获得/设置 组件布局方式 默认为 Auto
    /// </summary>
    [Parameter]
    public TableRenderMode RenderMode { get; set; }

    /// <summary>
    /// 获得/设置 组件布局自适应切换阈值 默认为 768
    /// </summary>
    [Parameter]
    public BreakPoint RenderModeResponsiveWidth { get; set; } = BreakPoint.Medium;

    /// <summary>
    /// 获得/设置 编辑弹框是否 Body 出现滚动条 默认 false
    /// </summary>
    [Parameter]
    public bool ScrollingDialogContent { get; set; }

    /// <summary>
    /// 获得/设置 是否支持键盘 ESC 关闭当前弹窗 默认 true 支持
    /// </summary>
    [Parameter]
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// 获得/设置 行样式格式回调委托
    /// </summary>
    [Parameter]
    public Func<TItem, string?>? SetRowClassFormatter { get; set; }

    /// <summary>
    /// 获得/设置 保存后回调委托方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnAfterSaveAsync { get; set; }

    /// <summary>
    /// 获得/设置 删除后回调委托方法
    /// </summary>
    [Parameter]
    public Func<List<TItem>, Task>? OnAfterDeleteAsync { get; set; }

    /// <summary>
    /// 获得/设置 保存删除后回调委托方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnAfterModifyAsync { get; set; }

    /// <summary>
    /// 获得/设置 编辑数据弹窗 Title
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditModalTitle { get; set; }

    /// <summary>
    /// 获得/设置 新建数据弹窗 Title
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AddModalTitle { get; set; }

    /// <summary>
    /// 获得/设置 EditModel 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public TItem? EditModel { get; set; }

    /// <summary>
    /// 获得/设置 EditTemplate 实例
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? EditTemplate { get; set; }

    /// <summary>
    /// 获得/设置 BeforeRowButtonTemplate 实例 此模板生成的按钮默认放置到按钮前面如需放置前面 请查看 <see cref="RowButtonTemplate" />
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? BeforeRowButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 RowButtonTemplate 实例 此模板生成的按钮默认放置到按钮后面如需放置前面 请查看 <see cref="BeforeRowButtonTemplate" />
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? RowButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 行内功能按钮列头文本 默认为 操作
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ColumnButtonTemplateHeaderText { get; set; }

    /// <summary>
    /// 获得/设置 点击行即选中本行 默认为 false
    /// </summary>
    [Parameter]
    public bool ClickToSelect { get; set; }

    /// <summary>
    /// 获得/设置 单选模式下双击即编辑本行 默认为 false
    /// </summary>
    [Parameter]
    public bool DoubleClickToEdit { get; set; }

    /// <summary>
    /// 获得/设置 是否自动生成列信息 默认为 false
    /// </summary>
    [Parameter]
    public bool AutoGenerateColumns { get; set; }

    /// <summary>
    /// 获得/设置 查询时是否显示正在加载中动画 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowLoading { get; set; }

    [NotNull]
    private string? DataServiceInvalidOperationText { get; set; }

    /// <summary>
    /// 获得/设置 数据服务参数 组件采用就近原则 如果提供了 Items > OnQueryAsync > DataService > 全局注入的数据服务 IDataService
    /// </summary>
    [Parameter]
    public IDataService<TItem>? DataService { get; set; }

    /// <summary>
    /// 获得/设置 注入数据服务
    /// </summary>
    [Inject]
    [NotNull]
    private IDataService<TItem>? InjectDataService { get; set; }

    private async Task<QueryData<TItem>> InternalOnQueryAsync(QueryPageOptions options)
    {
        QueryData<TItem>? ret = null;
        if (_autoQuery)
        {
            if (OnQueryAsync != null)
            {
                ret = await OnQueryAsync(options);
            }
            else
            {
                var d = DataService ?? InjectDataService;
                ret = await d.QueryAsync(options);
            }
        }
        return ret ?? new QueryData<TItem>()
        {
            Items = [],
            TotalCount = 0,
            IsAdvanceSearch = true,
            IsFiltered = true,
            IsSearch = true,
            IsSorted = true
        };
    }

    private async Task<bool> InternalOnDeleteAsync()
    {
        bool ret;
        if (OnDeleteAsync != null)
        {
            ret = await OnDeleteAsync(SelectedRows);
        }
        else
        {
            if (Items != null)
            {
                // always return true if use Items as datasource
                ret = true;
            }
            else
            {
                var d = DataService ?? InjectDataService;
                ret = await d.DeleteAsync(SelectedRows);
            }
        }
        return ret;
    }

    private async Task<bool> InternalOnSaveAsync(TItem item, ItemChangedType changedType)
    {
        bool ret;
        if (OnSaveAsync != null)
        {
            ret = await OnSaveAsync(item, changedType);
        }
        else
        {
            if (Items != null)
            {
                // always return true if use Items as datasource
                ret = true;
            }
            else
            {
                var d = DataService ?? InjectDataService;
                ret = await d.SaveAsync(item, changedType);
            }
        }
        return ret;
    }

    private async Task InternalOnAddAsync()
    {
        SelectedRows.Clear();
        if (OnAddAsync != null)
        {
            EditModel = await OnAddAsync();
        }
        else
        {
            EditModel = new TItem();
            if (Items == null)
            {
                var d = DataService ?? InjectDataService;
                await d.AddAsync(EditModel);
            }
        }
    }

    /// <summary>
    /// 单选模式下选择行时调用此方法
    /// </summary>
    /// <param name="val"></param>
    protected async Task ClickRow(TItem val)
    {
        if (ClickToSelect)
        {
            // 多选模式清空
            if (!IsMultipleSelect)
            {
                SelectedRows.Clear();
            }

            if (SelectedRows.Any(row => Equals(val, row)))
            {
                SelectedRows.RemoveAll(row => Equals(val, row));
            }
            else
            {
                SelectedRows.Add(val);
            }
            await OnSelectedRowsChanged();
        }

        if (OnClickRowCallback != null)
        {
            await OnClickRowCallback(val);
        }
    }

    private async Task OnSelectedRowsChanged()
    {
        if (SelectedRowsChanged.HasDelegate)
        {
            await SelectedRowsChanged.InvokeAsync(SelectedRows);
        }
        else
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 检查当前行是否被选中方法
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    protected virtual bool CheckActive(TItem val) => SelectedRows.Any(row => Equals(val, row));

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected Task OnClickRefreshAsync() => QueryAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected void OnClickCardView()
    {
        var model = RenderMode;
        if (model == TableRenderMode.Auto)
        {
            model = ActiveRenderMode;
        }
        RenderMode = model switch
        {
            TableRenderMode.Table => TableRenderMode.CardView,
            _ => TableRenderMode.Table
        };
        StateHasChanged();
    }

    private async Task QueryAsync(bool shouldRender, int? pageIndex = null)
    {
        if (ScrollMode == ScrollMode.Virtual && VirtualizeElement != null)
        {
            await VirtualizeElement.RefreshDataAsync();
        }
        else
        {
            await InternalToggleLoading(true);
            if (pageIndex.HasValue)
            {
                PageIndex = pageIndex.Value;
            }
            await QueryData();
            await InternalToggleLoading(false);
        }

        if (shouldRender)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 查询按钮调用此方法 参数 pageIndex 默认值 null 保持上次页码 第一页页码为 1
    /// </summary>
    /// <returns></returns>
    public Task QueryAsync(int? pageIndex = null) => QueryAsync(true, pageIndex);

    /// <summary>
    /// 显示/隐藏 Loading 遮罩
    /// </summary>
    /// <param name="state">true 时显示，false 时隐藏</param>
    /// <returns></returns>
    public async ValueTask ToggleLoading(bool state)
    {
        if (ShowLoading)
        {
            IsLoading = state;
            await InvokeVoidAsync("load", Id, state ? "show" : "hide");
        }
    }

    /// <summary>
    /// 显示/隐藏 Loading 遮罩
    /// </summary>
    /// <param name="state">true 时显示，false 时隐藏</param>
    /// <returns></returns>
    protected async ValueTask InternalToggleLoading(bool state)
    {
        if (ShowLoading && !IsLoading)
        {
            await InvokeVoidAsync("load", Id, state ? "show" : "hide");
        }
    }

    /// <summary>
    /// 调用 OnQuery 回调方法获得数据源
    /// </summary>
    protected async Task QueryData()
    {
        // 目前设计使用 Items 参数后不回调 OnQueryAsync 方法
        if (Items == null)
        {
            var queryOption = BuildQueryPageOptions();
            // 设置是否为首次查询
            queryOption.IsFirstQuery = _firstQuery;

            if (OnQueryAsync == null && typeof(TItem).IsAssignableTo(typeof(IDynamicObject)))
            {
                QueryDynamicItems(queryOption, DynamicContext);
            }
            else
            {
                await OnQuery(queryOption);
            }
        }
        else
        {
            ResetSelectedRows(Items);
            RowsCache = null;
        }
        return;

        async Task OnQuery(QueryPageOptions queryOption)
        {
            var queryData = await InternalOnQueryAsync(queryOption);
            PageIndex = queryOption.PageIndex;
            PageItems = queryOption.PageItems;
            TotalCount = queryData.TotalCount;
            PageCount = (int)Math.Ceiling(TotalCount * 1.0 / Math.Max(1, PageItems));
            IsAdvanceSearch = queryData.IsAdvanceSearch;
            QueryItems = queryData.Items ?? [];

            if (!IsKeepSelectedRows)
            {
                // 处理选中行逻辑
                ResetSelectedRows(QueryItems);
            }

            // 分页情况下内部不做处理防止页码错乱
            ProcessData();

            if (IsTree)
            {
                await ProcessTreeData();
            }

            // 更新数据后清除缓存防止新数据不显示
            RowsCache = null;
            return;

            void ProcessData()
            {
                var filtered = queryData.IsFiltered;
                var sorted = queryData.IsSorted;
                var searched = queryData.IsSearch;

                // 外部未处理 SearchText 模糊查询
                if (!searched && queryOption.Searches.Count > 0)
                {
                    QueryItems = QueryItems.Where(queryOption.Searches.GetFilterFunc<TItem>(FilterLogic.Or));
                    TotalCount = QueryItems.Count();
                }

                // 外部未处理自定义高级搜索 内部进行高级自定义搜索过滤
                if (!IsAdvanceSearch && queryOption.CustomerSearches.Count > 0)
                {
                    QueryItems = QueryItems.Where(queryOption.CustomerSearches.GetFilterFunc<TItem>());
                    TotalCount = QueryItems.Count();
                    IsAdvanceSearch = true;
                }

                // 外部未过滤，内部自行过滤
                if (!filtered && queryOption.Filters.Count > 0)
                {
                    QueryItems = QueryItems.Where(queryOption.Filters.GetFilterFunc<TItem>());
                    TotalCount = QueryItems.Count();
                }

                // 外部未处理排序，内部自行排序
                // 先处理列头排序 再处理默认多列排序
                if (!sorted)
                {
                    if (OnSort == null && queryOption.SortOrder != SortOrder.Unset && !string.IsNullOrEmpty(queryOption.SortName))
                    {
                        var invoker = Utility.GetSortFunc<TItem>();
                        QueryItems = invoker(QueryItems, queryOption.SortName, queryOption.SortOrder);
                    }
                    else if (queryOption.SortList.Count > 0)
                    {
                        var invoker = Utility.GetSortListFunc<TItem>();
                        QueryItems = invoker(QueryItems, queryOption.SortList);
                    }
                }
            }

            async Task ProcessTreeData()
            {
                var treeNodes = new List<TableTreeNode<TItem>>();
                if (TreeNodeConverter != null)
                {
                    treeNodes.AddRange(await TreeNodeConverter(QueryItems));
                }

                if (treeNodes.Count > 0)
                {
                    await CheckExpand(treeNodes);
                }

                TreeRows.Clear();
                TreeRows.AddRange(treeNodes);
                return;

                async Task CheckExpand(IEnumerable<TableTreeNode<TItem>> nodes)
                {
                    // 恢复当前节点状态
                    foreach (var node in nodes)
                    {
                        await TreeNodeCache.CheckExpandAsync(node, GetChildrenRowAsync);

                        if (node.Items.Any())
                        {
                            await CheckExpand(node.Items);
                        }
                    }
                }
            }
        }
    }

    private QueryPageOptions BuildQueryPageOptions()
    {
        var queryOption = new QueryPageOptions()
        {
            IsPage = IsPagination,
            PageIndex = PageIndex,
            PageItems = PageItems,
            SearchText = SearchText,
            SortOrder = SortOrder,
            SortName = SortName,
            SearchModel = SearchModel,
            StartIndex = StartIndex,
            IsVirtualScroll = ScrollMode == ScrollMode.Virtual
        };

        queryOption.Filters.AddRange(Filters.Values);
        queryOption.Searches.AddRange(GetSearches());
        queryOption.AdvanceSearches.AddRange(GetAdvanceSearches());
        queryOption.CustomerSearches.AddRange(GetCustomerSearches());
        queryOption.AdvancedSortList.AddRange(GetAdvancedSortList());

        if (!string.IsNullOrEmpty(SortString))
        {
            queryOption.SortList.AddRange(SortString.Split(",", StringSplitOptions.RemoveEmptyEntries));
        }

        if (CustomerSearchModel != null)
        {
            queryOption.SearchModel = CustomerSearchModel;
        }
        return queryOption;
    }

    private void ResetSelectedRows(IEnumerable<TItem> items)
    {
        if (SelectedRows.Count > 0)
        {
            SelectedRows = items.Where(i => SelectedRows.Any(row => Equals(i, row))).ToList();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Equals(TItem? x, TItem? y) => DynamicContext?.EqualityComparer?.Invoke((IDynamicObject?)x, (IDynamicObject?)y) ?? this.Equals<TItem>(x, y);

    private async Task OnClickExtensionButton(TItem item, TableCellButtonArgs args)
    {
        if ((IsMultipleSelect || ClickToSelect) && args.AutoSelectedRowWhenClick)
        {
            SelectedRows.Clear();
            SelectedRows.Add(item);
            StateHasChanged();
        }
        if (args.AutoRenderTableWhenClick)
        {
            await QueryAsync();
        }
    }

    private async Task ClickEditButton(TItem item)
    {
        SelectedRows.Clear();
        SelectedRows.Add(item);
        await OnSelectedRowsChanged();

        // 更新行选中状态
        await EditAsync();
    }

    private bool GetEditButtonDisabledState(TItem item) => DisableExtendEditButtonCallback?.Invoke(item) ?? DisableExtendEditButton;

    private bool GetDeleteButtonDisabledState(TItem item) => DisableExtendDeleteButtonCallback?.Invoke(item) ?? DisableExtendDeleteButton;

    private async Task ClickUpdateButtonCallback()
    {
        var context = new EditContext(EditModel);
        await SaveAsync(context, AddInCell ? ItemChangedType.Add : ItemChangedType.Update);
    }

    /// <summary>
    /// 双击行回调此方法
    /// </summary>
    /// <param name="item"></param>
    protected async Task DoubleClickRow(TItem item)
    {
        if (DoubleClickToEdit)
        {
            await ClickEditButton(item);
        }

        if (OnDoubleClickRowCallback != null)
        {
            await OnDoubleClickRowCallback(item);
        }

        StateHasChanged();
    }

    /// <summary>
    /// 行尾列按钮点击回调此方法
    /// </summary>
    /// <param name="item"></param>
    protected Func<Task<bool>> ClickBeforeDelete(TItem item) => () =>
    {
        SelectedRows.Clear();
        SelectedRows.Add(item);

        StateHasChanged();
        return Task.FromResult(true);
    };
}
