// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ITableHeader interface
///</para>
/// <para lang="en">ITableHeader interface
///</para>
/// </summary>
public interface ITableColumn : IEditorItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否 the current item is ignored when export operation. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether the current item is ignored when export operation. Default is false.
    ///</para>
    /// </summary>
    bool? IgnoreWhenExport { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 sorting is allowed. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether sorting is allowed. Default is null.
    ///</para>
    /// </summary>
    bool? Sortable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 it is the default sort column. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether it is the default sort column. Default is false.
    ///</para>
    /// </summary>
    bool DefaultSort { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the default sort order. 默认为 SortOrder.Unset.
    ///</para>
    /// <para lang="en">Gets or sets the default sort order. Default is SortOrder.Unset.
    ///</para>
    /// </summary>
    SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 数据 filtering is allowed. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether data filtering is allowed. Default is null.
    ///</para>
    /// </summary>
    bool? Filterable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column participates in search. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether the column participates in search. Default is null.
    ///</para>
    /// </summary>
    bool? Searchable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the column 宽度.
    ///</para>
    /// <para lang="en">Gets or sets the column width.
    ///</para>
    /// </summary>
    int? Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is fixed. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is fixed. Default is false.
    ///</para>
    /// </summary>
    bool Fixed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 text wrapping is allowed in this column. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether text wrapping is allowed in this column. Default is null.
    ///</para>
    /// </summary>
    bool? TextWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 text overflow is ellipsis in this column. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether text overflow is ellipsis in this column. Default is null.
    ///</para>
    /// </summary>
    bool? TextEllipsis { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the header text is allowed to wrap. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether the header text is allowed to wrap. Default is false.
    ///</para>
    /// </summary>
    bool HeaderTextWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the header shows a tooltip. 默认为 false. Can be used with <see cref="HeaderTextEllipsis"/>. This parameter is not effective when <see cref="HeaderTextWrap"/> is set to true.
    ///</para>
    /// <para lang="en">Gets or sets whether the header shows a tooltip. Default is false. Can be used with <see cref="HeaderTextEllipsis"/>. This parameter is not effective when <see cref="HeaderTextWrap"/> is set to true.
    ///</para>
    /// </summary>
    bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the header tooltip 内容.
    ///</para>
    /// <para lang="en">Gets or sets the header tooltip content.
    ///</para>
    /// </summary>
    string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the header text is truncated when overflowing. 默认为 false. Can be used with <see cref="HeaderTextTooltip"/>. This parameter is not effective when <see cref="HeaderTextWrap"/> is set to true.
    ///</para>
    /// <para lang="en">Gets or sets whether the header text is truncated when overflowing. Default is false. Can be used with <see cref="HeaderTextTooltip"/>. This parameter is not effective when <see cref="HeaderTextWrap"/> is set to true.
    ///</para>
    /// </summary>
    bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the custom CSS class for the column td. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the custom CSS class for the column td. Default is null.
    ///</para>
    /// </summary>
    string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the breakpoint at which the column is shown. 默认为 BreakPoint.None.
    ///</para>
    /// <para lang="en">Gets or sets the breakpoint at which the column is shown. Default is BreakPoint.None.
    ///</para>
    /// </summary>
    BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column can be copied. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether the column can be copied. Default is null.
    ///</para>
    /// </summary>
    bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 显示 模板.
    ///</para>
    /// <para lang="en">Gets or sets the display template.
    ///</para>
    /// </summary>
    RenderFragment<object>? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the search 模板.
    ///</para>
    /// <para lang="en">Gets or sets the search template.
    ///</para>
    /// </summary>
    RenderFragment<object>? SearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the filter 模板.
    ///</para>
    /// <para lang="en">Gets or sets the filter template.
    ///</para>
    /// </summary>
    RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the header 模板.
    ///</para>
    /// <para lang="en">Gets or sets the header template.
    ///</para>
    /// </summary>
    RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the toolbox 模板. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the toolbox template. Default is null.
    ///</para>
    /// </summary>
    RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the column filter.
    ///</para>
    /// <para lang="en">Gets or sets the column filter.
    ///</para>
    /// </summary>
    IFilter? Filter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the format string, such as "yyyy-MM-dd" for date 类型s.
    ///</para>
    /// <para lang="en">Gets or sets the format string, such as "yyyy-MM-dd" for date types.
    ///</para>
    /// </summary>
    string? FormatString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the column format 回调 委托 <see cref="TableColumnContext{TItem, TValue}"/>.
    ///</para>
    /// <para lang="en">Gets or sets the column format callback delegate <see cref="TableColumnContext{TItem, TValue}"/>.
    ///</para>
    /// </summary>
    Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the text alignment. 默认为 null, using Alignment.None.
    ///</para>
    /// <para lang="en">Gets or sets the text alignment. Default is null, using Alignment.None.
    ///</para>
    /// </summary>
    Alignment? Align { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show tooltips on mouse hover. 默认为 null, using false value.
    ///</para>
    /// <para lang="en">Gets or sets whether to show tooltips on mouse hover. Default is null, using false value.
    ///</para>
    /// </summary>
    bool? ShowTips { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the custom tooltip 内容 回调 委托. 默认为 null, using the current value.
    ///</para>
    /// <para lang="en">Gets or sets the custom tooltip content callback delegate. Default is null, using the current value.
    ///</para>
    /// </summary>
    Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the cell render 回调方法.
    ///</para>
    /// <para lang="en">Gets or sets the cell render callback method.
    ///</para>
    /// </summary>
    Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is a MarkupString. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is a MarkupString. Default is false.
    ///</para>
    /// </summary>
    bool IsMarkupString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is required when adding a new item. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is required when adding a new item. Default is null.
    ///</para>
    /// </summary>
    bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is required when editing an item. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is required when editing an item. Default is null.
    ///</para>
    /// </summary>
    bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is read-only when adding a new item. 默认为 null, using the <see cref="IEditorItem.Readonly"/> value.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is read-only when adding a new item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.
    ///</para>
    /// </summary>
    bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is read-only when editing an item. 默认为 null, using the <see cref="IEditorItem.Readonly"/> value.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is read-only when editing an item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.
    ///</para>
    /// </summary>
    bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the current edit item is visible. 默认为 null, using true value.
    ///</para>
    /// <para lang="en">Gets or sets whether the current edit item is visible. Default is null, using true value.
    ///</para>
    /// </summary>
    bool? Visible { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is visible when adding a new item. 默认为 null, using the <see cref="Visible"/> value.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is visible when adding a new item. Default is null, using the <see cref="Visible"/> value.
    ///</para>
    /// </summary>
    bool? IsVisibleWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the column is visible when editing an item. 默认为 null, using the <see cref="Visible"/> value.
    ///</para>
    /// <para lang="en">Gets or sets whether the column is visible when editing an item. Default is null, using the <see cref="Visible"/> value.
    ///</para>
    /// </summary>
    bool? IsVisibleWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the custom search logic.
    ///</para>
    /// <para lang="en">Gets or sets the custom search logic.
    ///</para>
    /// </summary>
    Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }
}
