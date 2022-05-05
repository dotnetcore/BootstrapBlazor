// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

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
    /// 获得/设置 升序图标
    /// </summary>
    [Parameter]
    public string SortIconAsc { get; set; } = "fa fa-sort-asc";

    /// <summary>
    /// 获得/设置 降序图标
    /// </summary>
    [Parameter]
    public string SortIconDesc { get; set; } = "fa fa-sort-desc";

    /// <summary>
    /// 获得/设置 默认图标
    /// </summary>
    [Parameter]
    public string SortIcon { get; set; } = "fa fa-sort";

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
    protected Func<string, SortOrder, Task>? IntenralOnSortAsync { get; set; }

    /// <summary>
    /// 点击列进行排序方法
    /// </summary>
    protected Func<Task> OnClickHeader(ITableColumn col) => async () =>
    {
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

        // 通知 Table 组件刷新数据
        if (IntenralOnSortAsync != null)
        {
            await IntenralOnSortAsync(SortName, SortOrder);
        }
    };

    /// <summary>
    /// 获取指定列头样式字符串
    /// </summary>
    /// <param name="col"></param>
    /// <param name="isFilterHeader"></param>
    /// <returns></returns>
    protected string? GetHeaderClassString(ITableColumn col, bool isFilterHeader = false) => CssBuilder.Default()
        .AddClass("sortable", col.Sortable && !isFilterHeader)
        .AddClass("filterable", col.Filterable)
        .AddClass(GetFixedCellClassString(col))
        .Build();

    /// <summary>
    /// 获得指定列头固定列样式
    /// </summary>
    /// <param name="col"></param>
    /// <param name="cellClass"></param>
    /// <returns></returns>
    protected string? GetFixedCellClassString(ITableColumn col, string? cellClass = null) => CssBuilder.Default(cellClass)
        .AddClass("fixed", col.Fixed)
        .AddClass("fixed-right", col.Fixed && IsTail(col))
        .Build();

    /// <summary>
    /// 获得扩展按钮列固定列样式
    /// </summary>
    /// <returns></returns>
    protected string? FixedExtendButtonsColumnClassString => CssBuilder.Default()
        .AddClass("fixed", FixedExtendButtonsColumn)
        .AddClass("fixed-right", !IsExtendButtonsInRowHeader)
        .Build();

    /// <summary>
    /// 获得 按钮列样式表集合
    /// </summary>
    /// <returns></returns>
    protected string? ExtendButtonsColumnClass => CssBuilder.Default("table-th-button")
        .AddClass("fixed", FixedExtendButtonsColumn)
        .AddClass("fixed-right", !IsExtendButtonsInRowHeader)
        .Build();

    /// <summary>
    /// 获得扩展按钮列固定列样式
    /// </summary>
    /// <returns></returns>
    protected string? GetFixedExtendButtonsColumnStyleString(int margin = 0) => CssBuilder.Default()
        .AddClass($"right: {(IsFixedHeader ? margin : 0)}px;", FixedExtendButtonsColumn && !IsExtendButtonsInRowHeader)
        .AddClass("left: 0px;", FixedExtendButtonsColumn && IsExtendButtonsInRowHeader)
        .Build();

    private int CalcMargin(int margin)
    {
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
        var middle = Math.Ceiling(Columns.Count * 1.0 / 2);
        var index = Columns.IndexOf(col);
        return middle < index;
    }

    /// <summary>
    /// 获得列单元格 Style 用于设置文本超长溢出
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected string? GetCellStyleString(ITableColumn col) => col.TextEllipsis && !AllowResizing ? $"width: {col.Width ?? 200}px" : null;

    /// <summary>
    /// 获得指定列头固定列样式
    /// </summary>
    /// <param name="col"></param>
    /// <param name="margin"></param>
    /// <returns></returns>
    protected string? GetFixedCellStyleString(ITableColumn col, int margin = 0)
    {
        var style = CssBuilder.Default();
        if (col.Fixed)
        {
            var defaultWidth = 200;
            var isTail = IsTail(col);
            var index = Columns.IndexOf(col);
            var width = 0;
            var start = 0;
            if (isTail)
            {
                // after
                while (index + 1 < Columns.Count)
                {
                    width += Columns[index++].Width ?? defaultWidth;
                }
                if (ShowExtendButtons && FixedExtendButtonsColumn)
                {
                    width += ExtendButtonColumnWidth;
                }

                // 如果是固定表头时增加滚动条位置
                if (IsFixedHeader && (index + 1) == Columns.Count)
                {
                    width += margin;
                }

                style.AddClass($"right: {width}px;");
            }
            else
            {
                while (index > start)
                {
                    width += Columns[start++].Width ?? defaultWidth;
                };
                style.AddClass($"left: {width}px;");
            }
        }
        return style.Build();
    }

    /// <summary>
    /// 获取指定列头样式字符串
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected string? GetHeaderWrapperClassString(ITableColumn col) => CssBuilder.Default("table-cell")
        .AddClass("is-sort", col.Sortable)
        .AddClass("is-filter", col.Filterable)
        .Build();

    /// <summary>
    /// 获得 Header 中表头文字样式
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    protected string? GetHeaderTextClassString(ITableColumn col) => CssBuilder.Default("table-text")
        .AddClass("text-start", col.Align == Alignment.Left)
        .AddClass("text-end", col.Align == Alignment.Right)
        .AddClass("text-center", col.Align == Alignment.Center)
        .Build();

    /// <summary>
    /// 获得 Cell 文字样式
    /// </summary>
    /// <param name="col"></param>
    /// <param name="hasChildren"></param>
    /// <param name="inCell"></param>
    /// <returns></returns>
    protected string? GetCellClassString(ITableColumn col, bool hasChildren, bool inCell) => CssBuilder.Default("table-cell")
        .AddClass("text-star", col.Align == Alignment.Left)
        .AddClass("text-end", col.Align == Alignment.Right)
        .AddClass("text-center", col.Align == Alignment.Center)
        .AddClass("is-wrap", col.TextWrap)
        .AddClass("is-ellips", col.TextEllipsis)
        .AddClass("is-tips", col.ShowTips)
        .AddClass("is-resizable", AllowResizing)
        .AddClass("is-tree", IsTree && hasChildren)
        .AddClass("is-incell", inCell)
        .AddClass(col.CssClass)
        .Build();

    /// <summary>
    /// 获取指定列头样式字符串
    /// </summary>
    /// <returns></returns>
    protected string? GetIconClassString(string fieldName)
    {
        var order = SortName == fieldName ? SortOrder : SortOrder.Unset;
        return order switch
        {
            SortOrder.Asc => SortIconAsc,
            SortOrder.Desc => SortIconDesc,
            _ => SortIcon
        };
    }
}
