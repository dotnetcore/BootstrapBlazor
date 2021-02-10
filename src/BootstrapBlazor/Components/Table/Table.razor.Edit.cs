// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
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
        protected bool IsLoading { get; set; }

        /// <summary>
        /// 获得 渲染模式
        /// </summary>
        protected TableRenderModel ActiveRenderModel => RenderModel switch
        {
            TableRenderModel.Auto => ScreenSize < RenderModelResponsiveWidth ? TableRenderModel.CardView : TableRenderModel.Table,
            _ => RenderModel
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
        public TableRenderModel RenderModel { get; set; }

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
        /// 获得/设置 RowButtonTemplate 实例
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

        [NotNull]
        private string? DataServiceInvalidOperationText { get; set; }

        /// <summary>
        /// 获得/设置 注入数据服务
        /// </summary>
        [Inject]
        [NotNull]
        private IEnumerable<IDataService<TItem>>? DataServices { get; set; }

        private IDataService<TItem> GetDataService()
        {
            if (DataServices.Any())
            {
                return DataServices.Last();
            }
            else
            {
                throw new InvalidOperationException(DataServiceInvalidOperationText);
            }
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
            }

            if (OnClickRowCallback != null) await OnClickRowCallback(val);
        };

        private async Task OnSelectedRowsChanged()
        {
            if (SelectedRowsChanged.HasDelegate)
            {
                SelectedRows = SelectedItems;
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
        /// 查询按钮调用此方法
        /// </summary>
        /// <returns></returns>
        public async Task QueryAsync()
        {
            // 通知客户端开启遮罩
            if (!IsAutoRefresh)
            {
                IsLoading = true;
                var _ = JSRuntime.InvokeVoidAsync(TableElement, "bb_table_load", "show");
            }
            await QueryData();
            StateHasChanged();
        }

        /// <summary>
        /// 调用 OnQuery 回调方法获得数据源
        /// </summary>
        protected async Task QueryData()
        {
            // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I29YK1
            // 选中行目前不支持跨页 原因是选中行实例无法在翻页后保持
            SelectedItems.Clear();

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
                Items = queryData.Items;
                TotalCount = queryData.TotalCount;
                IsFiltered = queryData.IsFiltered;
                IsSorted = queryData.IsSorted;
                IsSearch = queryData.IsSearch;

                // 外部未过滤，内部自行过滤
                if (!IsFiltered && Filters.Any())
                {
                    Items = Items.Where(Filters.Values.GetFilterFunc<TItem>());
                    TotalCount = Items.Count();
                }

                // 外部未处理排序，内部自行排序
                if (!IsSorted && SortOrder != SortOrder.Unset && !string.IsNullOrEmpty(SortName))
                {
                    var invoker = SortLambdaCache.GetOrAdd(typeof(TItem), key => Items.GetSortLambda().Compile());
                    Items = invoker(Items, SortName, SortOrder);
                }
            }

            if (!IsRendered && SelectedRows != null)
            {
                SelectedItems.AddRange(Items.Where(i => SelectedRows.Contains(i)));
            }
        }

        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<TItem>, string, SortOrder, IEnumerable<TItem>>> SortLambdaCache = new ConcurrentDictionary<Type, Func<IEnumerable<TItem>, string, SortOrder, IEnumerable<TItem>>>();

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

            if (OnDoubleClickRowCallback != null) await OnDoubleClickRowCallback(item);

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

            return Task.FromResult(true);
        };
    }
}
