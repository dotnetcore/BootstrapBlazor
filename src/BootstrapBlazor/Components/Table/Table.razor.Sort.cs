﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Table<TItem>
{
    /// <summary>
    /// 获得/设置 排序字段名称
    /// </summary>
    protected string? SortName { get; set; }

    /// <summary>
    /// 获得/设置 排序方式
    /// </summary>
    protected SortOrder SortOrder { get; set; }

    /// <summary>
    /// 获得/设置 升序图标 fa-solid fa-sort-up
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SortIconAsc { get; set; }

    /// <summary>
    /// 获得/设置 降序图标 fa-solid fa-sort-down
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SortIconDesc { get; set; }

    /// <summary>
    /// 获得/设置 默认图标 fa-solid fa-sort
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SortIcon { get; set; }

    /// <summary>
    /// 获得/设置 过滤图标 默认 fa-solid fa-filter
    /// </summary>
    [Parameter]
    public string? FilterIcon { get; set; }

    /// <summary>
    /// 获得/设置 多列排序顺序 默认为空 多列时使用逗号分割 如："Name, Age desc"
    /// </summary>
    [Parameter]
    public string? SortString { get; set; }

    /// <summary>
    /// 获得/设置 点击表头排序时回调方法
    /// </summary>
    [Parameter]
    public Func<string, SortOrder, string>? OnSort { get; set; }

    /// <summary>
    /// 获得/设置 内部表头排序时回调方法
    /// </summary>
    [NotNull]
    protected Func<string, SortOrder, Task>? InternalOnSortAsync { get; set; }

    /// <summary>
    /// 点击列进行排序方法
    /// </summary>
    protected Func<Task> OnClickHeader(ITableColumn col) => async () =>
    {
        UpdateSortTooltip = true;

        if (SortOrder == SortOrder.Unset)
        {
            SortOrder = SortOrder.Asc;
        }
        else if (SortOrder == SortOrder.Asc)
        {
            SortOrder = SortOrder.Desc;
        }
        else if (SortOrder == SortOrder.Desc)
        {
            SortOrder = SortOrder.Unset;
        }

        SortName = col.GetFieldName();

        // 清除高级排序 (保证点击 Header 排序的优先级最高)
        AdvancedSortItems.Clear();

        // 通知 Table 组件刷新数据
        await InternalOnSortAsync(SortName, SortOrder);
    };

    /// <summary>
    /// 获取指定列头样式字符串
    /// </summary>
    /// <param name="col"></param>
    /// <param name="isFilterHeader"></param>
    /// <returns></returns>
    protected string? GetHeaderClassString(ITableColumn col, bool isFilterHeader = false) => CssBuilder.Default()
        .AddClass("sortable", col.GetSortable() && !isFilterHeader)
        .AddClass("filterable", col.GetFilterable())
        .AddClass(("toolbox"), col.ToolboxTemplate != null)
        .AddClass(GetFixedCellClassString(col))
        .Build();

    /// <summary>
    /// 获得列头单元格样式
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected string? GetHeaderCellClassString(ITableColumn col) => CssBuilder.Default()
        .AddClass("table-text")
        .AddClass("text-truncate", col.HeaderTextEllipsis)
        .AddClass("text-wrap", HeaderTextWrap || col.HeaderTextWrap)
        .Build();

    private string? MultiColumnClassString => CssBuilder.Default()
        .AddClass("fixed", FixedMultipleColumn)
        .AddClass("fr", IsLastMultiColumn())
        .Build();

    private string? DetailColumnClassString => CssBuilder.Default()
        .AddClass("fixed", FixedDetailRowHeaderColumn)
        .AddClass("fr", IsLastDetailColumn())
        .Build();

    private string? LineNoColumnClassString => CssBuilder.Default()
        .AddClass("fixed", FixedLineNoColumn)
        .AddClass("fr", IsLastLineNoColumn())
        .Build();

    private int LineNoColumnLeft()
    {
        var left = 0;
        if (GetFixedDetailRowHeaderColumn && GetFixedMultipleSelectColumn)
        {
            left = DetailColumnWidth + MultiColumnWidth;
        }
        else if (GetFixedMultipleSelectColumn)
        {
            left = MultiColumnWidth;
        }
        else if (GetFixedDetailRowHeaderColumn)
        {
            left = DetailColumnWidth;
        }
        return left;
    }

    private int MultipleSelectColumnLeft()
    {
        var left = 0;
        if (GetFixedDetailRowHeaderColumn)
        {
            left = DetailColumnWidth;
        }
        return left;
    }

    private bool GetFixedDetailRowHeaderColumn => FixedDetailRowHeaderColumn && ShowDetails();

    private bool GetFixedMultipleSelectColumn => FixedMultipleColumn && IsMultipleSelect;

    private bool GetFixedLineNoColumn => FixedLineNoColumn && ShowLineNo;

    private string? DetailColumnStyleString => GetFixedDetailRowHeaderColumn ? "left: 0;" : null;

    private string? LineNoColumnStyleString => GetFixedLineNoColumn ? $"left: {LineNoColumnLeft()}px;" : null;

    private string? MultiColumnStyleString => GetFixedMultipleSelectColumn ? $"left: {MultipleSelectColumnLeft()}px;" : null;

    private int MultiColumnWidth => ShowCheckboxText ? ShowCheckboxTextColumnWidth : CheckboxColumnWidth;

    /// <summary>
    /// 获得指定列头固定列样式
    /// </summary>
    /// <param name="col"></param>
    /// <param name="cellClass"></param>
    /// <returns></returns>
    protected string? GetFixedCellClassString(ITableColumn col, string? cellClass = null) => CssBuilder.Default(cellClass)
        .AddClass("fixed", col.Fixed)
        .AddClass("fixed-right", col.Fixed && IsTail(col))
        .AddClass("fr", IsLastColumn(col))
        .AddClass("fl", IsFirstColumn(col))
        .Build();

    /// <summary>
    /// 获得扩展按钮列固定列样式
    /// </summary>
    /// <returns></returns>
    protected string? FixedExtendButtonsColumnClassString => CssBuilder.Default("table-column-button")
        .AddClass("fixed", FixedExtendButtonsColumn)
        .AddClass("fixed-right", !IsExtendButtonsInRowHeader)
        .AddClass("fr", IsLastExtendButtonColumn())
        .AddClass("fl", IsFirstExtendButtonColumn())
        .Build();

    /// <summary>
    /// 获得 按钮列样式表集合
    /// </summary>
    /// <returns></returns>
    protected string? ExtendButtonsColumnClass => CssBuilder.Default()
        .AddClass("fixed", FixedExtendButtonsColumn)
        .AddClass("fixed-right", !IsExtendButtonsInRowHeader)
        .AddClass("fr", IsLastExtendButtonColumn())
        .AddClass("fl", IsFirstExtendButtonColumn())
        .Build();

    /// <summary>
    /// 获得扩展按钮列固定列样式
    /// </summary>
    /// <returns></returns>
    protected string? GetFixedExtendButtonsColumnStyleString(int margin = 0) => CssBuilder.Default()
        .AddClass($"right: {(IsFixedHeader ? margin : 0)}px;", FixedExtendButtonsColumn && !IsExtendButtonsInRowHeader)
        .AddClass($"left: {GetExtendButtonsColumnLeftMargin()}px;", FixedExtendButtonsColumn && IsExtendButtonsInRowHeader)
        .Build();

    private bool IsLastDetailColumn() => !GetFixedMultipleSelectColumn && !GetFixedLineNoColumn && IsNotFixedColumn();

    private bool IsLastMultiColumn() => !GetFixedLineNoColumn && IsNotFixedColumn();

    private bool IsLastLineNoColumn() => IsNotFixedColumn();

    private bool IsNotFixedColumn() => !(FixedExtendButtonsColumn && IsExtendButtonsInRowHeader) && !(GetVisibleColumns().FirstOrDefault()?.Fixed ?? false);

    private ConcurrentDictionary<ITableColumn, bool> LastFixedColumnCache { get; } = new();

    private bool IsLastColumn(ITableColumn col) => LastFixedColumnCache.GetOrAdd(col, col =>
    {
        var ret = false;
        if (col.Fixed && !IsTail(col))
        {
            var index = Columns.IndexOf(col) + 1;
            ret = index < Columns.Count && Columns[index].Fixed == false;
        }
        return ret;
    });

    private bool IsLastExtendButtonColumn() => IsExtendButtonsInRowHeader && !GetVisibleColumns().Any(i => i.Fixed);

    private ConcurrentDictionary<ITableColumn, bool> FirstFixedColumnCache { get; } = new();

    private bool IsFirstColumn(ITableColumn col) => FirstFixedColumnCache.GetOrAdd(col, col =>
    {
        var ret = false;
        if (col.Fixed && IsTail(col))
        {
            // 查找前一列是否固定
            var index = Columns.IndexOf(col) - 1;
            if (index > 0)
            {
                ret = !Columns[index].Fixed;
            }
        }
        return ret;
    });

    private bool IsFirstExtendButtonColumn() => !IsExtendButtonsInRowHeader && !GetVisibleColumns().Any(i => i.Fixed);

    private int GetExtendButtonsColumnLeftMargin()
    {
        var width = 0;
        if (ShowDetails())
        {
            width += DetailColumnWidth;
        }
        if (ShowLineNo)
        {
            width += LineNoColumnWidth;
        }
        if (FixedMultipleColumn)
        {
            width += MultiColumnWidth;
        }
        return width;
    }

    private int CalcMargin()
    {
        var margin = 0;
        if (ShowDetails())
        {
            margin += DetailColumnWidth;
        }
        if (IsMultipleSelect)
        {
            margin += ShowCheckboxText ? ShowCheckboxTextColumnWidth : CheckboxColumnWidth;
        }
        if (ShowLineNo)
        {
            margin += LineNoColumnWidth;
        }
        return margin;
    }

    private bool IsTail(ITableColumn col)
    {
        var middle = Math.Floor(GetVisibleColumns().Count() * 1.0 / 2);
        var index = Columns.IndexOf(col);
        return middle < index;
    }

    /// <summary>
    /// 获得列单元格 Style 用于设置文本超长溢出
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected string? GetCellStyleString(ITableColumn col)
    {
        return col.GetTextEllipsis() && !AllowResizing
            ? GetFixedHeaderStyleString()
            : null;

        string GetFixedHeaderStyleString() => IsFixedHeader
            ? $"width: calc({col.Width ?? 200}px - 2 * var(--bb-table-td-padding-x));"
            : $"width: {col.Width ?? 200}px;";
    }

    /// <summary>
    /// 获得指定列头固定列样式
    /// </summary>
    /// <param name="col"></param>
    /// <param name="margin"></param>
    /// <returns></returns>
    protected string? GetFixedCellStyleString(ITableColumn col, int margin = 0)
    {
        string? ret = null;
        if (col.Fixed)
        {
            ret = IsTail(col) ? GetRightStyle(col, margin) : GetLeftStyle(col);
        }
        return ret;
    }

    private string? GetLeftStyle(ITableColumn col)
    {
        var columns = GetVisibleColumns().ToList();
        var defaultWidth = 200;
        var width = 0;
        var start = 0;
        var index = columns.IndexOf(col);
        if (GetFixedDetailRowHeaderColumn)
        {
            width += DetailColumnWidth;
        }
        if (GetFixedMultipleSelectColumn)
        {
            width += MultiColumnWidth;
        }
        if (GetFixedLineNoColumn)
        {
            width += LineNoColumnWidth;
        }
        while (index > start)
        {
            var column = columns[start++];
            width += column.Width ?? defaultWidth;
        }
        return $"left: {width}px;";
    }

    private string? GetRightStyle(ITableColumn col, int margin)
    {
        var columns = GetVisibleColumns().ToList();
        var defaultWidth = 200;
        var width = 0;
        var index = columns.IndexOf(col);

        // after
        while (index + 1 < columns.Count)
        {
            var column = columns[index++];
            width += column.Width ?? defaultWidth;
        }
        if (ShowExtendButtons && FixedExtendButtonsColumn)
        {
            width += ExtendButtonColumnWidth;
        }

        // 如果是固定表头时增加滚动条位置
        if (IsFixedHeader && (index + 1) == columns.Count)
        {
            width += margin;
        }
        return $"right: {width}px;";
    }

    /// <summary>
    /// 获取指定列头样式字符串
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected string? GetHeaderWrapperClassString(ITableColumn col) => CssBuilder.Default("table-cell")
        .AddClass("is-sort", col.GetSortable())
        .AddClass("is-filter", col.GetFilterable())
        .AddClass("is-toolbox", col.ToolboxTemplate != null)
        .AddClass(col.GetAlign().ToDescriptionString(), col.Align == Alignment.Center || col.Align == Alignment.Right)
        .Build();

    /// <summary>
    /// 获得 Cell 文字样式
    /// </summary>
    /// <param name="col"></param>
    /// <param name="hasChildren"></param>
    /// <param name="inCell"></param>
    /// <returns></returns>
    protected string? GetCellClassString(ITableColumn col, bool hasChildren, bool inCell) => CssBuilder.Default("table-cell")
        .AddClass(col.GetAlign().ToDescriptionString(), col.Align == Alignment.Center || col.Align == Alignment.Right)
        .AddClass("is-wrap", col.GetTextWrap())
        .AddClass("is-ellips", col.GetTextEllipsis())
        .AddClass("is-tips", col.GetShowTips())
        .AddClass("is-resizable", AllowResizing)
        .AddClass("is-tree", IsTree && hasChildren)
        .AddClass("is-incell", inCell)
        .AddClass(col.CssClass)
        .Build();

    /// <summary>
    /// 获取指定列头样式字符串
    /// </summary>
    /// <returns></returns>
    protected string? GetIconClassString(string fieldName) => CssBuilder.Default("sort-icon")
        .AddClass(SortIcon, SortName != fieldName || SortOrder == SortOrder.Unset)
        .AddClass(SortIconAsc, SortName == fieldName && SortOrder == SortOrder.Asc)
        .AddClass(SortIconDesc, SortName == fieldName && SortOrder == SortOrder.Desc)
        .Build();

    /// <summary>
    /// 获取指定列头样式字符串
    /// </summary>
    /// <returns></returns>
    protected string? GetColumnToolboxIconClassString() => CssBuilder.Default(ColumnToolboxIcon)
        .Build();

    #region Advanced Sort
    /// <summary>
    /// 获得 高级排序样式
    /// </summary>
    protected string? AdvancedSortClass => CssBuilder.Default("btn btn-secondary")
        .AddClass("btn-info", AdvancedSortItems.Any())
        .Build();

    /// <summary>
    /// 获得/设置 是否显示高级排序按钮 默认 false 不显示 />
    /// </summary>
    [Parameter]
    public bool ShowAdvancedSort { get; set; }

    /// <summary>
    /// 获得/设置 高级排序按钮图标
    /// </summary>
    [Parameter]
    public string? AdvancedSortButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 高级排序框的大小 默认 Medium
    /// </summary>
    [Parameter]
    public Size AdvancedSortDialogSize { get; set; } = Size.Medium;

    /// <summary>
    /// 获得/设置 高级排序框是否可以拖拽 默认 false 不可以拖拽
    /// </summary>
    [Parameter]
    public bool AdvancedSortDialogIsDraggable { get; set; }

    /// <summary>
    /// 获得/设置 高级排序框是否显示最大化按钮 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool AdvancedSortDialogShowMaximizeButton { get; set; }

    /// <summary>
    /// 获得/设置 高级排序，默认为 Empty
    /// </summary>
    [Parameter]
    public List<TableSortItem> AdvancedSortItems { get; set; } = [];

    /// <summary>
    /// 高级排序按钮点击时调用此方法
    /// </summary>
    private async Task ShowSortDialog()
    {
        var result = await DialogService.ShowModal<TableAdvancedSortDialog>(new ResultDialogOption
        {
            Title = AdvancedSortModalTitle,
            Size = AdvancedSortDialogSize,
            IsDraggable = AdvancedSortDialogIsDraggable,
            ShowMaximizeButton = AdvancedSortDialogShowMaximizeButton,
            ComponentParameters = new Dictionary<string, object>
            {
                [nameof(TableAdvancedSortDialog.Value)] = AdvancedSortItems,
                [nameof(TableAdvancedSortDialog.ValueChanged)] = EventCallback.Factory.Create<List<TableSortItem>>(this, v => AdvancedSortItems = v),
                [nameof(TableAdvancedSortDialog.Items)] = Columns.Where(p => p.GetSortable()).Select(p => new SelectedItem(p.GetFieldName(), p.GetDisplayName()))
            }
        });
        if (result == DialogResult.Yes)
        {
            await QueryAsync();
        }
    }

    /// <summary>
    /// 获得 <see cref="AdvancedSortItems"/> 中过滤条件
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<string> GetAdvancedSortList() => ShowAdvancedSort ? AdvancedSortItems.Select(p => p.ToString()) : Enumerable.Empty<string>();
    #endregion
}
