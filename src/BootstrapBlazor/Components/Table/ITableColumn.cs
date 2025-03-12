// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITableHeader interface
/// </summary>
public interface ITableColumn : IEditorItem
{
    /// <summary>
    /// Gets or sets whether sorting is allowed. Default is null.
    /// </summary>
    bool? Sortable { get; set; }

    /// <summary>
    /// Gets or sets whether it is the default sort column. Default is false.
    /// </summary>
    bool DefaultSort { get; set; }

    /// <summary>
    /// Gets or sets the default sort order. Default is SortOrder.Unset.
    /// </summary>
    SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// Gets or sets whether data filtering is allowed. Default is null.
    /// </summary>
    bool? Filterable { get; set; }

    /// <summary>
    /// Gets or sets whether the column participates in search. Default is null.
    /// </summary>
    bool? Searchable { get; set; }

    /// <summary>
    /// Gets or sets the column width.
    /// </summary>
    int? Width { get; set; }

    /// <summary>
    /// Gets or sets whether the column is fixed. Default is false.
    /// </summary>
    bool Fixed { get; set; }

    /// <summary>
    /// Gets or sets whether text wrapping is allowed in this column. Default is null.
    /// </summary>
    bool? TextWrap { get; set; }

    /// <summary>
    /// Gets or sets whether text overflow is ellipsis in this column. Default is null.
    /// </summary>
    bool? TextEllipsis { get; set; }

    /// <summary>
    /// Gets or sets whether the header text is allowed to wrap. Default is false.
    /// </summary>
    bool HeaderTextWrap { get; set; }

    /// <summary>
    /// Gets or sets whether the header shows a tooltip. Default is false. Can be used with <see cref="HeaderTextEllipsis"/>. This parameter is not effective when <see cref="HeaderTextWrap"/> is set to true.
    /// </summary>
    bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// Gets or sets the header tooltip content.
    /// </summary>
    string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// Gets or sets whether the header text is truncated when overflowing. Default is false. Can be used with <see cref="HeaderTextTooltip"/>. This parameter is not effective when <see cref="HeaderTextWrap"/> is set to true.
    /// </summary>
    bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// Gets or sets the custom CSS class for the column td. Default is null.
    /// </summary>
    string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets the breakpoint at which the column is shown. Default is BreakPoint.None.
    /// </summary>
    BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// Gets or sets whether the column can be copied. Default is null.
    /// </summary>
    bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// Gets or sets the display template.
    /// </summary>
    RenderFragment<object>? Template { get; set; }

    /// <summary>
    /// Gets or sets the search template.
    /// </summary>
    RenderFragment<object>? SearchTemplate { get; set; }

    /// <summary>
    /// Gets or sets the filter template.
    /// </summary>
    RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// Gets or sets the header template.
    /// </summary>
    RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// Gets or sets the toolbox template. Default is null.
    /// </summary>
    RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// Gets or sets the column filter.
    /// </summary>
    IFilter? Filter { get; set; }

    /// <summary>
    /// Gets or sets the format string, such as "yyyy-MM-dd" for date types.
    /// </summary>
    string? FormatString { get; set; }

    /// <summary>
    /// Gets or sets the column format callback delegate <see cref="TableColumnContext{TItem, TValue}"/>.
    /// </summary>
    Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// Gets or sets the text alignment. Default is null, using Alignment.None.
    /// </summary>
    Alignment? Align { get; set; }

    /// <summary>
    /// Gets or sets whether to show tooltips on mouse hover. Default is null, using false value.
    /// </summary>
    bool? ShowTips { get; set; }

    /// <summary>
    /// Gets or sets the custom tooltip content callback delegate. Default is null, using the current value.
    /// </summary>
    Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// Gets or sets the cell render callback method.
    /// </summary>
    Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// Gets or sets whether the column is a MarkupString. Default is false.
    /// </summary>
    bool IsMarkupString { get; set; }

    /// <summary>
    /// Gets or sets whether the column is required when adding a new item. Default is null.
    /// </summary>
    bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// Gets or sets whether the column is required when editing an item. Default is null.
    /// </summary>
    bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// Gets or sets whether the column is read-only when adding a new item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.
    /// </summary>
    bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// Gets or sets whether the column is read-only when editing an item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.
    /// </summary>
    bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// Gets or sets whether the current edit item is visible. Default is null, using true value.
    /// </summary>
    bool? Visible { get; set; }

    /// <summary>
    /// Gets or sets whether the column is visible when adding a new item. Default is null, using the <see cref="Visible"/> value.
    /// </summary>
    bool? IsVisibleWhenAdd { get; set; }

    /// <summary>
    /// Gets or sets whether the column is visible when editing an item. Default is null, using the <see cref="Visible"/> value.
    /// </summary>
    bool? IsVisibleWhenEdit { get; set; }

    /// <summary>
    /// Gets or sets the custom search logic.
    /// </summary>
    Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }
}
