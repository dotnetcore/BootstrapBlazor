// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ITableColumn 接口</para>
/// <para lang="en">ITableColumn interface</para>
/// </summary>
public interface ITableColumn : IEditorItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 导出时是否忽略当前项，默认为 false</para>
    /// <para lang="en">Gets or sets whether to ignore current item when exporting. Default is false.</para>
    /// </summary>
    bool? IgnoreWhenExport { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许排序，默认为 null</para>
    /// <para lang="en">Gets or sets whether sorting is allowed. Default is null.</para>
    /// </summary>
    bool? Sortable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为默认排序列，默认为 false</para>
    /// <para lang="en">Gets or sets whether it is the default sort column. Default is false.</para>
    /// </summary>
    bool DefaultSort { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 默认排序顺序，默认为 SortOrder.Unset</para>
    /// <para lang="en">Gets or sets the default sort order. Default is SortOrder.Unset.</para>
    /// </summary>
    SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许数据过滤，默认为 null</para>
    /// <para lang="en">Gets or sets whether data filtering is allowed. Default is null.</para>
    /// </summary>
    bool? Filterable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许在搜索中参与，默认为 null</para>
    /// <para lang="en">Gets or sets whether the column participates in search. Default is null.</para>
    /// </summary>
    bool? Searchable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列宽</para>
    /// <para lang="en">Gets or sets the column width.</para>
    /// </summary>
    int? Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否列固定，默认为 false</para>
    /// <para lang="en">Gets or sets whether the column is fixed. Default is false.</para>
    /// </summary>
    bool Fixed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许此列中文本换行，默认为 null</para>
    /// <para lang="en">Gets or sets whether text wrapping is allowed in this column. Default is null.</para>
    /// </summary>
    bool? TextWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否此列中文本溢出省略号，默认为 null</para>
    /// <para lang="en">Gets or sets whether text overflow is ellipsis in this column. Default is null.</para>
    /// </summary>
    bool? TextEllipsis { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许表头文本换行，默认为 false</para>
    /// <para lang="en">Gets or sets whether the header text is allowed to wrap. Default is false.</para>
    /// </summary>
    bool HeaderTextWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示表头提示，默认为 false，可配合 HeaderTextEllipsis 使用，当 HeaderTextWrap 为 true 时此参数无效</para>
    /// <para lang="en">Gets or sets whether the header shows a tooltip. Default is false. Can be used with HeaderTextEllipsis. This parameter is not effective when HeaderTextWrap is true.</para>
    /// </summary>
    bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表头提示内容</para>
    /// <para lang="en">Gets or sets the header tooltip content.</para>
    /// </summary>
    string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否表头文本溢出时省略，默认为 false，可配合 HeaderTextTooltip 使用，当 HeaderTextWrap 为 true 时此参数无效</para>
    /// <para lang="en">Gets or sets whether the header text is truncated when overflowing. Default is false. Can be used with HeaderTextTooltip. This parameter is not effective when HeaderTextWrap is true.</para>
    /// </summary>
    bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列 td 的自定义 CSS 类，默认为 null</para>
    /// <para lang="en">Gets or sets the custom CSS class for the column td. Default is null.</para>
    /// </summary>
    string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示列的断点，默认为 BreakPoint.None</para>
    /// <para lang="en">Gets or sets the breakpoint at which the column is shown. Default is BreakPoint.None.</para>
    /// </summary>
    BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可以复制列，默认为 null</para>
    /// <para lang="en">Gets or sets whether the column can be copied. Default is null.</para>
    /// </summary>
    bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示模板</para>
    /// <para lang="en">Gets or sets the display template.</para>
    /// </summary>
    RenderFragment<object>? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索模板</para>
    /// <para lang="en">Gets or sets the search template.</para>
    /// </summary>
    RenderFragment<object>? SearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤模板</para>
    /// <para lang="en">Gets or sets the filter template.</para>
    /// </summary>
    RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表头模板</para>
    /// <para lang="en">Gets or sets the header template.</para>
    /// </summary>
    RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 工具栏模板，默认为 null</para>
    /// <para lang="en">Gets or sets the toolbox template. Default is null.</para>
    /// </summary>
    RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列过滤器</para>
    /// <para lang="en">Gets or sets the column filter.</para>
    /// </summary>
    IFilter? Filter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 格式字符串，例如日期类型的 "yyyy-MM-dd"</para>
    /// <para lang="en">Gets or sets the format string, such as "yyyy-MM-dd" for date types.</para>
    /// </summary>
    string? FormatString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列格式回调委托</para>
    /// <para lang="en">Gets or sets the column format callback delegate.</para>
    /// </summary>
    Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文本对齐方式，默认为 null，使用 Alignment.None</para>
    /// <para lang="en">Gets or sets the text alignment. Default is null, using Alignment.None.</para>
    /// </summary>
    Alignment? Align { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 鼠标悬停时是否显示提示，默认为 null，使用 false 值</para>
    /// <para lang="en">Gets or sets whether to show tooltips on mouse hover. Default is null, using false value.</para>
    /// </summary>
    bool? ShowTips { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义提示内容回调委托，默认为 null，使用当前值</para>
    /// <para lang="en">Gets or sets the custom tooltip content callback delegate. Default is null, using the current value.</para>
    /// </summary>
    Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 单元格渲染回调方法</para>
    /// <para lang="en">Gets or sets the cell render callback method.</para>
    /// </summary>
    Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列是否为 MarkupString，默认为 false</para>
    /// <para lang="en">Gets or sets whether the column is a MarkupString. Default is false.</para>
    /// </summary>
    bool IsMarkupString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 添加新项时列是否必需，默认为 null</para>
    /// <para lang="en">Gets or sets whether the column is required when adding a new item. Default is null.</para>
    /// </summary>
    bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑项时列是否必需，默认为 null</para>
    /// <para lang="en">Gets or sets whether the column is required when editing an item. Default is null.</para>
    /// </summary>
    bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 添加新项时列是否为只读，默认为 null，使用 IEditorItem.Readonly 值</para>
    /// <para lang="en">Gets or sets whether the column is read-only when adding a new item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.</para>
    /// </summary>
    bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑项时列是否为只读，默认为 null，使用 IEditorItem.Readonly 值</para>
    /// <para lang="en">Gets or sets whether the column is read-only when editing an item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.</para>
    /// </summary>
    bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前编辑项是否可见，默认为 null，使用 true 值</para>
    /// <para lang="en">Gets or sets whether the current edit item is visible. Default is null, using true value.</para>
    /// </summary>
    bool? Visible { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 添加新项时列是否可见，默认为 null，使用 Visible 值</para>
    /// <para lang="en">Gets or sets whether the column is visible when adding a new item. Default is null, using the <see cref="Visible"/> value.</para>
    /// </summary>
    bool? IsVisibleWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑项时列是否可见，默认为 null，使用 Visible 值</para>
    /// <para lang="en">Gets or sets whether the column is visible when editing an item. Default is null, using the <see cref="Visible"/> value.</para>
    /// </summary>
    bool? IsVisibleWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义搜索逻辑</para>
    /// <para lang="en">Gets or sets the custom search logic.</para>
    /// </summary>
    Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }
}
