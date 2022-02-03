// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

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
    /// 获得/设置 是否为默认排序规则 默认为 SortOrder.Unset
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
    /// 获得/设置 列是否显示 默认为 true 可见的
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// 获得/设置 本列是否允许换行 默认为 false
    /// </summary>
    bool TextWrap { get; set; }

    /// <summary>
    /// 获得/设置 本列文本超出省略 默认为 false
    /// </summary>
    bool TextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 列 td 自定义样式 默认为 null 未设置
    /// </summary>
    string? CssClass { get; set; }

    /// <summary>
    /// 显示节点阈值 默认值 BreakPoint.None 未设置
    /// </summary>
    BreakPoint ShownWithBreakPoint { get; set; }

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
    Func<object?, Task<string>>? Formatter { get; set; }

    /// <summary>
    /// 获得/设置 文字对齐方式 默认为 Alignment.None
    /// </summary>
    Alignment Align { get; set; }

    /// <summary>
    /// 字段鼠标悬停提示
    /// </summary>
    bool ShowTips { get; set; }

    /// <summary>
    /// 获得/设置 顺序号
    /// </summary>
    int Order { get; set; }

    /// <summary>
    /// 获得/设置 单元格回调方法
    /// </summary>
    Action<TableCellArgs>? OnCellRender { get; set; }
}
