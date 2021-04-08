// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table 组件基类
    /// </summary>
    public partial class Table<TItem> : BootstrapComponentBase, IDisposable, ITable where TItem : class, new()
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
        /// 树形数据展开小箭头
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetTreeClassString(TItem item) => CssBuilder.Default("is-tree")
            .AddClass("fa fa-caret-right", CheckTreeChildren(item))
            .AddClass("fa-rotate-90", TryGetTreeNodeByItem(item, out var node) && node.IsExpand)
            .AddClass("fa-spin fa-spinner", IsLoadChildren)
            .Build();

        /// <summary>
        /// 树形数据展开小箭头
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetTreeStyleString(TItem item) => CssBuilder.Default()
            .AddClass($"margin-right: .5rem;")
            .AddClass($"margin-left: {GetIndentSize(item)}px;")
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
        /// 获得/设置 树形数据已展开集合
        /// </summary>
        [NotNull]
        private List<TableTreeNode<TItem>>? TreeRows { get; set; }

        /// <summary>
        /// 获得/设置 是否正在加载子项 默认为 false
        /// </summary>
        private bool IsLoadChildren { get; set; }

        /// <summary>
        /// 获得/设置 树形数据节点展开式回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task<IEnumerable<TItem>>>? OnTreeExpand { get; set; }

        /// <summary>
        /// 获得/设置 缩进大小 默认为 16 单位 px
        /// </summary>
        [Parameter]
        public int IndentSize { get; set; } = 16;

        [NotNull]
        private string? NotSetOnTreeExpandErrorMessage { get; set; }

        private string GetIndentSize(TItem item)
        {
            // 查找递归层次
            var indent = 0;
            if (TryGetTreeNodeByItem(item, out var node))
            {
                while (node.Parent != null)
                {
                    indent += IndentSize;
                    node = node.Parent;
                }
            }
            return indent.ToString();
        }

        /// <summary>
        /// 明细行功能中切换行状态时调用此方法
        /// </summary>
        /// <param name="item"></param>
        protected EventCallback<MouseEventArgs> ExpandDetailRow(TItem item) => EventCallback.Factory.Create<MouseEventArgs>(this, () =>
        {
            DetailRows.Add(item);
            if (ExpandRows.Contains(item))
            {
                ExpandRows.Remove(item);
            }
            else
            {
                ExpandRows.Add(item);
            }
        });

        /// <summary>
        /// 展开收缩树形数据节点方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected Func<Task> ToggleTreeRow(TItem item) => async () =>
        {
            if (OnTreeExpand == null)
            {
                throw new InvalidOperationException(NotSetOnTreeExpandErrorMessage);
            }

            if (IsLoadChildren)
            {
                return;
            }

            if (TryGetTreeNodeByItem(item, out var node))
            {
                node.IsExpand = !node.IsExpand;

                // 无子项时通过回调方法延时加载
                if (node.Children.Count == 0)
                {
                    IsLoadChildren = true;
                    var nodes = await OnTreeExpand(item);
                    IsLoadChildren = false;

                    node.Children.AddRange(nodes.Select(i => new TableTreeNode<TItem>(i)
                    {
                        HasChildren = CheckTreeChildren(i),
                        Parent = node
                    }));
                }
            }
            StateHasChanged();
        };

        private bool TryGetTreeNodeByItem(TItem item, [MaybeNullWhen(false)] out TableTreeNode<TItem> node)
        {
            TableTreeNode<TItem>? n = null;
            foreach (var v in TreeRows)
            {
                if (v.Value == item)
                {
                    n = v;
                    break;
                }

                if (v.Children != null)
                {
                    n = GetTreeNodeByItem(item, v.Children);
                }

                if (n != null)
                {
                    break;
                }
            }
            node = n;
            return n != null;
        }

        private TableTreeNode<TItem>? GetTreeNodeByItem(TItem item, IEnumerable<TableTreeNode<TItem>> nodes)
        {
            TableTreeNode<TItem>? ret = null;
            foreach (var node in nodes)
            {
                if (node.Value == item)
                {
                    ret = node;
                    break;
                }

                if (node.Children.Any())
                {
                    ret = GetTreeNodeByItem(item, node.Children);
                }

                if (ret != null)
                {
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 通过设置的 HasChildren 属性得知是否有子节点用于显示 UI
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CheckTreeChildren(TItem item)
        {
            var invoker = GetPropertyCache.GetOrAdd((typeof(TItem), HasChildrenColumnName), key => LambdaExtensions.GetPropertyValueLambda<TItem, object>(item, key.PropertyName).Compile());
            var v = invoker.Invoke(item);
            var ret = false;
            if (v is bool b)
            {
                ret = b;
            }
            return ret;
        }

        #region Tree 树形数据获取 Items 方法集合
        private IEnumerable<TItem> GetItems() => IsTree ? GetTreeRows() : Items;

        private IEnumerable<TItem> GetTreeRows()
        {
            var ret = new List<TItem>();
            ReloadTreeNodes(ret, TreeRows);
            return ret;
        }

        private void ReloadTreeNodes(List<TItem> items, IEnumerable<TableTreeNode<TItem>> nodes)
        {
            foreach (var node in nodes)
            {
                items.Add(node.Value);

                if (node.IsExpand && node.Children.Any())
                {
                    ReloadTreeNodes(items, node.Children);
                }
            }
        }
        #endregion

        /// <summary>
        /// 明细行集合用于数据懒加载
        /// </summary>
        protected List<TItem> DetailRows { get; set; } = new List<TItem>();

        /// <summary>
        /// 获得/设置 可过滤表格列集合
        /// </summary>
        protected IEnumerable<ITableColumn>? FilterColumns { get; set; }

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
        /// 获得/设置 数据集合，适用于无功能时仅做数据展示使用，高级功能时请使用 <see cref="OnQueryAsync"/> 回调委托
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
        /// 获取/设置 表格 thead 样式 <see cref="TableHeaderStyle"/>，默认为浅色<see cref="TableHeaderStyle.None"/>
        /// </summary>
        [Parameter]
        public TableHeaderStyle HeaderStyle { get; set; } = TableHeaderStyle.None;

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
        /// 获得/设置 是否为树形数据 默认为 false
        /// </summary>
        /// <remarks>通过 <see cref="ChildrenColumnName"/> 参数设置树状数据关联列，是否有子项请使用 <seealso cref="HasChildrenColumnName"/> 树形进行设置</remarks>
        [Parameter]
        public bool IsTree { get; set; }

        /// <summary>
        /// 获得/设置 树形数据模式子项字段 默认为 Children
        /// </summary>
        /// <remarks>通过 <see cref="HasChildrenColumnName"/> 参数判断是否有子项</remarks>
        [Parameter]
        public string ChildrenColumnName { get; set; } = "Children";

        /// <summary>
        /// 获得设置 树形数据模式子项字段是否有子节点属性名称 默认为 HasChildren
        /// </summary>
        [Parameter]
        public string HasChildrenColumnName { get; set; } = "HasChildren";

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

            if (IsTree)
            {
                TreeRows = Items.Select(item => new TableTreeNode<TItem>(item)
                {
                    HasChildren = CheckTreeChildren(item)
                }).ToList();
            }
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
                    await Interop.InvokeVoidAsync(this, TableElement, "bb_table_search", nameof(OnSearch), nameof(OnClearSearch));
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

            if (!firstRender)
            {
                IsRendered = true;
            }

            if (IsRendered)
            {
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

                // 自动化处理 bool 值
                if (val is bool && col.ComponentType != null)
                {
                    builder.OpenComponent(0, col.ComponentType);
                    builder.AddAttribute(1, "Value", val);
                    builder.AddAttribute(2, "IsDisabled", true);
                    builder.CloseComponent();
                    return;
                }
                if (col.Formatter != null)
                {
                    // 格式化回调委托
                    content = await col.Formatter(val);
                }
                else if (!string.IsNullOrEmpty(col.FormatString))
                {
                    // 格式化字符串
                    content = Utility.Format(val, col.FormatString);
                }
                else if (col.PropertyType.IsEnum())
                {
                    content = col.PropertyType.ToDescriptionString(val?.ToString());
                }
                else if (col.PropertyType.IsDateTime())
                {
                    content = Utility.Format(val, CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                else if (val is IEnumerable<object> v)
                {
                    content = string.Join(",", v);
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
                var invoker = GetPropertyCache.GetOrAdd((typeof(TItem), fieldName), key => LambdaExtensions.GetPropertyValueLambda<TItem, object>(item, key.PropertyName).Compile());
                ret = invoker(item);

                if (ret?.GetType().IsEnum ?? false)
                {
                    ret = ret.GetType().ToEnumDisplayName(ret.ToString());
                }
            }
            return ret;
        }

        private static readonly ConcurrentDictionary<(Type Type, string PropertyName), Func<TItem, object?>> GetPropertyCache = new();
        #endregion

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Interop?.Dispose();
                Interop = null;

                AutoRefreshCancelTokenSource?.Cancel();
                AutoRefreshCancelTokenSource?.Dispose();
                AutoRefreshCancelTokenSource = null;
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
