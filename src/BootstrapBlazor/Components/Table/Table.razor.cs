// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件基类
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TItem))]
#endif
public partial class Table<TItem> : BootstrapComponentBase, IDisposable, ITable where TItem : class, new()
{
    [NotNull]
    private JSInterop<Table<TItem>>? Interop { get; set; }

    /// <summary>
    /// 获得 Table 组件样式表
    /// </summary>
    private string? ClassName => CssBuilder.Default("table-container")
        .AddClass("table-fixed", IsFixedHeader && !Height.HasValue)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height}px;", IsFixedHeader && Height.HasValue)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 Table 组件样式表
    /// </summary>
    private string? TableClassName => CssBuilder.Default("table")
        .AddClass("table-sm", TableSize == TableSize.Compact)
        .AddClass("table-excel", IsExcel)
        .AddClass("table-bordered", IsBordered)
        .AddClass("table-striped table-hover", IsStriped)
        .Build();

    /// <summary>
    /// 获得 wrapper 样式表集合
    /// </summary>
    protected string? WrapperClassName => CssBuilder.Default()
        .AddClass("table-wrapper", IsBordered)
        .AddClass("is-clickable", ClickToSelect || DoubleClickToEdit || OnClickRowCallback != null || OnDoubleClickRowCallback != null)
        .AddClass("table-scroll", !IsFixedHeader)
        .AddClass("table-fixed", IsFixedHeader)
        .AddClass("table-fixed-column", Columns.Any(c => c.Fixed) || FixedExtendButtonsColumn)
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
        .AddClass("is-master", ShowDetails())
        .AddClass("is-click", ClickToSelect)
        .AddClass("is-dblclick", DoubleClickToEdit)
        .AddClass("is-edit", EditInCell)
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
    protected string? GetTreeClassString(TItem item) => CssBuilder.Default("is-tree fa fa-fw")
        .AddClass(TreeIcon, CheckTreeChildren(item) && !IsLoadChildren)
        .AddClass("fa-rotate-90", TryGetTreeNodeByItem(item, out var node) && node.IsExpand)
        .AddClass("fa-spin fa-spinner", IsLoadChildren)
        .Build();

    /// <summary>
    /// 树形数据展开小箭头
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string? GetTreeStyleString(TItem item) => CssBuilder.Default()
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
    protected string? GetDetailCaretClassString(TItem item) => CssBuilder.Default("fa fa-fw")
        .AddClass(TreeIcon)
        .AddClass("fa-rotate-90", ExpandRows.Contains(item))
        .Build();

    private static string? GetColspan(int? colspan) => (colspan.HasValue && colspan.Value > 1) ? colspan.Value.ToString() : null;

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
    /// 获得/设置 数型结构小箭头图标 默认 fa fa-caret-right
    /// </summary>
    [Parameter]
    public string TreeIcon { get; set; } = "fa-caret-right";

    /// <summary>
    /// 获得/设置 树形数据节点展开式回调委托方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task<IEnumerable<TItem>>>? OnTreeExpand { get; set; }

    /// <summary>
    /// 获得/设置 是否有子节点回调方法 默认为 null 用于未提供 <see cref="HasChildrenColumnName"/> 列名时使用
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? HasChildrenCallback { get; set; }

    /// <summary>
    /// 获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新 默认 false
    /// </summary>
    [Parameter]
    public bool IsTracking { get; set; }

    /// <summary>
    /// 获得/设置 组件工作模式为 Excel 模式 默认 false
    /// </summary>
    [Parameter]
    public bool IsExcel { get; set; }

    /// <summary>
    /// 获得/设置 缩进大小 默认为 16 单位 px
    /// </summary>
    [Parameter]
    public int IndentSize { get; set; } = 16;

    /// <summary>
    /// 获得/设置 是否显示明细行 默认为 null 为空时使用 <see cref="DetailRowTemplate" /> 进行逻辑判断
    /// </summary>
    [Parameter]
    public bool? IsDetails { get; set; }

    /// <summary>
    /// 获得/设置 无数据时是否隐藏表格 Footer 默认为 false 不隐藏 
    /// </summary>
    [Parameter]
    public bool IsHideFooterWhenNoData { get; set; }

    /// <summary>
    /// 获得/设置 每行显示组件数量 默认为 2
    /// </summary>
    [Parameter]
    public int EditDialogItemsPerRow { get; set; } = 2;

    /// <summary>
    /// 获得/设置 设置行内组件布局格式 默认 Inline 布局
    /// </summary>
    [Parameter]
    public RowType EditDialogRowType { get; set; } = RowType.Inline;

    /// <summary>
    /// 获得/设置 设置 <see cref="EditDialogRowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐
    /// </summary>
    [Parameter]
    public Alignment EditDialogLabelAlign { get; set; }

    /// <summary>
    /// 获得/设置 明细行 Row Header 宽度 默认 24
    /// </summary>
    [Parameter]
    public int DetailColumnWidth { get; set; }

    /// <summary>
    /// 获得/设置 显示文字的复选框列宽度 默认 80
    /// </summary>
    [Parameter]
    public int ShowCheckboxTextColumnWidth { get; set; }

    /// <summary>
    /// 获得/设置 复选框宽度 默认 36
    /// </summary>
    [Parameter]
    public int CheckboxColumnWidth { get; set; }

    /// <summary>
    /// 获得/设置 行号列宽度 默认 60
    /// </summary>
    [Parameter]
    public int LineNoColumnWidth { get; set; }

    /// <summary>
    /// 获得/设置 Table 组件渲染完毕回调
    /// </summary>
    [Parameter]
    public Func<Table<TItem>, Task>? OnAfterRenderCallback { get; set; }

    /// <summary>
    /// 获得/设置 双击单元格回调委托
    /// </summary>
    [Parameter]
    public Func<string, TItem, object?, Task>? OnDoubleClickCellCallback { get; set; }

    /// <summary>
    /// 获得/设置 数据滚动模式
    /// </summary>
    [Parameter]
    public ScrollMode ScrollMode { get; set; }

    /// <summary>
    /// 获得/设置 虚拟滚动行高 默认为 39.5
    /// </summary>
    /// <remarks>需要设置 <see cref="ScrollMode"/> 值为 Virtual 时生效</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 39.5f;

    /// <summary>
    /// 获得/设置 滚动时是否显示虚拟行 默认为 true 显示
    /// </summary>
    /// <remarks>需要设置 <see cref="ScrollMode"/> 值为 Virtual 时生效</remarks>
    [Parameter]
    public bool ShowVirtualRowMask { get; set; } = true;

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? Options { get; set; }

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    private bool ShowDetails() => IsDetails == null
        ? DetailRowTemplate != null
        : IsDetails.Value && DetailRowTemplate != null;

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
    protected void ExpandDetailRow(TItem item)
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
    }

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
        var ret = false;
        if (HasChildrenCallback != null)
        {
            ret = HasChildrenCallback(item);
        }
        else
        {
            var v = Utility.GetPropertyValue<TItem, object?>(item, HasChildrenColumnName);
            if (v is bool b)
            {
                ret = b;
            }
        }
        return ret;
    }

    private List<TItem> GetTreeRows()
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

    /// <summary>
    /// 明细行集合用于数据懒加载
    /// </summary>
    protected List<TItem> DetailRows { get; } = new List<TItem>();

    /// <summary>
    /// 获得/设置 可过滤表格列集合
    /// </summary>
    protected IEnumerable<ITableColumn>? FilterColumns { get; set; }

    /// <summary>
    /// 获得 表头集合
    /// </summary>
    public List<ITableColumn> Columns { get; } = new(50);

    /// <summary>
    /// 获得/设置 是否使用注入的数据服务
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，组件内部采用就近原则智能推断是否使用全局注册数据服务")]
    public bool UseInjectDataService { get; set; }

    /// <summary>
    /// 获得/设置 明细行模板 <see cref="IsDetails" />
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
    /// 获得/设置 Table Footer 模板
    /// </summary>
    [Parameter]
    public RenderFragment<IEnumerable<TItem>>? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 数据集合，适用于无功能时仅做数据展示使用，高级功能时请使用 <see cref="OnQueryAsync"/> 回调委托
    /// </summary>
    [Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 数据集合回调方法
    /// </summary>
    [Parameter]
    public EventCallback<IEnumerable<TItem>> ItemsChanged { get; set; }

    /// <summary>
    /// 获得/设置 表格组件大小 默认为 Normal 正常模式
    /// </summary>
    [Parameter]
    public TableSize TableSize { get; set; }

    /// <summary>
    /// 获得/设置 无数据时显示模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 无数据时显示文本 默认取资源文件 英文 NoData 中文 无数据
    /// </summary>
    [Parameter]
    public string? EmptyText { get; set; }

    /// <summary>
    /// 获得/设置 无数据时显示图片路径 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? EmptyImage { get; set; }

    /// <summary>
    /// 获得/设置 是否显示无数据空记录 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowEmpty { get; set; }

    /// <summary>
    /// 获得/设置 是否显示过滤表头 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFilterHeader { get; set; }

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
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
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
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
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
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
    [Parameter]
    public Func<TItem, bool>? ShowDetailRow { get; set; }

    /// <summary>
    /// 获得/设置 是否为树形数据 默认为 false
    /// </summary>
    /// <remarks>是否有子项请使用 <seealso cref="HasChildrenColumnName"/> 树形进行设置，此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
    [Parameter]
    public bool IsTree { get; set; }

    /// <summary>
    /// 获得/设置 树形数据模式子项字段是否有子节点属性名称 默认为 HasChildren 无法提供时请设置 <see cref="HasChildrenCallback"/> 回调方法
    /// </summary>
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
    [Parameter]
    public string HasChildrenColumnName { get; set; } = "HasChildren";

    /// <summary>
    /// 获得/设置 动态数据上下文实例
    /// </summary>
    [Parameter]
    public IDynamicObjectContext? DynamicContext { get; set; }

    /// <summary>
    /// 获得/设置 未设置排序时 tooltip 显示文字 默认点击升序
    /// </summary>
    [Parameter]
    [NotNull]
    public string? UnsetText { get; set; }

    /// <summary>
    /// 获得/设置 升序排序时 tooltip 显示文字 默认点击降序
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SortAscText { get; set; }

    /// <summary>
    /// 获得/设置 降序排序时 tooltip 显示文字 默认取消排序
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SortDescText { get; set; }

    /// <summary>
    /// 获得/设置 列创建时回调委托方法
    /// </summary>
    [Parameter]
    public Func<List<ITableColumn>, Task>? OnColumnCreating { get; set; }

    /// <summary>
    /// 获得/设置 OnAfterRenderCallback 是否已经触发 默认 false
    /// </summary>
    /// <remarks>与 <see cref="OnAfterRenderCallback"/> 回调配合</remarks>
    private bool OnAfterRenderIsTriggered { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnInitLocalization();

        OnInitParameters();

        Interop = new JSInterop<Table<TItem>>(JSRuntime);

        // 设置 OnSort 回调方法
        IntenralOnSortAsync = async (sortName, sortOrder) =>
        {
            // 调用 OnSort 回调方法
            OnSort?.Invoke(sortName, SortOrder);

            // 重新查询
            await QueryAsync();
        };

        // 设置 OnFilter 回调方法
        OnFilterAsync = async () =>
        {
            PageIndex = 1;
            await QueryAsync();
        };

        if (IsTree)
        {
            var rows = Items ?? QueryItems ?? Enumerable.Empty<TItem>();
            TreeRows = rows.Select(item => new TableTreeNode<TItem>(item)
            {
                HasChildren = CheckTreeChildren(item)
            }).ToList();
        }
    }

    private void OnInitParameters()
    {
        if (ShowCheckboxTextColumnWidth == 0)
        {
            ShowCheckboxTextColumnWidth = Options.Value.TableSettings.ShowCheckboxTextColumnWidth;
        }

        if (DetailColumnWidth == 0)
        {
            DetailColumnWidth = Options.Value.TableSettings.DetailColumnWidth;
        }

        if (LineNoColumnWidth == 0)
        {
            LineNoColumnWidth = Options.Value.TableSettings.LineNoColumnWidth;
        }

        if (CheckboxColumnWidth == 0)
        {
            CheckboxColumnWidth = Options.Value.TableSettings.CheckboxColumnWidth;
        }

        if (Options.Value.TableSettings.TableRenderMode != null && RenderMode == TableRenderMode.Auto)
        {
            RenderMode = Options.Value.TableSettings.TableRenderMode.Value;
        }
    }

    /// <summary>
    /// 获得/设置 是否为第一次 Render
    /// </summary>
    protected bool FirstRender { get; set; } = true;

    private CancellationTokenSource? AutoRefreshCancelTokenSource { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 初始化每页显示数量
        PageItemsSource ??= new int[] { 20, 50, 100, 200, 500, 1000 };
        if (IsPagination)
        {
            PageItems = PageItemsSource.FirstOrDefault();
        }

        if (ScrollMode == ScrollMode.Virtual)
        {
            IsFixedHeader = true;
            RenderMode = TableRenderMode.Table;
        }

        RowItemsCache = null;

        if (IsExcel)
        {
            IsStriped = false;
            IsMultipleSelect = true;
            IsTree = false;
            IsDetails = false;
        }

        if (!FirstRender)
        {
            // 动态列模式
            ResetDynamicContext();

            ColumnVisibles = Columns.Select(i => new ColumnVisibleItem { FieldName = i.GetFieldName(), Visible = i.Visible }).ToList();

            // set default sortName
            var col = Columns.FirstOrDefault(i => i.Sortable && i.DefaultSort);
            if (col != null)
            {
                SortName = col.GetFieldName();
                SortOrder = col.DefaultSortOrder;
            }
        }
    }

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
                await Interop.InvokeVoidAsync(this, TableElement, "bb_table_search", nameof(OnSearch), nameof(OnClearSearch));
            }

            FirstRender = false;

            ScreenSize = await JSRuntime.InvokeAsync<decimal>(TableElement, "bb_table_width", UseComponentWidth);

            // 动态列模式
            if (DynamicContext != null && typeof(TItem).IsAssignableTo(typeof(IDynamicObject)))
            {
                AutoGenerateColumns = false;

                var cols = DynamicContext.GetColumns();
                Columns.Clear();
                Columns.AddRange(cols);
            }

            // 初始化列
            if (AutoGenerateColumns)
            {
                var cols = InternalTableColumn.GetProperties<TItem>(Columns);
                Columns.Clear();
                Columns.AddRange(cols);
            }

            if (OnColumnCreating != null)
            {
                await OnColumnCreating(Columns);
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

            // 设置 init 执行客户端脚本
            _init = true;
        }

        if (!OnAfterRenderIsTriggered && OnAfterRenderCallback != null)
        {
            OnAfterRenderIsTriggered = true;
            await OnAfterRenderCallback(this);
        }

        if (Columns.Any(col => col.ShowTips))
        {
            await JSRuntime.InvokeVoidAsync(TableElement, "bb_table_tooltip");
        }

        if (_init)
        {
            _init = false;

            // 此处代码防止快速切换页面导致 Table 未渲染完毕异步加载 Interop 被销毁时导致空引用问题
            if (Interop != null)
            {
                await Interop.InvokeVoidAsync(this, TableElement, "bb_table", "init", new { unset = UnsetText, sortAsc = SortAscText, sortDesc = SortDescText });
            }
        }

        // 增加去重保护 _loop 为 false 时执行
        if (!_loop && IsAutoRefresh && AutoRefreshInterval > 500)
        {
            _loop = true;

            AutoRefreshCancelTokenSource ??= new();

            try
            {
                AutoRefreshInterval = 3000;
                // 自动刷新功能
                await Task.Delay(AutoRefreshInterval, AutoRefreshCancelTokenSource.Token);

                // 不调用 QueryAsync 防止出现 Loading 动画 保持屏幕静止
                await QueryData();
                StateHasChanged();
                _loop = false;
            }
            catch (TaskCanceledException)
            {

            }
        }
    }

    private bool _loop;
    private bool _init;

    /// <summary>
    /// 检查当前列是否显示方法
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected bool CheckShownWithBreakpoint(ITableColumn col) => col.ShownWithBreakPoint switch
    {
        BreakPoint.Small => ScreenSize >= 576,
        BreakPoint.Medium => ScreenSize >= 768,
        BreakPoint.Large => ScreenSize >= 992,
        BreakPoint.ExtraLarge => ScreenSize >= 1200,
        _ => true
    };

    /// <summary>
    /// OnQueryAsync 查询结果数据集合
    /// </summary>
    private IEnumerable<TItem>? QueryItems { get; set; }

    private List<TItem>? RowItemsCache { get; set; }

    private List<TItem> RowItems
    {
        get
        {
            RowItemsCache ??= Items?.ToList() ?? QueryItems?.ToList() ?? new List<TItem>();
            return IsTree ? GetTreeRows() : RowItemsCache;
        }
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
        else if (col.ComponentType == typeof(ColorPicker))
        {
            // 自动化处理 ColorPicker 组件
            var val = GetItemValue(col.GetFieldName(), item);
            var v = val?.ToString() ?? "#000";
            var style = string.IsNullOrEmpty(v) ? null : $"background-color: {v};";
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "is-color");
            builder.AddAttribute(2, "style", style);
            builder.CloseElement();
        }
        else
        {
            var content = "";
            var val = GetItemValue(col.GetFieldName(), item);

            if (col.Lookup == null && val is bool v1)
            {
                // 自动化处理 bool 值
                builder.OpenComponent(0, typeof(Switch));
                builder.AddAttribute(1, "Value", v1);
                builder.AddAttribute(2, "IsDisabled", true);
                builder.CloseComponent();
                return;
            }
            if (col.Lookup != null && val != null)
            {
                // 转化 Lookup 数据源
                var lookupVal = col.Lookup.FirstOrDefault(l => l.Value.Equals(val.ToString(), StringComparison.OrdinalIgnoreCase));
                if (lookupVal != null)
                {
                    content = lookupVal.Text;
                }
            }
            else
            {
                if (col.Formatter != null)
                {
                    // 格式化回调委托
                    content = await col.Formatter(new TableColumnContext<TItem, object?>(item, val));
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
            }
            builder.AddContent(0, content);
        }
    };

    private static object? GetItemValue(string fieldName, TItem item)
    {
        object? ret = null;
        if (item != null)
        {
            if (item is IDynamicObject dynamicObject)
            {
                ret = dynamicObject.GetValue(fieldName);
            }
            else
            {
                ret = Utility.GetPropertyValue<TItem, object?>(item, fieldName);

                if (ret != null)
                {
                    var t = ret.GetType();
                    if (t.IsEnum)
                    {
                        ret = t.ToEnumDisplayName(ret.ToString());
                    }
                }
            }
        }
        return ret;
    }
    #endregion

    private RenderFragment RenderCell(ITableColumn col, TItem item, ItemChangedType changedType) => col.IsEditable(changedType)
        ? (col.EditTemplate == null
            ? builder => builder.CreateComponentByFieldType(this, col, item, false, changedType)
            : col.EditTemplate(item))
        : (col.Template == null
            ? builder => builder.CreateDisplayByFieldType(this, col, item, false)
            : col.Template(item));

    private RenderFragment RenderExcelCell(ITableColumn col, TItem item, ItemChangedType changedType)
    {
        col.PlaceHolder ??= "";

        // 可编辑列未设置模板
        if (col.IsEditable(changedType) && col.EditTemplate == null)
        {
            if (DynamicContext != null)
            {
                SetDynamicEditTemplate();
            }
            else
            {
                SetEditTemplate();
            }
        }
        return RenderCell(col, item, changedType);

        void SetDynamicEditTemplate()
        {
            col.EditTemplate = row => builder =>
            {
                var d = (IDynamicObject)row;
                var onValueChanged = Utility.CreateOnValueChanged<IDynamicObject>(col.PropertyType).Compile();
                if (DynamicContext.OnValueChanged != null)
                {
                    var parameters = col.ComponentParameters?.ToList() ?? new List<KeyValuePair<string, object>>();
                    parameters.Add(new(nameof(ValidateBase<string>.OnValueChanged), onValueChanged.Invoke(d, col, (model, column, val) => DynamicContext.OnValueChanged(model, column, val))));
                    col.ComponentParameters = parameters;
                }
                builder.CreateComponentByFieldType(this, col, row, false, changedType);
            };
        }

        void SetEditTemplate()
        {
            if (CanSave)
            {
                var onValueChanged = Utility.CreateOnValueChanged<TItem>(col.PropertyType).Compile();
                var parameters = col.ComponentParameters?.ToList() ?? new List<KeyValuePair<string, object>>();
                parameters.Add(new(nameof(ValidateBase<string>.OnValueChanged), onValueChanged.Invoke(item, col, (model, column, val) => InternalOnSaveAsync(model, ItemChangedType.Update))));
                col.ComponentParameters = parameters;
            }
        }
    }

    #region Filter
    /// <summary>
    /// 获得 过滤小图标样式
    /// </summary>
    protected string? GetFilterClassString(string fieldName) => CssBuilder.Default("fa fa-fw fa-filter")
        .AddClass("active", Filters.ContainsKey(fieldName))
        .Build();

    /// <summary>
    /// 获得/设置 表头过滤时回调方法
    /// </summary>
    public Func<Task>? OnFilterAsync { get; private set; }

    /// <summary>
    /// 获得 过滤集合
    /// </summary>
    public Dictionary<string, IFilterAction> Filters { get; } = new Dictionary<string, IFilterAction>();

    /// <summary>
    /// 点击 过滤小图标方法
    /// </summary>
    /// <param name="col"></param>
    protected void OnFilterClick(ITableColumn col) => col.Filter?.Show();
    #endregion

    private async ValueTask<ItemsProviderResult<TItem>> LoadItems(ItemsProviderRequest request)
    {
        StartIndex = request.StartIndex;
        if (TotalCount > 0)
        {
            PageItems = Math.Min(request.Count, TotalCount - request.StartIndex);
        }
        await QueryData();
        return new ItemsProviderResult<TItem>(QueryItems ?? Enumerable.Empty<TItem>(), TotalCount);
    }

    private Func<Task> TriggerDoubleClickCell(ITableColumn col, TItem item) => async () =>
    {
        if (OnDoubleClickCellCallback != null)
        {
            var val = GetItemValue(col.GetFieldName(), item);
            await OnDoubleClickCellCallback(col.GetFieldName(), item, val);
        }
    };

    private static string? GetDoubleClickCellClassString(bool trigger) => CssBuilder.Default()
        .AddClass("is-dbcell", trigger)
        .Build();

    private bool IsShowEmpty => ShowEmpty && !RowItems.Any();

    private RenderFragment RenderEmpty() => builder =>
    {
        var index = 0;
        builder.OpenComponent<Empty>(index++);
        builder.AddAttribute(index++, nameof(Empty.Text), EmptyText);
        builder.AddAttribute(index++, nameof(Empty.Image), EmptyImage);
        if (EmptyTemplate != null)
        {
            builder.AddAttribute(index++, nameof(Empty.Template), EmptyTemplate);
        }
        builder.CloseComponent();
    };

    private int GetColumnCount()
    {
        var colspan = ColumnVisibles.Count(col => col.Visible);
        if (IsMultipleSelect)
        {
            colspan++;
        }

        if (ShowLineNo)
        {
            colspan++;
        }

        if (ShowExtendButtons)
        {
            colspan++;
        }

        return colspan;
    }

    #region Dispose
    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            AutoRefreshCancelTokenSource?.Cancel();
            AutoRefreshCancelTokenSource?.Dispose();
            AutoRefreshCancelTokenSource = null;

            Interop?.Dispose();
            Interop = null;
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
    #endregion
}
