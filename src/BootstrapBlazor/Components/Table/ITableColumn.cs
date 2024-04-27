// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ITableHeader 接口
/// </summary>
public interface ITableColumn : IEditorItem
{
    /// <summary>
    /// 获得/设置 是否允许排序 默认为 false
    /// </summary>
    bool Sortable { get; set; }

    /// <summary>
    /// 获得/设置 是否为默认排序列 默认为 false
    /// </summary>
    bool DefaultSort { get; set; }

    /// <summary>
    /// 获得/设置 默认排序规则 默认为 SortOrder.Unset
    /// </summary>
    SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// 获得/设置 是否允许过滤数据 默认为 false
    /// </summary>
    bool Filterable { get; set; }

    /// <summary>
    /// 获得/设置 是否参与搜索 默认为 false
    /// </summary>
    bool Searchable { get; set; }

    /// <summary>
    /// 获得/设置 列宽
    /// </summary>
    int? Width { get; set; }

    /// <summary>
    /// 获得/设置 是否固定本列 默认 false 不固定
    /// </summary>
    bool Fixed { get; set; }

    /// <summary>
    /// 获得/设置 本列是否允许换行 默认为 false
    /// </summary>
    bool TextWrap { get; set; }

    /// <summary>
    /// 获得/设置 本列文本超出省略 默认为 false
    /// </summary>
    bool TextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 是否表头允许折行 默认 false 不折行
    /// </summary>
    bool HeaderTextWrap { get; set; }

    /// <summary>
    /// 获得/设置 是否表头显示 Tooltip 默认 false 不显示 可配合 <see cref="HeaderTextEllipsis"/> 使用 设置 <see cref="HeaderTextWrap"/> 为 true 时本参数不生效
    /// </summary>
    bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// 获得/设置 是否表头 Tooltip 内容
    /// </summary>
    string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// 获得/设置 是否表头溢出时截断 默认 false 不截断 可配合 <see cref="HeaderTextTooltip"/> 使用 设置 <see cref="HeaderTextWrap"/> 为 true 时本参数不生效
    /// </summary>
    bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 列 td 自定义样式 默认为 null 未设置
    /// </summary>
    string? CssClass { get; set; }

    /// <summary>
    /// 显示节点阈值 默认值 BreakPoint.None 未设置
    /// </summary>
    BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// 获得/设置 是否可以拷贝列 默认 false 不可以
    /// </summary>
    bool ShowCopyColumn { get; set; }

    /// <summary>
    /// 获得/设置 显示模板
    /// </summary>
    RenderFragment<object>? Template { get; set; }

    /// <summary>
    /// 获得/设置 搜索模板
    /// </summary>
    RenderFragment<object>? SearchTemplate { get; set; }

    /// <summary>
    /// 获得/设置 过滤模板
    /// </summary>
    RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表头模板
    /// </summary>
    RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 列过滤器
    /// </summary>
    IFilter? Filter { get; set; }

    /// <summary>
    /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
    /// </summary>
    string? FormatString { get; set; }

    /// <summary>
    /// 获得/设置 列格式化回调委托
    /// </summary>
    Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// 获得/设置 文字对齐方式 默认为 Alignment.None
    /// </summary>
    Alignment Align { get; set; }

    /// <summary>
    /// 字段鼠标悬停提示
    /// </summary>
    bool ShowTips { get; set; }

    /// <summary>
    /// 获得/设置 单元格回调方法
    /// </summary>
    Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// 获得/设置 是否为 MarkupString 默认 false
    /// </summary>
    bool IsMarkupString { get; set; }

    /// <summary>
    /// 获得/设置 新建时此列只读 默认为 null 使用 <see cref="IEditorItem.Readonly"/> 值
    /// </summary>
    bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// 获得/设置 编辑时此列只读 默认为 null 使用 <see cref="IEditorItem.Readonly"/> 值
    /// </summary>
    bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑项是否显示 默认为 true
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// 获得/设置 新建时是否此列显示  默认为 null 使用 <see cref="Visible"/> 值
    /// </summary>
    bool? IsVisibleWhenAdd { get; set; }

    /// <summary>
    /// 获得/设置 编辑时是否此列显示  默认为 null 使用 <see cref="Visible"/> 值
    /// </summary>
    bool? IsVisibleWhenEdit { get; set; }
}
