// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 查询条件实体类
/// </summary>
public class QueryPageOptions
{
    /// <summary>
    /// 每页数据数量 默认 20 行
    /// </summary>
    internal const int DefaultPageItems = 20;

    /// <summary>
    /// 获得/设置 查询关键字
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// 获得/设置 通过列集合中的 <see cref="ITableColumn.Searchable"/> 列与 <see cref="SearchText"/> 拼装 IFilterAction 集合
    /// </summary>
    public IEnumerable<IFilterAction> Searchs { get; set; } = Enumerable.Empty<IFilterAction>();

    /// <summary>
    /// 获得/设置 获得 <see cref="Table{TItem}.CustomerSearchModel"/> 中过滤条件 <see cref="Table{TItem}.SearchTemplate"/> 模板中的条件请使用 <see cref="AdvanceSearchs" />获得
    /// </summary>
    public IEnumerable<IFilterAction> CustomerSearchs { get; set; } = Enumerable.Empty<IFilterAction>();

    /// <summary>
    /// 获得/设置 获得 <see cref="Table{TItem}.SearchModel"/> 中过滤条件
    /// </summary>
    public IEnumerable<IFilterAction> AdvanceSearchs { get; set; } = Enumerable.Empty<IFilterAction>();

    /// <summary>
    /// 获得/设置 排序字段名称
    /// </summary>
    public string? SortName { get; set; }

    /// <summary>
    /// 获得/设置 排序方式
    /// </summary>
    public SortOrder SortOrder { get; set; }

    /// <summary>
    /// 获得/设置 多列排序集合 默认为 Empty 内部为 "Name" "Age desc"
    /// </summary>
    public List<string>? SortList { get; set; }

    /// <summary>
    /// 获得/设置 过滤条件集合
    /// </summary>
    public IEnumerable<IFilterAction> Filters { get; set; } = Enumerable.Empty<IFilterAction>();

    /// <summary>
    /// 获得/设置 搜索条件绑定模型
    /// </summary>
    public object? SearchModel { get; set; }

    /// <summary>
    /// 获得/设置 当前页码 首页为 第一页
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 获得/设置 请求读取数据开始行 默认 1
    /// </summary>
    /// <remarks><see cref="Table{TItem}.ScrollMode"/> 开启虚拟滚动时使用</remarks>
    public int StartIndex { get; set; }

    /// <summary>
    /// 获得/设置 每页条目数量
    /// </summary>
    public int PageItems { get; set; } = DefaultPageItems;

    /// <summary>
    /// 获得/设置 是否是分页查询 默认为 false
    /// </summary>
    public bool IsPage { get; set; }
}
