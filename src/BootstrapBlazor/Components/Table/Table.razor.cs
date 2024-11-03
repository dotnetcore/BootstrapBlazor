﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Reflection;
using System.Text.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件基类
/// </summary>
[CascadingTypeParameter(nameof(TItem))]
public partial class Table<TItem> : ITable, IModelEqualityComparer<TItem> where TItem : class
{
    /// <summary>
    /// 获得/设置 Loading 模板
    /// </summary>
    [Parameter]
    public RenderFragment? LoadingTemplate { get; set; }

    /// <summary>
    /// 获得/设置 列工具栏图标 fa-solid fa-gear
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ColumnToolboxIcon { get; set; }

    /// <summary>
    /// 获得/设置 默认固定列宽度 默认 200 单位 px
    /// </summary>
    [Parameter]
    public int DefaultFixedColumnWidth { get; set; } = 200;

    /// <summary>
    /// 获得/设置 内置虚拟化组件实例
    /// </summary>
    protected Virtualize<TItem>? VirtualizeElement { get; set; }

    /// <summary>
    /// 获得 Table 组件样式表
    /// </summary>
    private string? ClassName => CssBuilder.Default("table-container")
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
        .AddClass("table-layout-fixed", IsFixedHeader)
        .AddClass("table-draggable", AllowDragColumn)
        .Build();

    /// <summary>
    /// 获得 wrapper 样式表集合
    /// </summary>
    protected string? WrapperClassName => CssBuilder.Default()
        .AddClass("table-shim", ActiveRenderMode == TableRenderMode.Table)
        .AddClass("table-card scroll", ActiveRenderMode == TableRenderMode.CardView)
        .AddClass("table-wrapper", IsBordered)
        .AddClass("is-clickable", ClickToSelect || DoubleClickToEdit || OnClickRowCallback != null || OnDoubleClickRowCallback != null)
        .AddClass("table-scroll scroll", !IsFixedHeader || FixedColumn)
        .AddClass("table-fixed", IsFixedHeader)
        .AddClass("table-fixed-column", FixedColumn)
        .AddClass("table-resize", AllowResizing)
        .AddClass("table-fixed-body", RenderMode == TableRenderMode.CardView && IsFixedHeader)
        .AddClass("table-striped table-hover", ActiveRenderMode == TableRenderMode.CardView && IsStriped)
        .Build();

    private string? FooterClassString => CssBuilder.Default("table-footer")
        .AddClass("table-footer-fixed", IsFixedFooter)
        .Build();

    private bool FixedColumn => FixedExtendButtonsColumn || FixedMultipleColumn || FixedDetailRowHeaderColumn || FixedLineNoColumn || Columns.Any(c => c.Fixed);

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

    private string? CopyColumnButtonIconString => CssBuilder.Default("col-copy")
        .AddClass(CopyColumnButtonIcon)
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
    protected string? GetDetailCaretClassString(TItem item) => CssBuilder.Default("node-icon")
        .AddClass(TreeIcon, !ExpandRows.Contains(item))
        .AddClass(TreeExpandIcon, ExpandRows.Contains(item))
        .Build();

    private string? LineCellClassString => CssBuilder.Default("table-cell")
        .AddClass(LineNoColumnAlignment.ToDescriptionString())
        .Build();

    private string? ExtendButtonsCellClassString => CssBuilder.Default("table-cell")
        .AddClass(ExtendButtonColumnAlignment.ToDescriptionString())
        .Build();

    private string GetSortTooltip(ITableColumn col) => SortName != col.GetFieldName()
        ? UnsetText
        : SortOrder switch
        {
            SortOrder.Asc => SortAscText,
            SortOrder.Desc => SortDescText,
            _ => UnsetText
        };

    private static string GetHeaderTooltipText(string? headerTooltip, string displayName) => headerTooltip ?? displayName;

    private static string? GetColSpan(int colSpan) => colSpan > 1 ? colSpan.ToString() : null;

    private bool IsShowFooter => ShowFooter && (Rows.Count > 0 || !IsHideFooterWhenNoData);

    private int PageStartIndex => Rows.Count > 0 ? (PageIndex - 1) * _pageItems + 1 : 0;

    private string PageInfoLabelString => Localizer[nameof(PageInfoText), PageStartIndex, (PageIndex - 1) * _pageItems + Rows.Count, TotalCount];

    private static string? GetColWidthString(int? width) => width.HasValue ? $"width: {width.Value}px;" : null;

    /// <summary>
    /// 获得/设置 滚动条宽度 默认 null 未设置使用 <see cref="ScrollOptions"/> 配置类中的 <see cref="ScrollOptions.ScrollWidth"/>
    /// </summary>
    [Parameter]
    public int? ScrollWidth { get; set; }

    /// <summary>
    /// 获得/设置 滚动条 hover 状态下宽度 默认 null 未设置使用 <see cref="ScrollOptions"/> 配置类中的 <see cref="ScrollOptions.ScrollHoverWidth"/>
    /// </summary>
    [Parameter]
    public int? ScrollHoverWidth { get; set; }

    /// <summary>
    /// 获得/设置 列调整提示前缀文字 默认 null 未设置使用资源文件中文字
    /// </summary>
    [Parameter]
    public string? ColumnWidthTooltipPrefix { get; set; }

    /// <summary>
    /// 获得/设置 是否显示列宽提示信息，默认 false 显示
    /// </summary>
    [Parameter]
    public bool ShowColumnWidthTooltip { get; set; }

    private string ScrollWidthString => $"width: {ActualScrollWidth}px;";

    private string ScrollStyleString => $"--bb-scroll-width: {ActualScrollWidth}px; --bb-scroll-hover-width: {ActualScrollHoverWidth}px;";

    private int ActualScrollWidth => ScrollWidth ?? Options.CurrentValue.ScrollOptions.ScrollWidth;

    private int ActualScrollHoverWidth => ScrollHoverWidth ?? Options.CurrentValue.ScrollOptions.ScrollHoverWidth;

    /// <summary>
    /// 获得/设置 Table 高度 默认为 null
    /// </summary>
    /// <remarks>开启固定表头功能时生效 <see cref="IsFixedHeader"/></remarks>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// 获得/设置 固定表头 默认 false
    /// </summary>
    [Parameter]
    public bool IsFixedHeader { get; set; }

    /// <summary>
    /// 获得/设置 固定 Footer 默认 false
    /// </summary>
    [Parameter]
    public bool IsFixedFooter { get; set; }

    /// <summary>
    /// 获得/设置 多表头模板
    /// </summary>
    [Parameter]
    public RenderFragment? MultiHeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 列拷贝 Tooltip 文字
    /// </summary>
    [Parameter]
    public string? CopyColumnTooltipText { get; set; }

    /// <summary>
    /// 获得/设置 列拷贝完毕后 Tooltip 文字
    /// </summary>
    [Parameter]
    public string? CopyColumnCopiedTooltipText { get; set; }

    /// <summary>
    /// 获得/设置 CopyColumn Tooltip 默认 true
    /// </summary>
    [Parameter]
    public bool ShowCopyColumnTooltip { get; set; } = true;

    /// <summary>
    /// 明细行集合用于数据懒加载
    /// </summary>
    protected List<TItem> ExpandRows { get; } = [];

    /// <summary>
    /// 获得/设置 组件工作模式为 Excel 模式 默认 false
    /// </summary>
    [Parameter]
    public bool IsExcel { get; set; }

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
    /// 获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置
    /// </summary>
    [Parameter]
    public bool? DisableAutoSubmitFormByEnter { get; set; }

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
    /// 获得/设置 行号内容位置
    /// </summary>
    [Parameter]
    public Alignment LineNoColumnAlignment { get; set; }

    /// <summary>
    /// 获得/设置 呈现每行之前的回调
    /// </summary>
    [Parameter]
    public Action<TItem>? OnBeforeRenderRow { get; set; }

    /// <summary>
    /// 获得/设置 Table 组件渲染完毕回调
    /// </summary>
    [Parameter]
    public Func<Table<TItem>, bool, Task>? OnAfterRenderCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否自动将选中行滚动到可视区域 默认 false
    /// </summary>
    [Parameter]
    public bool AutoScrollLastSelectedRowToView { get; set; }

    /// <summary>
    /// 获得/设置 选中行滚动到可视区域对齐方式 默认 ScrollToViewAlign.Center
    /// </summary>
    [Parameter]
    public ScrollToViewAlign AutoScrollVerticalAlign { get; set; } = ScrollToViewAlign.Center;

    /// <summary>
    /// 获得/设置 双击单元格回调委托
    /// </summary>
    [Parameter]
    public Func<string, TItem, object?, Task>? OnDoubleClickCellCallback { get; set; }

    /// <summary>
    /// 获得/设置 展开收起明细行回调方法 第二个参数 true 时表示展开 false 时表示收起
    /// </summary>
    [Parameter]
    public Func<TItem, bool, Task>? OnToggleDetailRowCallback { get; set; }

    /// <summary>
    /// 获得/设置 工具栏下拉框按钮是否 IsPopover 默认 false
    /// </summary>
    [Parameter]
    public bool IsPopoverToolbarDropdownButton { get; set; }

    /// <summary>
    /// 获得/设置 数据滚动模式
    /// </summary>
    [Parameter]
    public ScrollMode ScrollMode { get; set; }

    /// <summary>
    /// 获得/设置 虚拟滚动行高 默认为 38
    /// </summary>
    /// <remarks>需要设置 <see cref="ScrollMode"/> 值为 Virtual 时生效</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 38f;

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    /// <summary>
    /// 获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新 默认 false
    /// </summary>
    [Parameter]
    public bool IsTracking { get; set; }

    private string ToggleDropdownString => IsPopoverToolbarDropdownButton ? "bb.dropdown" : "dropdown";

    [Inject]
    [NotNull]
    private ILookupService? LookupService { get; set; }

    private bool _breakPointChanged;

    private bool _viewChanged;

    private List<ColumnWidth> _clientColumnWidths = [];

    private async Task OnBreakPointChanged(BreakPoint size)
    {
        if (size != ScreenSize)
        {
            ScreenSize = size;
            _breakPointChanged = true;
            await InvokeAsync(StateHasChanged);
        }
    }

    private bool ShowDetails() => IsDetails == null
        ? DetailRowTemplate != null
        : IsDetails.Value && DetailRowTemplate != null;

    /// <summary>
    /// 获得/设置 明细行手风琴效果 默认 false
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// 获得/设置 列最小宽度 默认 null 未设置 可通过 <see cref="TableSettings.ColumnMinWidth"/> 统一设置
    /// </summary>
    [Parameter]
    public int? ColumnMinWidth { get; set; }

    /// <summary>
    /// 明细行功能中切换行状态时调用此方法
    /// </summary>
    /// <param name="item"></param>
    public async Task ExpandDetailRow(TItem item)
    {
        // 展开明细行回调方法
        if (OnToggleDetailRowCallback != null)
        {
            await OnToggleDetailRowCallback(item, !ExpandRows.Contains(item));
        }
        if (!DetailRows.Contains(item))
        {
            DetailRows.Add(item);
        }
        if (!ExpandRows.Remove(item))
        {
            if (IsAccordion)
            {
                ExpandRows.Clear();
            }

            ExpandRows.Add(item);
        }
    }

    /// <summary>
    /// 明细行集合用于数据懒加载
    /// </summary>
    protected List<TItem> DetailRows { get; } = [];

    /// <summary>
    /// 获得 表头集合
    /// </summary>
    public List<ITableColumn> Columns { get; } = new(50);

    /// <summary>
    /// 获得/设置 明细行模板 <see cref="IsDetails" />
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? DetailRowTemplate { get; set; }

    /// <summary>
    /// 获得/设置 行模板
    /// </summary>
    [Parameter]
    public RenderFragment<TableRowContext<TItem>>? RowTemplate { get; set; }

    /// <summary>
    /// 获得/设置 行内容模板
    /// </summary>
    [Parameter]
    public RenderFragment<TableRowContext<TItem>>? RowContentTemplate { get; set; }

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
    /// 获得/设置 数据集合，适用于无功能仅做数据展示使用，高级功能时请使用 <see cref="OnQueryAsync"/> 回调委托
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
    /// 获得/设置 是否显示过滤表头 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowMultiFilterHeader { get; set; }

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
    /// 获得/设置 是否表头允许折行 默认 false 不折行 此设置为 true 时覆盖 <see cref="ITableColumn.HeaderTextWrap"/> 参数值
    /// </summary>
    [Parameter]
    public bool HeaderTextWrap { get; set; }

    /// <summary>
    /// 获得/设置 是否斑马线样式 默认为 false
    /// </summary>
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
    [Parameter]
    public bool IsStriped { get; set; }

    /// <summary>
    /// 获得/设置 首次加载时是否自动查询数据 默认 true <see cref="Items"/> 模式下此参数不起作用
    /// </summary>
    [Parameter]
    public bool IsAutoQueryFirstRender { get; set; } = true;

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
    /// 获得/设置 自定义列排序规则 默认 null 未设置 使用内部排序机制 1 2 3 0 -3 -2 -1 顺序
    /// </summary>
    /// <remarks>如果设置 <see cref="AllowDragColumn"/> 并且设置 <see cref="ClientTableName"/> 开启客户端持久化后本回调不生效</remarks>
    [Parameter]
    public Func<IEnumerable<ITableColumn>, IEnumerable<ITableColumn>>? ColumnOrderCallback { get; set; }

    /// <summary>
    /// 获得/设置 数据主键标识标签 默认为 <see cref="KeyAttribute"/><code><br /></code>用于判断数据主键标签，如果模型未设置主键时可使用 <see cref="ModelEqualityComparer"/> 参数自定义判断 <code><br /></code>数据模型支持联合主键
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// 获得/设置 比较数据是否相同回调方法 默认为 null
    /// <para>提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</para>
    /// </summary>
    [Parameter]
    public Func<TItem, TItem, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// 获得/设置 获得高级搜索条件回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<PropertyInfo, TItem, List<SearchFilterAction>?>? GetAdvancedSearchFilterCallback { get; set; }

    /// <summary>
    /// 获得/设置 客户端表格名称 默认 null 用于客户端列宽与列顺序持久化功能
    /// </summary>
    [Parameter]
    public string? ClientTableName { get; set; }

    /// <summary>
    /// 获得/设置 左对齐显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AlignLeftText { get; set; }

    /// <summary>
    /// 获得/设置左对齐提示信息文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AlignLeftTooltipText { get; set; }

    /// <summary>
    /// 获得/设置 居中对齐显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AlignCenterText { get; set; }

    /// <summary>
    /// 获得/设置 居中对齐提示信息文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AlignCenterTooltipText { get; set; }

    /// <summary>
    /// 获得/设置 右对齐显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AlignRightText { get; set; }

    /// <summary>
    /// 获得/设置 右对齐提示信息文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AlignRightTooltipText { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮是否禁用回调方法
    /// </summary>
    [Parameter]
    public Func<List<TItem>, bool>? DisableDeleteButtonCallback { get; set; }

    /// <summary>
    /// 获得/设置 编辑按钮是否禁用回调方法
    /// </summary>
    [Parameter]
    public Func<List<TItem>, bool>? DisableEditButtonCallback { get; set; }

    [CascadingParameter]
    private ContextMenuZone? ContextMenuZone { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool UpdateSortTooltip { get; set; }

    private bool _isFilterTrigger;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 初始化节点缓存
        TreeNodeCache ??= new(Equals);
        OnInitLocalization();

        // 设置 OnSort 回调方法
        InternalOnSortAsync = (sortName, sortOrder) =>
        {
            // 调用 OnSort 回调方法
            if (OnSort != null)
            {
                SortString = OnSort(sortName, SortOrder);
            }

            // 重新查询
            return QueryAsync();
        };

        // 设置 OnFilter 回调方法
        OnFilterAsync = () =>
        {
            PageIndex = 1;
            TotalCount = 0;
            if (ScrollMode == ScrollMode.Virtual)
            {
                _isFilterTrigger = true;
            }
            return QueryAsync();
        };
    }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        if (IsTree && Items != null && TreeNodeConverter != null)
        {
            TreeRows.AddRange(await TreeNodeConverter(Items));
        }
    }

    private void OnInitParameters()
    {
        var op = Options.CurrentValue;
        if (ShowCheckboxTextColumnWidth == 0)
        {
            ShowCheckboxTextColumnWidth = op.TableSettings.ShowCheckboxTextColumnWidth;
        }

        if (DetailColumnWidth == 0)
        {
            DetailColumnWidth = op.TableSettings.DetailColumnWidth;
        }

        if (LineNoColumnWidth == 0)
        {
            LineNoColumnWidth = op.TableSettings.LineNoColumnWidth;
        }

        if (CheckboxColumnWidth == 0)
        {
            CheckboxColumnWidth = op.TableSettings.CheckboxColumnWidth;
        }

        if (op.TableSettings.TableRenderMode != null && RenderMode == TableRenderMode.Auto)
        {
            RenderMode = op.TableSettings.TableRenderMode.Value;
        }

        PageItemsSource ??= [20, 50, 100, 200, 500, 1000];

        if (_originPageItems != PageItems)
        {
            _originPageItems = PageItems;
            _pageItems = 0;
        }

        if (_pageItems == 0)
        {
            // 如果未设置 PageItems 取默认值第一个
            _pageItems = _originPageItems ?? PageItemsSource.First();
        }

        if (ExtendButtonColumnAlignment == Alignment.None)
        {
            ExtendButtonColumnAlignment = Alignment.Center;
        }

        if (LineNoColumnAlignment == Alignment.None)
        {
            LineNoColumnAlignment = Alignment.Center;
        }

        SortIconAsc ??= IconTheme.GetIconByKey(ComponentIcons.TableSortIconAsc);
        SortIconDesc ??= IconTheme.GetIconByKey(ComponentIcons.TableSortDesc);
        SortIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableSortIcon);
        FilterIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterIcon);
        ExportButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableExportButtonIcon);
        ColumnToolboxIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableColumnToolboxIcon);

        AddButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableAddButtonIcon);
        EditButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableEditButtonIcon);
        DeleteButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableDeleteButtonIcon);
        RefreshButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableRefreshButtonIcon);
        CardViewButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableCardViewButtonIcon);
        ColumnListButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableColumnListButtonIcon);
        CsvExportIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableExportCsvIcon);
        ExcelExportIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableExportExcelIcon);
        PdfExportIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableExportPdfIcon);
        SearchButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableSearchButtonIcon);
        ResetSearchButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableResetSearchButtonIcon);
        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableCloseButtonIcon);
        SaveButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableSaveButtonIcon);
        AdvanceButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableAdvanceButtonIcon);
        CancelButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableCancelButtonIcon);
        CopyColumnButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableCopyColumnButtonIcon);
        GearIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableGearIcon);

        TreeIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableTreeIcon);
        TreeExpandIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableTreeExpandIcon);
        TreeNodeLoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableTreeNodeLoadingIcon);
        AdvancedSortButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableAdvancedSortButtonIcon);

        SearchModel ??= CreateSearchModel();
    }

    /// <summary>
    /// 获得/设置 是否为第一次 Render
    /// </summary>
    protected bool FirstRender { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动刷新 CancellationTokenSource 实例
    /// </summary>
    protected CancellationTokenSource? AutoRefreshCancelTokenSource { get; set; }

    private bool _bindResizeColumn;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        OnInitParameters();

        if (Items != null && OnQueryAsync != null)
        {
            throw new InvalidOperationException($"{GetType()} can only accept one item source from its parameters. Do not supply both '{nameof(Items)}' and '{nameof(OnQueryAsync)}'.");
        }

        if (ScrollMode == ScrollMode.Virtual)
        {
            IsFixedHeader = true;
            RenderMode = TableRenderMode.Table;
            IsPagination = false;
        }

        RowsCache = null;

        if (IsExcel)
        {
            IsStriped = false;
            IsMultipleSelect = true;
            IsTree = false;
        }

        if (!FirstRender)
        {
            // 动态列模式
            ResetDynamicContext();

            // resize column width;
            ResetColumnWidth();
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
            await ProcessFirstRender();
        }

        if (_viewChanged)
        {
            _viewChanged = false;
            await InvokeVoidAsync("toggleView", Id);
        }

        if (_breakPointChanged)
        {
            _breakPointChanged = false;
            await InvokeVoidAsync("reset", Id);
        }

        if (_resetColumns)
        {
            _resetColumns = false;
            await InvokeVoidAsync("resetColumn", Id);
        }

        if (_bindResizeColumn)
        {
            _bindResizeColumn = false;
            await InvokeVoidAsync("bindResizeColumn", Id);
        }

        if (UpdateSortTooltip)
        {
            UpdateSortTooltip = false;
            await InvokeVoidAsync("sort", Id);
        }

        if (AutoScrollLastSelectedRowToView)
        {
            await InvokeVoidAsync("scroll", Id, AutoScrollVerticalAlign.ToDescriptionString());
        }

        if (_isFilterTrigger)
        {
            _isFilterTrigger = false;
            await InvokeVoidAsync("scrollTo", Id);
        }

        // 增加去重保护 _loop 为 false 时执行
        if (!_loop && IsAutoRefresh && AutoRefreshInterval > 500)
        {
            _loop = true;
            await LoopQueryAsync();
            _loop = false;
        }
    }

    private async Task OnTableRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InvokeVoidAsync("init", Id, Interop, new
            {
                DragColumnCallback = nameof(DragColumnCallback),
                AutoFitContentCallback = nameof(AutoFitContentCallback),
                ResizeColumnCallback = OnResizeColumnAsync != null ? nameof(ResizeColumnCallback) : null,
                ColumnMinWidth = ColumnMinWidth ?? Options.CurrentValue.TableSettings.ColumnMinWidth,
                ScrollWidth = ActualScrollWidth,
                ShowColumnWidthTooltip,
                ColumnWidthTooltipPrefix,
                ColumnToolboxContent = new List<object>()
                {
                    new
                    {
                        Key = "align-left",
                        Icon = "fa-solid fa-align-left",
                        Text = Localizer["AlignLeftText"].Value,
                        Tooltip = Localizer["AlignLeftTooltipText"].Value
                    },
                    new
                    {
                        Key = "align-center",
                        Icon = "fa-solid fa-align-center",
                        Text = Localizer["AlignCenterText"].Value,
                        Tooltip = Localizer["AlignCenterTooltipText"].Value
                    },
                    new
                    {
                        Key = "align-left",
                        Icon = "fa-solid fa-align-right",
                        Text = Localizer["AlignRightText"].Value,
                        Tooltip = Localizer["AlignRightTooltipText"].Value
                    }
                }
            });
        }

        if (OnAfterRenderCallback != null)
        {
            await OnAfterRenderCallback(this, firstRender);
        }
    }

    private int? _localStorageTableWidth;

    private string? GetTableStyleString(bool hasHeader)
    {
        string? ret = null;
        if (_localStorageTableWidth.HasValue)
        {
            var width = hasHeader ? _localStorageTableWidth.Value : _localStorageTableWidth.Value - ActualScrollWidth;
            ret = $"width: {width}px;";
        }
        return ret;
    }

    private string? GetTableName(bool hasHeader) => hasHeader ? ClientTableName : null;

    private readonly JsonSerializerOptions _serializerOption = new(JsonSerializerDefaults.Web);

    private async Task<List<ColumnWidth>> ReloadColumnWidthFromBrowserAsync()
    {
        List<ColumnWidth>? ret = null;
        if (!string.IsNullOrEmpty(ClientTableName) && AllowResizing)
        {
            var jsonData = await InvokeAsync<string>("reloadColumnWidth", ClientTableName);
            if (!string.IsNullOrEmpty(jsonData))
            {
                try
                {
                    var doc = JsonDocument.Parse(jsonData);
                    if (doc.RootElement.TryGetProperty("cols", out var element))
                    {
                        ret = element.Deserialize<List<ColumnWidth>>(_serializerOption);
                    }
                    if (doc.RootElement.TryGetProperty("table", out var tableEl) && tableEl.TryGetInt32(out var tableWidth))
                    {
                        _localStorageTableWidth = tableWidth;
                    }
                }
                catch { }
            }
        }
        return ret ?? [];
    }

    private async Task ReloadColumnOrdersFromBrowserAsync(List<ITableColumn> columns)
    {
        if (!string.IsNullOrEmpty(ClientTableName))
        {
            var orders = await InvokeAsync<List<string>?>("reloadColumnOrder", ClientTableName);
            if (orders != null)
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    var col = columns.Find(c => c.GetFieldName() == orders[i]);
                    if (col != null)
                    {
                        col.Order = i + 1;
                    }
                }
            }
        }
    }

    private async Task ProcessFirstRender()
    {
        IsLoading = true;

        // 设置渲染完毕
        FirstRender = false;

        // 动态列模式
        var cols = new List<ITableColumn>();
        if (DynamicContext != null && typeof(TItem).IsAssignableTo(typeof(IDynamicObject)))
        {
            cols.AddRange(DynamicContext.GetColumns());
        }
        else if (AutoGenerateColumns)
        {
            cols.AddRange(Utility.GetTableColumns<TItem>(Columns));
        }
        else
        {
            cols.AddRange(Columns);
        }

        if (ColumnOrderCallback != null)
        {
            cols = ColumnOrderCallback(cols).ToList();
        }

        await ReloadColumnOrdersFromBrowserAsync(cols);
        Columns.Clear();
        Columns.AddRange(cols.OrderFunc());

        // 查看是否开启列宽序列化
        _clientColumnWidths = await ReloadColumnWidthFromBrowserAsync();
        ResetColumnWidth();

        if (OnColumnCreating != null)
        {
            await OnColumnCreating(Columns);
        }

        InternalResetVisibleColumns();

        // set default sortName
        var col = Columns.Find(i => i is { Sortable: true, DefaultSort: true });
        if (col != null)
        {
            SortName = col.GetFieldName();
            SortOrder = col.DefaultSortOrder;
        }

        // 获取是否自动查询参数值
        _autoQuery = IsAutoQueryFirstRender;

        _firstQuery = true;
        await QueryAsync();
        _firstQuery = false;

        // 恢复自动查询功能
        _autoQuery = true;
        IsLoading = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        ScreenSize = BreakPoint.None;
        var breakPoint = await InvokeAsync<BreakPoint>("getResponsive");
        if (breakPoint != BreakPoint.None)
        {
            ScreenSize = breakPoint;
        }
    }

    private void ResetColumnWidth()
    {
        foreach (var cw in _clientColumnWidths.Where(c => c.Width > 0))
        {
            var c = Columns.Find(c => c.GetFieldName() == cw.Name);
            if (c != null)
            {
                c.Width = cw.Width;
            }
        }
    }

    private void InternalResetVisibleColumns(IEnumerable<ColumnVisibleItem>? items = null)
    {
        var cols = Columns.Select(i => new ColumnVisibleItem(i.GetFieldName(), i.GetVisible()) { DisplayName = i.GetDisplayName() }).ToList();
        if (items != null)
        {
            foreach (var column in cols)
            {
                var item = items.FirstOrDefault(i => i.Name == column.Name);
                if (item != null)
                {
                    column.Visible = item.Visible;
                    if (!string.IsNullOrEmpty(item.DisplayName))
                    {
                        column.DisplayName = item.DisplayName;
                    }
                }
            }
        }
        VisibleColumns.Clear();
        VisibleColumns.AddRange(cols);
    }

    /// <summary>
    /// 设置 列可见方法
    /// </summary>
    /// <param name="columns"></param>
    public void ResetVisibleColumns(IEnumerable<ColumnVisibleItem> columns)
    {
        InternalResetVisibleColumns(columns);
        StateHasChanged();
    }

    /// <summary>
    /// 周期性查询方法
    /// </summary>
    /// <returns></returns>
    protected async Task LoopQueryAsync()
    {
        try
        {
            AutoRefreshCancelTokenSource ??= new();
            // 自动刷新功能
            await Task.Delay(AutoRefreshInterval, AutoRefreshCancelTokenSource.Token);

            // 不调用 QueryAsync 防止出现 Loading 动画 保持屏幕静止
            await QueryData();
            StateHasChanged();
        }
        catch (TaskCanceledException) { }
    }

    private bool _loop;
    private bool _firstQuery;
    private bool _autoQuery;

    /// <summary>
    /// OnQueryAsync 查询结果数据集合
    /// </summary>
    private IEnumerable<TItem> QueryItems { get; set; } = [];

    [NotNull]
    private List<TItem>? RowsCache { get; set; }

    /// <summary>
    /// 获得 当前表格所有 Rows 集合
    /// </summary>
    public List<TItem> Rows
    {
        get
        {
            // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I5JG5D
            // 如果 QueryItems 无默认值
            // 页面 OnInitializedAsync 二刷再 OnAfterRender 过程中导致 QueryItems 变量为空 ToList 报错
            RowsCache ??= IsTree ? TreeRows.GetAllItems() : (Items ?? QueryItems).ToList();
            return RowsCache;
        }
    }

    #region 生成 Row 方法
    /// <summary>
    /// 获得 指定单元格数据方法
    /// </summary>
    /// <param name="col"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    protected RenderFragment GetValue(ITableColumn col, TItem item) => builder =>
    {
        if (col.Template != null)
        {
            builder.AddContent(0, col.Template(item));
        }
        else if (col.ComponentType == typeof(ColorPicker))
        {
            // 自动化处理 ColorPicker 组件
            builder.AddContent(10, col.RenderColor(item));
        }
        else
        {
            if (col.Lookup == null && !string.IsNullOrEmpty(col.LookupServiceKey))
            {
                // 未设置 Lookup
                // 设置 LookupService 键值
                col.Lookup = LookupService.GetItemsByKey(col.LookupServiceKey, col.LookupServiceData);
            }
            builder.AddContent(20, col.RenderValue(item));
        }
    };
    #endregion

    /// <summary>
    /// 渲染单元格方法
    /// </summary>
    /// <param name="col"></param>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    protected RenderFragment RenderCell(ITableColumn col, TItem item, ItemChangedType changedType)
    {
        return col.CanWrite(typeof(TItem), changedType) ? RenderEditTemplate() : RenderTemplate();

        RenderFragment RenderTemplate() => col.Template == null
            ? new RenderFragment(builder => builder.CreateDisplayByFieldType(col, item))
            : col.Template(item);

        RenderFragment RenderEditTemplate() => col.EditTemplate == null
            ? new RenderFragment(builder => builder.CreateComponentByFieldType(this, col, item, changedType, false, LookupService))
            : col.EditTemplate(item);
    }

    /// <summary>
    /// 渲染 Excel 单元格方法
    /// </summary>
    /// <param name="col"></param>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    protected RenderFragment RenderExcelCell(ITableColumn col, TItem item, ItemChangedType changedType)
    {
        col.PlaceHolder ??= "";
        col.IsPopover = true;

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
                var onValueChanged = Utility.GetOnValueChangedInvoke<IDynamicObject>(col.PropertyType);
                if (DynamicContext.OnValueChanged != null)
                {
                    var parameters = col.ComponentParameters?.ToList() ?? [];
                    parameters.Add(new(nameof(ValidateBase<string>.OnValueChanged), onValueChanged.Invoke(d, col, (model, column, val) => DynamicContext.OnValueChanged(model, column, val))));
                    col.ComponentParameters = parameters;
                }
                builder.CreateComponentByFieldType(this, col, row, changedType, false, LookupService);
            };
        }

        void SetEditTemplate()
        {
            var onValueChanged = Utility.GetOnValueChangedInvoke<TItem>(col.PropertyType);
            var parameters = col.ComponentParameters?.ToList() ?? [];
            parameters.Add(new(nameof(ValidateBase<string>.OnValueChanged), onValueChanged(item, col, (model, column, val) => InternalOnSaveAsync(model, ItemChangedType.Update))));
            col.ComponentParameters = parameters;
        }
    }

    #region Filter
    /// <summary>
    /// 获得/设置 表头过滤时回调方法
    /// </summary>
    public Func<Task>? OnFilterAsync { get; private set; }

    /// <summary>
    /// 获得 过滤集合
    /// </summary>
    public Dictionary<string, IFilterAction> Filters { get; } = [];
    #endregion

    private async ValueTask<ItemsProviderResult<TItem>> LoadItems(ItemsProviderRequest request)
    {
        StartIndex = _isFilterTrigger ? 0 : request.StartIndex;
        _pageItems = TotalCount > 0 ? Math.Min(request.Count, TotalCount - request.StartIndex) : request.Count;
        await QueryData();
        return new ItemsProviderResult<TItem>(QueryItems, TotalCount);
    }

    private Func<Task> TriggerDoubleClickCell(ITableColumn col, TItem item) => async () =>
    {
        if (OnDoubleClickCellCallback != null)
        {
            var val = col.GetItemValue(item);
            await OnDoubleClickCellCallback(col.GetFieldName(), item, val);
        }
    };

    private static string? GetDoubleClickCellClassString(bool trigger) => CssBuilder.Default()
        .AddClass("is-dbcell", trigger)
        .Build();

    private bool IsShowEmpty => ShowEmpty && Rows.Count == 0;

    private int GetColumnCount()
    {
        var colSpan = GetVisibleColumns().Count();
        if (IsMultipleSelect)
        {
            colSpan++;
        }

        if (ShowLineNo)
        {
            colSpan++;
        }

        if (ShowExtendButtons)
        {
            colSpan++;
        }
        return colSpan;
    }

    private int GetEmptyColumnCount() => ShowDetails() ? GetColumnCount() + 1 : GetColumnCount();

    private bool GetShowHeader()
    {
        var ret = true;
        if (MultiHeaderTemplate != null)
        {
            ret = ShowMultiFilterHeader;
        }
        return ret;
    }

    private int GetLineNo(TItem item) => Rows.IndexOf(item) + 1 + ((ScrollMode == ScrollMode.Virtual && Items == null) ? StartIndex : (PageIndex - 1) * _pageItems);

    /// <summary>
    /// Reset all Columns Filter
    /// </summary>
    public async Task ResetFilters()
    {
        foreach (var column in Columns)
        {
            column.Filter?.FilterAction.Reset();
        }
        Filters.Clear();

        if (OnFilterAsync != null)
        {
            await OnFilterAsync();
        }
    }

    /// <summary>
    /// Reset all Columns Sort
    /// </summary>
    public async Task ResetSortAsync()
    {
        SortName = null;
        SortOrder = SortOrder.Unset;

        await QueryData();
    }

    /// <summary>
    /// 返回 true 时按钮禁用
    /// </summary>
    /// <returns></returns>
    private bool GetEditButtonStatus() => ShowAddForm || AddInCell || (DisableEditButtonCallback?.Invoke(SelectedRows) ?? SelectedRows.Count != 1);

    /// <summary>
    /// 返回 true 时按钮禁用
    /// </summary>
    /// <returns></returns>
    private bool GetDeleteButtonStatus() => ShowAddForm || AddInCell || (DisableDeleteButtonCallback?.Invoke(SelectedRows) ?? SelectedRows.Count == 0);

    private async Task InvokeItemsChanged()
    {
        if (ItemsChanged.HasDelegate)
        {
            await ItemsChanged.InvokeAsync(Rows);
        }
    }

    private async Task OnContextMenu(MouseEventArgs e, TItem item)
    {
        if (ContextMenuZone != null)
        {
            await ContextMenuZone.OnContextMenu(e, item);
        }
    }

    /// <summary>
    /// 获得/设置 是否允许拖放标题栏更改栏位顺序，默认为 false
    /// </summary>
    [Parameter]
    public bool AllowDragColumn { get; set; }

    private string? DraggableString => AllowDragColumn ? "true" : null;

    /// <summary>
    /// 获得/设置 拖动列结束回调方法，默认 null 可存储数据库用于服务器端保持列顺序
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<ITableColumn>, Task>? OnDragColumnEndAsync { get; set; }

    /// <summary>
    /// 获得/设置 设置列宽回调方法
    /// </summary>
    [Parameter]
    public Func<string, float, Task>? OnResizeColumnAsync { get; set; }

    /// <summary>
    /// 获得/设置 自动调整列宽回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task<float>>? OnAutoFitContentAsync { get; set; }

    /// <summary>
    /// 重置列方法 由 JavaScript 脚本调用
    /// </summary>
    /// <param name="originIndex"></param>
    /// <param name="currentIndex"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task DragColumnCallback(int originIndex, int currentIndex)
    {
        var firstColumn = GetVisibleColumns().ElementAtOrDefault(originIndex);
        var targetColumn = GetVisibleColumns().ElementAtOrDefault(currentIndex);
        if (firstColumn != null && targetColumn != null)
        {
            var index = Columns.IndexOf(targetColumn);
            Columns.Remove(firstColumn);
            Columns.Insert(index, firstColumn);

            if (OnDragColumnEndAsync != null)
            {
                await OnDragColumnEndAsync(firstColumn.GetFieldName(), Columns);
            }
            if (!string.IsNullOrEmpty(ClientTableName))
            {
                var cols = Columns.Select(i => i.GetFieldName()).ToList();
                await InvokeVoidAsync("saveColumnOrder", new { TableName = ClientTableName, Columns = cols });
            }
            StateHasChanged();
        }
    }

    /// <summary>
    /// 设置列宽方法 由 JavaScript 脚本调用
    /// </summary>
    /// <param name="index"></param>
    /// <param name="width"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task ResizeColumnCallback(int index, float width)
    {
        var column = GetVisibleColumns().Where(i => !i.Fixed).ElementAtOrDefault(index);
        if (column != null && OnResizeColumnAsync != null)
        {
            await OnResizeColumnAsync(column.GetFieldName(), width);
        }
    }

    /// <summary>
    /// 列宽自适应回调方法 由 JavaScript 脚本调用
    /// </summary>
    /// <param name="fieldName">当前列名称</param>
    /// <returns></returns>
    [JSInvokable]
    public async Task<float> AutoFitContentCallback(string fieldName)
    {
        float ret = 0;
        if (OnAutoFitContentAsync != null)
        {
            ret = await OnAutoFitContentAsync(fieldName);
        }
        return ret;
    }

    /// <summary>
    /// 是否触摸
    /// </summary>
    private bool TouchStart { get; set; }

    /// <summary>
    /// 触摸定时器工作指示
    /// </summary>
    private bool IsBusy { get; set; }

    private async Task OnTouchStart(TouchEventArgs e, TItem item)
    {
        if (!IsBusy && ContextMenuZone != null)
        {
            IsBusy = true;
            TouchStart = true;

            // 延时保持 TouchStart 状态
            var delay = Options.CurrentValue.ContextMenuOptions.OnTouchDelay;
            await Task.Delay(delay);
            if (TouchStart)
            {
                var args = new MouseEventArgs()
                {
                    ClientX = e.Touches[0].ClientX,
                    ClientY = e.Touches[0].ClientY,
                    ScreenX = e.Touches[0].ScreenX,
                    ScreenY = e.Touches[0].ScreenY,
                };
                // 弹出关联菜单
                await OnContextMenu(args, item);

                //延时防止重复激活菜单功能
                await Task.Delay(delay);
            }
            IsBusy = false;
        }
    }

    private void OnTouchEnd()
    {
        TouchStart = false;
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            AutoRefreshCancelTokenSource?.Cancel();
            AutoRefreshCancelTokenSource?.Dispose();
            AutoRefreshCancelTokenSource = null;
        }

        await base.DisposeAsync(disposing);
    }
}
