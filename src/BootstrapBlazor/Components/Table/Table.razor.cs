// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件基类
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TItem))]
#endif
public partial class Table<TItem> : ITable, IModelEqualityComparer<TItem> where TItem : class, new()
{
    /// <summary>
    /// 获得/设置 内置虚拟化组件实例
    /// </summary>
    protected Virtualize<TItem>? VirtualizeElement { get; set; }

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
        .AddClass("table-layout-fixed", IsFixedHeader)
        .Build();

    /// <summary>
    /// 获得 wrapper 样式表集合
    /// </summary>
    protected string? WrapperClassName => CssBuilder.Default()
        .AddClass("table-wrapper", IsBordered)
        .AddClass("is-clickable", ClickToSelect || DoubleClickToEdit || OnClickRowCallback != null || OnDoubleClickRowCallback != null)
        .AddClass("table-scroll", !IsFixedHeader || FixedColumn)
        .AddClass("table-fixed", IsFixedHeader)
        .AddClass("table-fixed-column", FixedColumn)
        .AddClass("table-resize", AllowResizing)
        .AddClass("table-fixed-body", RenderMode == TableRenderMode.CardView && IsFixedHeader)
        .AddClass("table-striped table-hover", ActiveRenderMode == TableRenderMode.CardView && IsStriped)
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

    private string? GetSortTooltip(ITableColumn col) => SortName != col.GetFieldName()
        ? UnsetText
        : SortOrder switch
        {
            SortOrder.Asc => SortAscText,
            SortOrder.Desc => SortDescText,
            _ => UnsetText
        };

    private static string GetHeaderTooltipText(string? headerTooltip, string displayName) => headerTooltip ?? displayName;

    private static string? GetColspan(int colspan) => colspan > 1 ? colspan.ToString() : null;

    private bool IsShowFooter => ShowFooter && (Rows.Any() || !IsHideFooterWhenNoData);

    private int PageStartIndex => Rows.Any() ? (PageIndex - 1) * PageItems + 1 : 0;

    private string? PageInfoLabelString => Localizer[nameof(PageInfoText), PageStartIndex, (PageIndex - 1) * PageItems + Rows.Count, TotalCount];

    private static string? GetColWidthString(int? width) => width.HasValue ? $"width: {width.Value}px;" : null;

    /// <summary>
    /// 获得/设置 Table 高度 默认为 null
    /// </summary>
    /// <remarks>开启固定表头功能时生效 <see cref="IsFixedHeader"/></remarks>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// 获得/设置 固定表头 默认 false
    /// </summary>
    /// <remarks>固定表头时设置 <see cref="Height"/> 即可出现滚动条，未设置时尝试自适应</remarks>
    [Parameter]
    public bool IsFixedHeader { get; set; }

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
    protected List<TItem> ExpandRows { get; } = new List<TItem>();

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

    [Inject]
    [NotNull]
    private ILookupService? LookupService { get; set; }

    private Task OnBreakPointChanged(BreakPoint size)
    {
        if (size != ScreenSize)
        {
            ScreenSize = size;
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private bool ShowDetails() => IsDetails == null
        ? DetailRowTemplate != null
        : IsDetails.Value && DetailRowTemplate != null;

    /// <summary>
    /// 明细行功能中切换行状态时调用此方法
    /// </summary>
    /// <param name="item"></param>
    public void ExpandDetailRow(TItem item)
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
    /// 明细行集合用于数据懒加载
    /// </summary>
    protected List<TItem> DetailRows { get; } = new List<TItem>();

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
    /// 获得/设置 OnAfterRenderCallback 是否已经触发 默认 false
    /// </summary>
    /// <remarks>与 <see cref="OnAfterRenderCallback"/> 回调配合</remarks>
    private bool OnAfterRenderIsTriggered { get; set; }

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

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool UpdateSortTooltip { get; set; }

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
        InternalOnSortAsync = async (sortName, sortOrder) =>
        {
            // 调用 OnSort 回调方法
            if (OnSort != null)
            {
                SortString = OnSort(sortName, SortOrder);
            }

            // 重新查询
            await QueryAsync();
        };

        // 设置 OnFilter 回调方法
        OnFilterAsync = async () =>
        {
            PageIndex = 1;
            await QueryAsync();
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

        PageItemsSource ??= new int[] { 20, 50, 100, 200, 500, 1000 };

        if (PageItems == 0)
        {
            // 如果未设置 PageItems 取默认值第一个
            PageItems = PageItemsSource.First();
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

        AddButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableAddButtonIcon);
        EditButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableEditButtonIcon);
        DeleteButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableDeleteButtonIcon);
        RefreshButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableRefreshButtonIcon);
        CardViewButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableCardViewButtonIcon);
        ColumnListButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableColumnListButtonIcon);
        ExcelExportIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableExcelExportIcon);
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
    }

    /// <summary>
    /// 获得/设置 是否为第一次 Render
    /// </summary>
    protected bool FirstRender { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动刷新 CancellationTokenSource 实例
    /// </summary>
    protected CancellationTokenSource? AutoRefreshCancelTokenSource { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        OnInitParameters();

        if (ScrollMode == ScrollMode.Virtual)
        {
            IsFixedHeader = true;
            RenderMode = TableRenderMode.Table;
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
            IsLoading = true;

            // 设置渲染完毕
            FirstRender = false;

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
                var cols = Utility.GetTableColumns<TItem>(Columns);
                Columns.Clear();
                Columns.AddRange(cols);
            }

            if (OnColumnCreating != null)
            {
                await OnColumnCreating(Columns);
            }

            InternalResetVisibleColumns(Columns.Select(i => new ColumnVisibleItem(i.GetFieldName(), i.Visible)));

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

            IsLoading = false;
        }

        if (!OnAfterRenderIsTriggered && OnAfterRenderCallback != null)
        {
            OnAfterRenderIsTriggered = true;
            await OnAfterRenderCallback(this);
        }

        if (_init)
        {
            _init = false;
            await InvokeVoidAsync("init", Id, Interop);
        }

        if (UpdateSortTooltip)
        {
            UpdateSortTooltip = false;
            await InvokeVoidAsync("sort", Id);
        }

        // 增加去重保护 _loop 为 false 时执行
        if (!_loop && IsAutoRefresh && AutoRefreshInterval > 500)
        {
            _loop = true;
            await LoopQueryAsync();
            _loop = false;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync() => ScreenSize = await InvokeAsync<BreakPoint>("getResponsive");

    private void InternalResetVisibleColumns(IEnumerable<ColumnVisibleItem> columns)
    {
        VisibleColumns.Clear();
        VisibleColumns.AddRange(columns);
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
        catch (TaskCanceledException)
        {

        }
    }

    private bool _loop;
    private bool _init;

    /// <summary>
    /// 检查当前列是否显示方法
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected bool CheckShownWithBreakpoint(ITableColumn col) => ScreenSize >= col.ShownWithBreakPoint;

    /// <summary>
    /// OnQueryAsync 查询结果数据集合
    /// </summary>
    private IEnumerable<TItem> QueryItems { get; set; } = Enumerable.Empty<TItem>();

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
    protected RenderFragment GetValue(ITableColumn col, TItem item) => async builder =>
    {
        if (col.Template != null)
        {
            builder.AddContent(0, col.Template(item));
        }
        else if (col.ComponentType == typeof(ColorPicker))
        {
            // 自动化处理 ColorPicker 组件
            var val = GetItemValue(col.GetFieldName(), item);
            var v = val?.ToString() ?? "#000";
            var style = $"background-color: {v};";
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "is-color");
            builder.AddAttribute(2, "style", style);
            builder.CloseElement();
        }
        else
        {
            var val = GetItemValue(col.GetFieldName(), item);

            if (col.Lookup == null && !string.IsNullOrEmpty(col.LookupServiceKey))
            {
                // 未设置 Lookup
                // 设置 LookupService 键值
                col.Lookup = LookupService.GetItemsByKey(col.LookupServiceKey);
            }

            if (col.Lookup == null && val is bool v1)
            {
                // 自动化处理 bool 值
                builder.OpenComponent(0, typeof(Switch));
                builder.AddAttribute(1, "Value", v1);
                builder.AddAttribute(2, "IsDisabled", true);
                builder.CloseComponent();
            }
            else if (col.Lookup != null && val != null)
            {
                // 转化 Lookup 数据源
                var lookupVal = col.Lookup.FirstOrDefault(l => l.Value.Equals(val.ToString(), col.LookupStringComparison));
                if (lookupVal != null)
                {
                    builder.AddContent(0, RenderTooltip(lookupVal.Text));
                }
            }
            else
            {
                string? content;
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
                    content = val?.ToString();
                }
                builder.AddContent(0, RenderTooltip(content));
            }
        }

        RenderFragment RenderTooltip(string? text) => pb =>
        {
            if (col.ShowTips && !string.IsNullOrEmpty(text))
            {
                pb.OpenComponent<Tooltip>(0);
                pb.AddAttribute(1, nameof(Tooltip.Title), text);
                pb.AddAttribute(2, nameof(Tooltip.ChildContent), RenderContent());
                pb.CloseComponent();
            }
            else
            {
                pb.AddContent(3, text);
            }

            RenderFragment RenderContent() => context => context.AddContent(0, text);
        };
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
                        // 如果是枚举这里返回 枚举的描述信息
                        var itemName = ret.ToString();
                        if (!string.IsNullOrEmpty(itemName))
                        {
                            ret = Utility.GetDisplayName(t, itemName);
                        }
                    }
                }
            }
        }
        return ret;
    }
    #endregion

    /// <summary>
    /// 渲染单元格方法
    /// </summary>
    /// <param name="col"></param>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    protected RenderFragment RenderCell(ITableColumn col, TItem item, ItemChangedType changedType) => col.CanWrite(typeof(TItem), changedType)
        ? (col.EditTemplate == null
            ? builder => builder.CreateComponentByFieldType(this, col, item, changedType, false, LookupService)
            : col.EditTemplate(item))
        : (col.Template == null
            ? builder => builder.CreateDisplayByFieldType(col, item)
            : col.Template(item));

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
                    var parameters = col.ComponentParameters?.ToList() ?? new List<KeyValuePair<string, object>>();
                    parameters.Add(new(nameof(ValidateBase<string>.OnValueChanged), onValueChanged.Invoke(d, col, (model, column, val) => DynamicContext.OnValueChanged(model, column, val))));
                    col.ComponentParameters = parameters;
                }
                builder.CreateComponentByFieldType(this, col, row, changedType, false, LookupService);
            };
        }

        void SetEditTemplate()
        {
            var onValueChanged = Utility.GetOnValueChangedInvoke<TItem>(col.PropertyType);
            col.ComponentParameters = new List<KeyValuePair<string, object>>
            {
                new(nameof(ValidateBase<string>.OnValueChanged), onValueChanged(item, col, (model, column, val) => InternalOnSaveAsync(model, ItemChangedType.Update)))
            };
        }
    }

    #region Filter
    /// <summary>
    /// 获得/设置 表头过滤时回调方法
    /// </summary>
    [NotNull]
    public Func<Task>? OnFilterAsync { get; private set; }

    /// <summary>
    /// 获得 过滤集合
    /// </summary>
    public Dictionary<string, IFilterAction> Filters { get; } = new();
    #endregion

    private async ValueTask<ItemsProviderResult<TItem>> LoadItems(ItemsProviderRequest request)
    {
        StartIndex = request.StartIndex;
        if (TotalCount > 0)
        {
            PageItems = Math.Min(request.Count, TotalCount - request.StartIndex);
        }
        await QueryData();
        return new ItemsProviderResult<TItem>(QueryItems, TotalCount);
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

    private bool IsShowEmpty => ShowEmpty && !Rows.Any();

    private int GetColumnCount()
    {
        var colspan = GetVisibleColumns().Count(col => col.Visible);
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
        await OnFilterAsync();
    }

    /// <summary>
    /// 返回 true 时按钮禁用
    /// </summary>
    /// <returns></returns>
    private bool GetEditButtonStatus() => ShowAddForm || AddInCell || SelectedRows.Count != 1;

    /// <summary>
    /// 返回 true 时按钮禁用
    /// </summary>
    /// <returns></returns>
    private bool GetDeleteButtonStatus() => ShowAddForm || AddInCell || !SelectedRows.Any();

    private async Task InvokeItemsChanged()
    {
        if (ItemsChanged.HasDelegate)
        {
            await ItemsChanged.InvokeAsync(Rows);
        }
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

            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }

        await base.DisposeAsync(disposing);
    }
}
