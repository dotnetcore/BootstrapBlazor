// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table 组件基类
    /// </summary>
    public partial class Table<TItem> : BootstrapComponentBase, ITable where TItem : class, new()
    {
        private JSInterop<Table<TItem>>? Interop { get; set; }

        /// <summary>
        /// 获得 Table 组件样式表
        /// </summary>
        protected string? TableClassName => CssBuilder.Default("table-container")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 wrapper 样式表集合
        /// </summary>
        protected string? WrapperClassName => CssBuilder.Default()
            .AddClass("table-bordered", IsBordered)
            .AddClass("table-striped table-hover", IsStriped)
            .AddClass("is-clickable", ClickToSelect || DoubleClickToEdit || OnClickRowCallback != null || OnDoubleClickRowCallback != null)
            .AddClass("table-scroll", !Height.HasValue)
            .AddClass("table-fixed", Height.HasValue)
            .AddClass("table-fixed-column", Columns.Any(c => c.Fixed))
            .AddClass("table-sm", TableSize == TableSize.Compact)
            .AddClass("table-resize", AllowResizing)
            .Build();

        /// <summary>
        /// 获得 Body 内行样式
        /// </summary>
        /// <param name="item"></param>
        /// <param name="css"></param>
        /// <returns></returns>
        protected string? GetRowClassString(TItem item, string? css = null) => CssBuilder.Default(css)
            .AddClass(SetRowClassFormatter?.Invoke(item))
            .AddClass("active", CheckActive(item))
            .AddClass("is-master", DetailRowTemplate != null)
            .AddClass("is-click", ClickToSelect)
            .AddClass("is-dblclick", DoubleClickToEdit)
            .Build();

        /// <summary>
        /// 明细行首小图标单元格样式
        /// </summary>
        protected string? GetDetailBarClassString(TItem item) => CssBuilder.Default("table-cell is-bar")
            .AddClass("is-load", DetailRows.Contains(item))
            .Build();

        /// <summary>
        /// 获得明细行样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetDetailRowClassString(TItem item) => CssBuilder.Default("is-detail")
            .AddClass("show", ExpandRows.Contains(item))
            .Build();

        /// <summary>
        /// 获得明细行小图标样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetDetailCaretClassString(TItem item) => CssBuilder.Default("fa fa-caret-right")
            .AddClass("fa-rotate-90", ExpandRows.Contains(item))
            .Build();

        /// <summary>
        /// 明细行集合用于数据懒加载
        /// </summary>
        protected List<TItem> ExpandRows { get; set; } = new List<TItem>();

        /// <summary>
        /// 明细行功能中切换行状态时调用此方法
        /// </summary>
        /// <param name="item"></param>
        protected EventCallback<MouseEventArgs> ExpandDetailRow(TItem item) => EventCallback.Factory.Create<MouseEventArgs>(this, () =>
        {
            DetailRows.Add(item);
            if (ExpandRows.Contains(item)) ExpandRows.Remove(item);
            else ExpandRows.Add(item);
        });

        /// <summary>
        /// 明细行集合用于数据懒加载
        /// </summary>
        protected List<TItem> DetailRows { get; set; } = new List<TItem>();

        /// <summary>
        /// 获得/设置 可过滤表格列集合
        /// </summary>
        protected IEnumerable<ITableColumn>? FilterColumns { get; set; }

        /// <summary>
        /// 获得 起始行号
        /// </summary>
        protected int StarRowIndex { get; set; }

        /// <summary>
        /// 获得/设置 组件是否渲染完毕 默认 false
        /// </summary>
        public bool IsRendered { get; set; }

        /// <summary>
        /// 获得 表头集合
        /// </summary>
        public List<ITableColumn> Columns { get; } = new List<ITableColumn>(50);

        /// <summary>
        /// 获得/设置 是否使用注入的数据服务
        /// </summary>
        [Parameter]
        public bool UseInjectDataService { get; set; }

        /// <summary>
        /// 获得/设置 明细行模板
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? DetailRowTemplate { get; set; }

        /// <summary>
        /// 获得/设置 TableHeader 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? TableColumns { get; set; }

        /// <summary>
        /// 获得/设置 TableFooter 实例
        /// </summary>
        [Parameter]
        public RenderFragment<IEnumerable<TItem>>? TableFooter { get; set; }

        /// <summary>
        /// 获得/设置 数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

        /// <summary>
        /// 获得/设置 表格组件大小 默认为 Normal 正常模式
        /// </summary>
        [Parameter]
        public TableSize TableSize { get; set; }

        /// <summary>
        /// 获得/设置 是否显示表脚 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; }

        /// <summary>
        /// 获得/设置 是否允许列宽度调整 默认 false 固定表头时此属性生效
        /// </summary>
        [Parameter]
        public bool AllowResizing { get; set; }

        /// <summary>
        /// 获得/设置 是否斑马线样式 默认为 false
        /// </summary>
        [Parameter]
        public bool IsStriped { get; set; }

        /// <summary>
        /// 获得/设置 是否带边框样式 默认为 false
        /// </summary>
        [Parameter]
        public bool IsBordered { get; set; }

        /// <summary>
        /// 获得/设置 是否自动刷新表格 默认为 false
        /// </summary>
        [Parameter]
        public bool IsAutoRefresh { get; set; }

        /// <summary>
        /// 获得/设置 自动刷新时间间隔 默认 2000 毫秒
        /// </summary>
        [Parameter]
        public int AutoRefreshInterval { get; set; } = 2000;

        /// <summary>
        /// 获得/设置 单击行回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnClickRowCallback { get; set; }

        /// <summary>
        /// 获得/设置 双击行回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnDoubleClickRowCallback { get; set; }

        /// <summary>
        /// 获得/设置 是否显示每行的明细行展开图标
        /// </summary>
        [Parameter]
        public Func<TItem, bool>? ShowDetailRow { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            OnInitLocalization();

            // 初始化每页显示数量
            if (IsPagination)
            {
                PageItems = PageItemsSource.FirstOrDefault();
            }

            // 设置 OnSort 回调方法
            OnSortAsync = QueryAsync;

            // 设置 OnFilter 回调方法
            OnFilterAsync = async () =>
            {
                PageIndex = 1;
                await QueryAsync();
            };
        }

        private string? methodName;

        /// <summary>
        /// 获得/设置 是否为第一次 Render
        /// </summary>
        protected bool FirstRender { get; set; } = true;

        private CancellationTokenSource? AutoRefreshCancelTokenSource { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (ShowSearch)
                {
                    // 注册 SeachBox 回调事件
                    Interop = new JSInterop<Table<TItem>>(JSRuntime);
                    await Interop.Invoke(this, TableElement, "bb_table_search", nameof(OnSearch), nameof(OnClearSearch));
                }

                FirstRender = false;
                methodName = Height.HasValue ? "fixTableHeader" : "init";

                ScreenSize = await RetrieveWidth();

                // 初始化列
                if (AutoGenerateColumns)
                {
                    var cols = InternalTableColumn.GetProperties<TItem>(Columns);
                    Columns.Clear();
                    Columns.AddRange(cols);
                }

                ColumnVisibles = Columns.Select(i => new ColumnVisibleItem { FieldName = i.GetFieldName(), Visible = i.Visible }).ToList();

                // set default sortName
                var col = Columns.FirstOrDefault(i => i.Sortable && i.DefaultSort);
                if (col != null)
                {
                    SortName = col.GetFieldName();
                    SortOrder = col.DefaultSortOrder;
                }
                await QueryAsync();
            }

            if (!firstRender) IsRendered = true;

            if (IsRendered)
            {
                if(IsLoading)
                {
                    IsLoading = false;
                    var _ = JSRuntime.InvokeVoidAsync(TableElement, "bb_table_load", "hide");
                }

                // fix: https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I2AYEH
                // PR: https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/818
                if (Columns.Any(col => col.ShowTips) && string.IsNullOrEmpty(methodName))
                {
                    methodName = "tooltip";
                }

                if (!string.IsNullOrEmpty(methodName))
                {
                    await JSRuntime.InvokeVoidAsync(TableElement, "bb_table", methodName);
                    methodName = null;
                }

                if (IsAutoRefresh && AutoRefreshInterval > 500 && AutoRefreshCancelTokenSource == null)
                {
                    AutoRefreshCancelTokenSource = new CancellationTokenSource();

                    // 自动刷新功能
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            while (!(AutoRefreshCancelTokenSource?.IsCancellationRequested ?? true))
                            {
                                await InvokeAsync(QueryAsync);
                                await Task.Delay(AutoRefreshInterval, AutoRefreshCancelTokenSource?.Token ?? new CancellationToken(true));
                            }
                        }
                        catch (TaskCanceledException) { }
                    });
                }
            }
        }

        /// <summary>
        /// 获得 Table 组件客户端宽度
        /// </summary>
        /// <returns></returns>
        protected ValueTask<decimal> RetrieveWidth() => JSRuntime.InvokeAsync<decimal>(TableElement, "bb_table", "width", UseComponentWidth);

        /// <summary>
        /// 检查当前列是否显示方法
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected bool CheckShownWithBreakpoint(ITableColumn col)
        {
            return col.ShownWithBreakPoint switch
            {
                BreakPoint.Small => ScreenSize >= 576,
                BreakPoint.Medium => ScreenSize >= 768,
                BreakPoint.Large => ScreenSize >= 992,
                BreakPoint.ExtraLarge => ScreenSize >= 1200,
                _ => true
            };
        }

        #region 生成 Row 方法
        /// <summary>
        /// 获得 指定单元格数据方法
        /// </summary>
        /// <param name="col"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected RenderFragment GetValue(ITableColumn col, TItem item) => async builder =>
        {
            if (col.Template != null)
            {
                builder.AddContent(0, col.Template.Invoke(item));
            }
            else
            {
                var content = "";
                var val = Table<TItem>.GetItemValue(col.GetFieldName(), item);
                if (col.Formatter != null)
                {
                    // 格式化回调委托
                    content = await col.Formatter(val);
                }
                else if (!string.IsNullOrEmpty(col.FormatString))
                {
                    // 格式化字符串
                    content = val?.Format(col.FormatString, CultureInfo.CurrentUICulture.DateTimeFormat) ?? "";
                }
                else if (col.PropertyType.IsEnum())
                {
                    content = col.PropertyType.ToDescriptionString(val?.ToString());
                }
                else if (col.PropertyType.IsDateTime())
                {
                    content = val?.Format(CultureInfo.CurrentUICulture.DateTimeFormat) ?? "";
                }
                else
                {
                    content = val?.ToString() ?? "";
                }
                builder.AddContent(0, content);
            }
        };

        private static object? GetItemValue(string fieldName, TItem item)
        {
            object? ret = null;
            if (item != null)
            {
                var invoker = GetPropertyCache.GetOrAdd((typeof(TItem), fieldName), key => item.GetPropertyValueLambda<TItem, object>(key.Item2).Compile());
                ret = invoker(item);
            }
            return ret;
        }

        private static readonly ConcurrentDictionary<(Type, string), Func<TItem, object>> GetPropertyCache = new ConcurrentDictionary<(Type, string), Func<TItem, object>>();
        #endregion

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Interop?.Dispose();

                AutoRefreshCancelTokenSource?.Cancel();
                AutoRefreshCancelTokenSource?.Dispose();
                AutoRefreshCancelTokenSource = null;
            }

            base.Dispose(disposing);
        }
    }
}
