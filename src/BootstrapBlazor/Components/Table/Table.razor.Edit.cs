// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
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
    public List<TItem> SelectedRows { get; set; } = new List<TItem>();

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<List<TItem>> SelectedRowsChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否正在查询数据
    /// </summary>
    private bool IsLoading { get; set; }

    /// <summary>
    /// 获得 渲染模式
    /// </summary>
    protected TableRenderMode ActiveRenderMode => RenderMode switch
    {
        TableRenderMode.Auto => ScreenSize < RenderModelResponsiveWidth ? TableRenderMode.CardView : TableRenderMode.Table,
        _ => RenderMode
    };

    /// <summary>
    /// 获得/设置 客户端屏幕宽度
    /// </summary>
    protected decimal ScreenSize { get; set; }

    /// <summary>
    /// 获得/设置 组件渲染模式是否使用组件宽度来判断 默认为 false
    /// </summary>
    [Parameter]
    public bool UseComponentWidth { get; set; }

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
    public decimal RenderModelResponsiveWidth { get; set; } = 768;

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

    private IDataService<TItem> GetDataService()
    {
        var ds = DataService ?? InjectDataService;
        if (ds == null)
        {
            throw new InvalidOperationException(DataServiceInvalidOperationText);
        }
        return ds;
    }

    private async Task<QueryData<TItem>> InternalOnQueryAsync(QueryPageOptions options)
    {
        QueryData<TItem>? ret = null;
        if (OnQueryAsync != null)
        {
            ret = await OnQueryAsync(options);
        }
        else
        {
            var d = DataService ?? InjectDataService;
            if (d != null)
            {
                ret = await d.QueryAsync(options);
            }
        }
        return ret ?? new QueryData<TItem>
        {
            Items = Enumerable.Empty<TItem>(),
            TotalCount = 0,
            IsAdvanceSearch = false,
            IsFiltered = false,
            IsSearch = false,
            IsSorted = false
        };
    }

    private async Task<bool> InternalOnDeleteAsync()
    {
        var ret = false;
        if (OnDeleteAsync != null)
        {
            ret = await OnDeleteAsync(SelectedRows);
        }
        else
        {
            var d = DataService ?? InjectDataService;
            if (d != null)
            {
                ret = await d.DeleteAsync(SelectedRows);
            }
        }
        return ret;
    }

    private async Task<bool> InternalOnSaveAsync(TItem item, ItemChangedType changedType)
    {
        var ret = false;
        if (OnSaveAsync != null)
        {
            ret = await OnSaveAsync(item, changedType);
        }
        else
        {
            var d = DataService ?? InjectDataService;
            if (d != null)
            {
                ret = await d.SaveAsync(item, changedType);
            }
        }
        return ret;
    }

    private async Task InternalOnAddAsync()
    {
        if (OnAddAsync != null)
        {
            EditModel = await OnAddAsync();
        }
        else
        {
            EditModel = new TItem();
            var d = DataService ?? InjectDataService;
            if (d != null)
            {
                await d.AddAsync(EditModel);
            }
        }
    }

    private async Task InternalOnEditAsync()
    {
        if (OnEditAsync != null)
        {
            EditModel = SelectedRows[0];
            await OnEditAsync(EditModel);
        }
        else
        {
            var d = DataService ?? InjectDataService;
            if (d is IEntityFrameworkCoreDataService ef)
            {
                EditModel = SelectedRows[0];
                await ef.EditAsync(EditModel);
            }
            else
            {
                EditModel = IsTracking ? SelectedRows[0] : Utility.Clone(SelectedRows[0]);
            }
        }
    }

    private bool CanDelete => OnDeleteAsync != null || DataService != null || InjectDataService != null;

    private bool CanSave => OnSaveAsync != null || DataService != null || InjectDataService != null;

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

            if (SelectedRows.Contains(val))
            {
                SelectedRows.Remove(val);
            }
            else
            {
                SelectedRows.Add(val);
            }
            await OnSelectedRowsChanged();

            // 更新 设置选中状态
            StateHasChanged();
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
    }

    /// <summary>
    /// 检查当前行是否被选中方法
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    protected virtual bool CheckActive(TItem val) => SelectedRows.Contains(val);

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

    /// <summary>
    /// 查询按钮调用此方法
    /// </summary>
    /// <returns></returns>
    public async Task QueryAsync()
    {
        await InternalToggleLoading(true);
        await QueryData();
        await InternalToggleLoading(false);
        StateHasChanged();
    }

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
            await JSRuntime.InvokeVoidAsync(TableElement, "bb_table_load", state ? "show" : "hide");
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
            await JSRuntime.InvokeVoidAsync(TableElement, "bb_table_load", state ? "show" : "hide");
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
            if (OnQueryAsync == null && DynamicContext != null && typeof(TItem).IsAssignableTo(typeof(IDynamicObject)))
            {
                // 动态数据
                QueryItems = DynamicContext.GetItems().Cast<TItem>();
                TotalCount = QueryItems.Count();
            }
            else
            {
                // 数据集合
                await OnQuery();
            }
        }

        async Task OnQuery()
        {
            QueryData<TItem>? queryData = null;
            var queryOption = new QueryPageOptions()
            {
                IsPage = IsPagination,
                PageIndex = PageIndex,
                PageItems = PageItems,
                SearchText = SearchText,
                SortOrder = SortOrder,
                SortName = SortName,
                SortList = string.IsNullOrEmpty(SortString) ? null : new List<string>(SortString.Split(",", StringSplitOptions.RemoveEmptyEntries)),
                Filters = Filters.Values,
                Searchs = GetSearchs(),
                AdvanceSearchs = GetAdvanceSearchs(),
                CustomerSearchs = GetCustomerSearchs(),
                SearchModel = SearchModel,
                StartIndex = StartIndex
            };

            if (CustomerSearchModel != null)
            {
                queryOption.SearchModel = CustomerSearchModel;
            }

            queryData = await InternalOnQueryAsync(queryOption);

            if (queryData != null)
            {
                RowItemsCache = null;
                Items = null;
                QueryItems = queryData.Items;
                TotalCount = queryData.TotalCount;
                IsAdvanceSearch = queryData.IsAdvanceSearch;

                // 处理选中行逻辑
                ProcessSelectedRows();

                // 分页情况下内部不做处理防止页码错乱
                if (!queryOption.IsPage)
                {
                    ProcessPageData(queryData, queryOption);
                }

                if (IsTree)
                {
                    await ProcessTreeData();
                }
            }

            void ProcessSelectedRows()
            {
                // 判断模型是否有 [Key] Id 等可识别字段尝试重构
                if (HasKeyAttribute)
                {
                    var rows = new List<TItem>();
                    // 更新选中行逻辑
                    foreach (var item in SelectedRows)
                    {
                        var key = Utility.GetKeyValue<TItem, object?>(item);
                        if (key != null)
                        {
                            if (QueryItems.Any(i => Utility.GetKeyValue<TItem, object?>(i)?.ToString() == key.ToString()))
                            {
                                rows.Add(item);
                            }
                        }
                    }
                    SelectedRows = rows;
                }
                else
                {
                    SelectedRows.Clear();
                }
            }

            void ProcessPageData(QueryData<TItem> queryData, QueryPageOptions queryOption)
            {
                var filtered = queryData.IsFiltered;
                var sorted = queryData.IsSorted;
                var searched = queryData.IsSearch;

                // 外部未处理 SearchText 模糊查询
                if (!searched && queryOption.Searchs.Any())
                {
                    QueryItems = QueryItems.Where(queryOption.Searchs.GetFilterFunc<TItem>(FilterLogic.Or));
                    TotalCount = QueryItems.Count();
                }

                // 外部未处理自定义高级搜索 内部进行高级自定义搜索过滤
                if (!IsAdvanceSearch && queryOption.CustomerSearchs.Any())
                {
                    QueryItems = QueryItems.Where(queryOption.CustomerSearchs.GetFilterFunc<TItem>());
                    TotalCount = QueryItems.Count();
                    IsAdvanceSearch = true;
                }

                // 外部未过滤，内部自行过滤
                if (!filtered && queryOption.Filters.Any())
                {
                    QueryItems = QueryItems.Where(queryOption.Filters.GetFilterFunc<TItem>());
                    TotalCount = QueryItems.Count();
                }

                // 外部未处理排序，内部自行排序
                // 先处理列头排序 再处理默认多列排序
                if (!sorted)
                {
                    if (queryOption.SortOrder != SortOrder.Unset && !string.IsNullOrEmpty(queryOption.SortName))
                    {
                        var invoker = Utility.GetSortFunc<TItem>();
                        QueryItems = invoker(QueryItems, queryOption.SortName, queryOption.SortOrder);
                    }
                    else if (queryOption.SortList != null && queryOption.SortList.Any())
                    {
                        var invoker = Utility.GetSortListFunc<TItem>();
                        QueryItems = invoker(QueryItems, queryOption.SortList);
                    }
                }
            }

            async Task ProcessTreeData()
            {
                KeySet.Clear();
                if (HasKeyAttribute)
                {
                    CheckExpandKeys(TreeRows);
                }
                if (KeySet.Count > 0)
                {
                    TreeRows = new List<TableTreeNode<TItem>>();
                    foreach (var item in QueryItems)
                    {
                        var node = new TableTreeNode<TItem>(item)
                        {
                            HasChildren = CheckTreeChildren(item),
                        };
                        node.IsExpand = node.HasChildren && node.Key != null && KeySet.Contains(node.Key);
                        if (node.IsExpand)
                        {
                            await RestoreIsExpand(node);
                        }
                        TreeRows.Add(node);
                    }
                }
                else
                {
                    TreeRows = QueryItems.Select(item => new TableTreeNode<TItem>(item)
                    {
                        HasChildren = CheckTreeChildren(item)
                    }).ToList();
                }
            }
        }
    }

    private HashSet<object> KeySet { get; } = new();

    private void CheckExpandKeys(List<TableTreeNode<TItem>> tableTreeNodes)
    {
        foreach (var node in tableTreeNodes)
        {
            if (node.IsExpand && node.Key != null)
            {
                KeySet.Add(node.Key);
            }
            CheckExpandKeys(node.Children);
        }
    }

    private async Task RestoreIsExpand(TableTreeNode<TItem> parentNode)
    {
        if (OnTreeExpand == null)
        {
            throw new InvalidOperationException(NotSetOnTreeExpandErrorMessage);
        }

        foreach (var item in (await OnTreeExpand(parentNode.Value)))
        {
            var node = new TableTreeNode<TItem>(item)
            {
                HasChildren = CheckTreeChildren(item),
                Parent = parentNode
            };
            node.IsExpand = node.HasChildren && node.Key != null && KeySet.Contains(node.Key);
            if (node.IsExpand)
            {
                await RestoreIsExpand(node);
            }
            parentNode.Children.Add(node);
        }
    }

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

        // 更新行选中状态
        await EditAsync();
    }

    private async Task ClickUpdateButtonCallback()
    {
        var context = new EditContext(EditModel);
        await SaveAsync(context, AddInCell ? ItemChangedType.Add : ItemChangedType.Update);

        // 回调外部自定义方法
        if (OnAfterSaveAsync != null)
        {
            await OnAfterSaveAsync(EditModel);
        }
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
