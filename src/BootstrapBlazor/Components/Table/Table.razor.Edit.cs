// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 编辑弹窗 Title 文字</para>
    /// <para lang="en">Gets or sets Edit Dialog Title Text</para>
    /// </summary>
    [NotNull]
    protected string? EditModalTitleString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 被选中数据集合</para>
    /// <para lang="en">Gets or sets Selected Rows Collection</para>
    /// </summary>
    [Parameter]
    public List<TItem> SelectedRows { get; set; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 选中行变化回调方法</para>
    /// <para lang="en">Gets or sets Selected Rows Changed Callback</para>
    /// </summary>
    [Parameter]
    public EventCallback<List<TItem>> SelectedRowsChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 新建行位置枚举，默认为最后插入</para>
    /// <para lang="en">Gets or sets Insert Row Mode. Default Last</para>
    /// </summary>
    [Parameter]
    public InsertRowMode InsertRowMode { get; set; } = InsertRowMode.Last;

    /// <summary>
    /// <para lang="zh">获得/设置 是否正在查询数据</para>
    /// <para lang="en">Gets or sets Whether is querying data</para>
    /// </summary>
    private bool IsLoading { get; set; }

    /// <summary>
    /// <para lang="zh">获得 渲染模式</para>
    /// <para lang="en">Get Render Mode</para>
    /// </summary>
    protected TableRenderMode ActiveRenderMode => RenderMode switch
    {
        TableRenderMode.Auto => ScreenSize < RenderModeResponsiveWidth ? TableRenderMode.CardView : TableRenderMode.Table,
        _ => RenderMode
    };

    /// <summary>
    /// <para lang="zh">获得/设置 客户端屏幕宽度</para>
    /// <para lang="en">Gets or sets Client Screen Width</para>
    /// </summary>
    protected BreakPoint ScreenSize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件编辑模式 默认为弹窗编辑行数据 PopupEditForm</para>
    /// <para lang="en">Gets or sets Edit Mode. Default PopupEditForm</para>
    /// </summary>
    [Parameter]
    public EditMode EditMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件布局方式 默认为 Auto</para>
    /// <para lang="en">Gets or sets Render Mode. Default Auto</para>
    /// </summary>
    [Parameter]
    public TableRenderMode RenderMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件布局自适应切换阈值 默认为 768</para>
    /// <para lang="en">Gets or sets Render Mode Responsive Width. Default 768</para>
    /// </summary>
    [Parameter]
    public BreakPoint RenderModeResponsiveWidth { get; set; } = BreakPoint.Medium;

    /// <summary>
    /// <para lang="zh">获得/设置 编辑弹框是否 Body 出现滚动条 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show scrollbar in Edit Dialog Body. Default false</para>
    /// </summary>
    [Parameter]
    public bool ScrollingDialogContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否支持键盘 ESC 关闭当前弹窗 默认 true 支持</para>
    /// <para lang="en">Gets or sets Whether to support ESC key to close current dialog. Default true</para>
    /// </summary>
    [Parameter]
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 行样式格式回调委托</para>
    /// <para lang="en">Gets or sets Row Class Formatter Callback</para>
    /// </summary>
    [Parameter]
    public Func<TItem, string?>? SetRowClassFormatter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消保存后回调委托方法</para>
    /// <para lang="en">Gets or sets After Cancel Save Callback</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnAfterCancelSaveAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存后回调委托方法</para>
    /// <para lang="en">Gets or sets After Save Callback</para>
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnAfterSaveAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除后回调委托方法</para>
    /// <para lang="en">Gets or sets After Delete Callback</para>
    /// </summary>
    [Parameter]
    public Func<List<TItem>, Task>? OnAfterDeleteAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存删除后回调委托方法</para>
    /// <para lang="en">Gets or sets After Modify Callback</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnAfterModifyAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑数据弹窗 Title</para>
    /// <para lang="en">Gets or sets Edit Dialog Title</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EditModalTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 新建数据弹窗 Title</para>
    /// <para lang="en">Gets or sets Add Dialog Title</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AddModalTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 EditModel 实例</para>
    /// <para lang="en">Gets or sets EditModel Instance</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public TItem? EditModel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 EditTemplate 实例</para>
    /// <para lang="en">Gets or sets EditTemplate Instance</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? EditTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 BeforeRowButtonTemplate 实例，此模板生成的按钮默认放置到按钮前面，如需放置后面请查看 <see cref="RowButtonTemplate" /></para>
    /// <para lang="en">Gets or sets BeforeRowButtonTemplate Instance. The buttons generated by this template are placed before the button by default. If you need to place them in front, please check <see cref="RowButtonTemplate" /></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? BeforeRowButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 RowButtonTemplate 实例，此模板生成的按钮默认放置到按钮后面，如需放置前面请查看 <see cref="BeforeRowButtonTemplate" /></para>
    /// <para lang="en">Gets or sets RowButtonTemplate Instance. The buttons generated by this template are placed after the button by default. If you need to place them in front, please check <see cref="BeforeRowButtonTemplate" /></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? RowButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 行内更多按钮 默认 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show inline more button. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowMoreButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 更多按钮颜色 默认 <see cref="Color.Secondary"/></para>
    /// <para lang="en">Gets or sets More Button Color. Default <see cref="Color.Secondary"/></para>
    /// </summary>
    [Parameter]
    public Color MoreButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// <para lang="zh">获得/设置 更多按钮文本 默认 null 读取资源文件设置文本</para>
    /// <para lang="en">Gets or sets More Button Text. Default null (Read from resource file)</para>
    /// </summary>
    [Parameter]
    public string? MoreButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 行内更多按钮下拉框模板 默认 null</para>
    /// <para lang="en">Gets or sets Inline More Button Dropdown Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? MoreButtonDropdownTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 行内功能按钮列头文本 默认为 操作</para>
    /// <para lang="en">Gets or sets Column Button Header Text. Default "Operation"</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ColumnButtonTemplateHeaderText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击行即选中本行 默认为 false</para>
    /// <para lang="en">Gets or sets Click Row to Select. Default false</para>
    /// </summary>
    [Parameter]
    public bool ClickToSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 单选模式下双击即编辑本行 默认为 false</para>
    /// <para lang="en">Gets or sets Double Click Row to Edit in Single Select Mode. Default false</para>
    /// </summary>
    [Parameter]
    public bool DoubleClickToEdit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动生成列信息 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to auto generate columns. Default false</para>
    /// </summary>
    [Parameter]
    public bool AutoGenerateColumns { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 查询时是否显示正在加载中动画 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to show loading animation when querying. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowLoading { get; set; }

    [NotNull]
    private string? DataServiceInvalidOperationText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据服务参数 组件采用就近原则 如果提供了 Items > OnQueryAsync > DataService > 全局注入的数据服务 IDataService</para>
    /// <para lang="en">Gets or sets Data Service. Use nearby principle. Items > OnQueryAsync > DataService > Global Data Service</para>
    /// </summary>
    [Parameter]
    public IDataService<TItem>? DataService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭弹窗确认弹窗。默认为 null 使用全局配置设置值 <see cref="BootstrapBlazorOptions.EditDialogSettings"/></para>
    /// <para lang="en">Gets or sets whether to show the close confirm dialog. Default is null to use global configuration <see cref="BootstrapBlazorOptions.EditDialogSettings"/></para>
    /// </summary>
    [Parameter]
    public bool? ShowConfirmCloseSwal { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭确认弹窗标题</para>
    /// <para lang="en">Gets or sets Close Confirm Dialog Title</para>
    /// </summary>
    [Parameter]
    public string? CloseConfirmTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭确认弹窗内容</para>
    /// <para lang="en">Gets or sets Close Confirm Dialog Content</para>
    /// </summary>
    [Parameter]
    public string? CloseConfirmContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 注入数据服务</para>
    /// <para lang="en">Gets or sets Injected Data Service</para>
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
            EditModel = CreateTItem();
            if (Items == null)
            {
                var d = DataService ?? InjectDataService;
                await d.AddAsync(EditModel);
            }
        }
    }

    /// <summary>
    /// <para lang="zh">获得/设置 新建模型回调方法 默认 null 未设置时使用默认无参构造函数创建</para>
    /// <para lang="en">Gets or sets Create Item Callback. Default null. Use default parameterless constructor if not set</para>
    /// </summary>
    [Parameter]
    public Func<TItem>? CreateItemCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动初始化模型属性 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to automatically initialize model properties. Default value is false</para>
    /// </summary>
    [Parameter]
    public bool IsAutoInitializeModelProperty { get; set; }

    private TItem CreateTItem() => CreateItemCallback?.Invoke() ?? CreateInstance();

    private readonly string ErrorMessage = $"{typeof(TItem)} create instance failed. Please provide {nameof(CreateItemCallback)} create the {typeof(TItem)} instance. {typeof(TItem)} 自动创建实例失败，请通过 {nameof(CreateItemCallback)} 回调方法手动创建实例";

    private TItem CreateInstance()
    {
        TItem? item;
        try
        {
            item = ObjectExtensions.CreateInstance<TItem>(IsAutoInitializeModelProperty);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ErrorMessage, ex);
        }
        return item!;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 新建搜索模型回调方法 默认 null 未设置时先 尝试使用 <see cref="CreateItemCallback"/> 回调，再使用默认无参构造函数创建</para>
    /// <para lang="en">Gets or sets Create Search Model Callback. Default null. Try to use <see cref="CreateItemCallback"/> callback first, then use default parameterless constructor</para>
    /// </summary>
    [Parameter]
    public Func<TItem>? CreateSearchModelCallback { get; set; }

    private TItem CreateSearchModel() => CreateSearchModelCallback?.Invoke() ?? CreateTItem();

    /// <summary>
    /// <para lang="zh">单选模式下选择行时调用此方法</para>
    /// <para lang="en">Method called when a row is selected in single selection mode</para>
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
    /// <para lang="zh">检查当前行是否被选中方法</para>
    /// <para lang="en">Check if current row is selected</para>
    /// </summary>
    /// <param name="val"></param>
    protected virtual bool CheckActive(TItem val) => SelectedRows.Any(row => Equals(val, row));

    /// <summary>
    /// <para lang="zh">刷新按钮回调方法</para>
    /// <para lang="en">Refresh Button Callback</para>
    /// </summary>
    protected Task OnClickRefreshAsync() => QueryAsync();

    /// <summary>
    /// <para lang="zh">点击 CardView 按钮回调方法</para>
    /// <para lang="en">Click CardView Button Callback</para>
    /// </summary>
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
        _viewChanged = true;
        StateHasChanged();
    }

    private async Task QueryAsync(bool shouldRender, int? pageIndex = null, bool triggerByPagination = false)
    {
        if (ScrollMode == ScrollMode.Virtual && _virtualizeElement != null)
        {
            await _virtualizeElement.RefreshDataAsync();
        }
        else
        {
            await InternalToggleLoading(true);
            if (pageIndex.HasValue)
            {
                PageIndex = pageIndex.Value;
            }
            await QueryData(triggerByPagination);
            await InternalToggleLoading(false);
        }

        if (shouldRender)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">查询按钮调用此方法 参数 pageIndex 默认值 null 保持上次页码 第一页页码为 1</para>
    /// <para lang="en">Query Button calls this method. Parameter pageIndex default null, keep last page number. First page number is 1</para>
    /// </summary>
    public Task QueryAsync(int? pageIndex = null) => QueryAsync(true, pageIndex);

    /// <summary>
    /// <para lang="zh">显示/隐藏 Loading 遮罩</para>
    /// <para lang="en">Show/Hide Loading Mask</para>
    /// </summary>
    /// <param name="state"><para lang="zh">true 时显示，false 时隐藏</para><para lang="en">true to display, false to hide</para></param>
    public async ValueTask ToggleLoading(bool state)
    {
        if (ShowLoading)
        {
            IsLoading = state;
            await InvokeVoidAsync("load", Id, state ? "show" : "hide");
        }
    }

    /// <summary>
    /// <para lang="zh">显示/隐藏 Loading 遮罩</para>
    /// <para lang="en">Show/Hide Loading Mask</para>
    /// </summary>
    /// <param name="state"><para lang="zh">true 时显示，false 时隐藏</para><para lang="en">true to display, false to hide</para></param>
    protected async ValueTask InternalToggleLoading(bool state)
    {
        if (ShowLoading && !IsLoading)
        {
            await InvokeVoidAsync("load", Id, state ? "show" : "hide");
        }
    }

    /// <summary>
    /// <para lang="zh">调用 OnQuery 回调方法获得数据源</para>
    /// <para lang="en">Call OnQuery callback to get data source</para>
    /// </summary>
    protected async Task QueryData(bool triggerByPagination = false)
    {
        // Design: Items parameter is used without calling OnQueryAsync method
        if (Items == null)
        {
            var queryOption = BuildQueryPageOptions();
            // Set whether it is the first query
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
            _rowsCache = null;
        }
        return;

        async Task OnQuery(QueryPageOptions queryOption)
        {
            var queryData = await InternalOnQueryAsync(queryOption);
            PageIndex = queryOption.PageIndex;
            _pageItems = queryOption.PageItems;
            TotalCount = queryData.TotalCount;
            PageCount = (int)Math.Ceiling(TotalCount * 1.0 / Math.Max(1, _pageItems));
            IsAdvanceSearch = queryData.IsAdvanceSearch;
            QueryItems = queryData.Items ?? [];

            if (!IsKeepSelectedRows)
            {
                // Process selected row logic
                ResetSelectedRows(QueryItems);
            }

            // Do not process internally in pagination case to prevent page number disorder
            ProcessData();

            if (IsTree)
            {
                await ProcessTreeData();
            }

            // Clear cache after updating data to prevent new data from not showing
            _rowsCache = null;
            return;

            void ProcessData()
            {
                var filtered = queryData.IsFiltered;
                var sorted = queryData.IsSorted;
                var searched = queryData.IsSearch;

                // External not handled SearchText fuzzy query
                if (!searched && queryOption.Searches.Count > 0)
                {
                    QueryItems = QueryItems.Where(queryOption.Searches.GetFilterFunc<TItem>(FilterLogic.Or));
                    TotalCount = QueryItems.Count();
                }

                // External not handled custom advanced search, internal advanced custom search filtering
                if (!IsAdvanceSearch && queryOption.CustomerSearches.Count > 0)
                {
                    QueryItems = QueryItems.Where(queryOption.CustomerSearches.GetFilterFunc<TItem>());
                    TotalCount = QueryItems.Count();
                    IsAdvanceSearch = true;
                }

                // External not filtered, internal filtering
                if (!filtered && queryOption.Filters.Count > 0)
                {
                    QueryItems = QueryItems.Where(queryOption.Filters.GetFilterFunc<TItem>());
                    TotalCount = QueryItems.Count();
                }

                // External not handled sorting, internal sorting
                // Process column header sort first, then default multi-column sort
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
            PageItems = _pageItems,
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
            var selectedRows = items.Where(i => SelectedRows.Any(row => Equals(i, row))).ToList();
            if (!selectedRows.SequenceEqual(SelectedRows))
            {
                SelectedRows = selectedRows;
                if (SelectedRowsChanged.HasDelegate)
                {
                    _ = SelectedRowsChanged.InvokeAsync(selectedRows);
                }
            }
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
        // 验证 InCell 模式下的表单
        var valid = await _inCellValidateForm.ValidateAsync();
        if (!valid)
        {
            return;
        }

        var context = new EditContext(EditModel);
        await SaveAsync(context, AddInCell ? ItemChangedType.Add : ItemChangedType.Update);
    }

    /// <summary>
    /// <para lang="zh">双击行回调此方法</para>
    /// <para lang="en">Double click row callback method</para>
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
    /// <para lang="zh">行尾列按钮点击回调此方法</para>
    /// <para lang="en">Row button click callback method</para>
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
