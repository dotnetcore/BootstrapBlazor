// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 按钮列样式表集合
        /// </summary>
        /// <returns></returns>
        protected string? ExtendButtonsColumnClass => CssBuilder.Default("table-th-button")
            .AddClass("fixed fixed-right", FixedExtendButtonsColumn)
            .Build();

        /// <summary>
        /// 获得 按钮列样式表集合
        /// </summary>
        /// <returns></returns>
        protected string? FixedExtendButtonsHeaderStyleString => CssBuilder.Default()
            .AddClass("right: 0;", FixedExtendButtonsColumn)
            .Build();

        /// <summary>
        /// 获得/设置 编辑弹窗 Title 文字
        /// </summary>
        [NotNull]
        protected string? EditModalTitleString { get; set; }

        /// <summary>
        /// 获得/设置 被选中数据集合
        /// </summary>
        /// <value></value>
        protected List<TItem> SelectedItems { get; set; } = new List<TItem>();

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
        /// 获得/设置 被选中的数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<TItem>? SelectedRows { get; set; }

        /// <summary>
        /// 获得/设置 被选中的数据集合回调委托
        /// </summary>
        [Parameter]
        public EventCallback<IEnumerable<TItem>> SelectedRowsChanged { get; set; }

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
        /// 获得/设置 数据服务
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

        /// <summary>
        /// 单选模式下选择行时调用此方法
        /// </summary>
        /// <param name="val"></param>
        protected Func<Task> ClickRow(TItem val) => async () =>
        {
            if (ClickToSelect)
            {
                // 多选模式清空
                if (!IsMultipleSelect)
                {
                    SelectedItems.Clear();
                }

                if (SelectedItems.Contains(val))
                {
                    SelectedItems.Remove(val);
                }
                else
                {
                    SelectedItems.Add(val);
                }

                await OnSelectedRowsChanged();

                // 更新 设置选中状态
                StateHasChanged();
            }

            if (OnClickRowCallback != null)
            {
                await OnClickRowCallback(val);
            }
        };

        private async Task OnSelectedRowsChanged()
        {
            var rows = new List<TItem>(SelectedItems);
            SelectedRows = rows;
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
        protected virtual bool CheckActive(TItem val) => SelectedItems.Contains(val);

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
            SelectedItems.Clear();
            RowItemsCache = null;
            Items = null;

            if (OnQueryAsync == null && DynamicContext != null && typeof(TItem).IsAssignableTo(typeof(IDynamicObject)))
            {
                QueryItems = DynamicContext.GetItems().Cast<TItem>();
            }
            else
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
                    Filters = Filters.Values,
                    Searchs = GetSearchs(),
                    SearchModel = SearchModel
                };
                if (OnQueryAsync != null)
                {
                    queryData = await OnQueryAsync(queryOption);
                }
                else if (UseInjectDataService)
                {
                    queryData = await GetDataService().QueryAsync(queryOption);
                }

                if (queryData != null)
                {
                    QueryItems = queryData.Items;
                    if (IsTree)
                    {
                        KeySet.Clear();
                        if (TableTreeNode<TItem>.HasKey)
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
                    TotalCount = queryData.TotalCount;
                    IsFiltered = queryData.IsFiltered;
                    IsSorted = queryData.IsSorted;
                    IsSearch = queryData.IsSearch;

                    // 外部未过滤，内部自行过滤
                    if (!IsFiltered && Filters.Any())
                    {
                        QueryItems = QueryItems.Where(Filters.Values.GetFilterFunc<TItem>());
                        TotalCount = QueryItems.Count();
                    }

                    // 外部未处理排序，内部自行排序
                    if (!IsSorted && SortOrder != SortOrder.Unset && !string.IsNullOrEmpty(SortName))
                    {
                        var invoker = SortLambdaCache.GetOrAdd(typeof(TItem), key => LambdaExtensions.GetSortLambda<TItem>().Compile());
                        QueryItems = invoker(QueryItems, SortName, SortOrder);
                    }
                }
            }
            if (SelectedRows != null)
            {
                SelectedItems.AddRange(RowItems.Where(i => SelectedRows.Contains(i)));
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

        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<TItem>, string, SortOrder, IEnumerable<TItem>>> SortLambdaCache = new();

        private async Task ClickEditButton(TItem item)
        {
            SelectedItems.Clear();
            SelectedItems.Add(item);

            // 更新行选中状态
            await EditAsync();
        }

        /// <summary>
        /// 行尾列编辑按钮点击回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected EventCallback<MouseEventArgs> ClickEditButtonCallback(TItem item) => EventCallback.Factory.Create<MouseEventArgs>(this, () => ClickEditButton(item));

        private EventCallback<MouseEventArgs> ClickUpdateButtonCallback() => EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
        {
            var context = new EditContext(EditModel);
            await SaveAsync(context);

            // 回调外部自定义方法
            if (OnAfterSaveAsync != null)
            {
                await OnAfterSaveAsync(EditModel);
            }
        });

        /// <summary>
        /// 双击行回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected Func<Task> DoubleClickRow(TItem item) => async () =>
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
        };

        /// <summary>
        /// 行尾列按钮点击回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected Func<Task<bool>> ClickBeforeDelete(TItem item) => () =>
        {
            SelectedItems.Clear();
            SelectedItems.Add(item);

            StateHasChanged();
            return Task.FromResult(true);
        };
    }
}
