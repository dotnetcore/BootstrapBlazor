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
    /// 获得/设置 模糊查询关键字
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// 获得 排序字段名称 由 <see cref="Table{TItem}.SortName"/> 设置
    /// </summary>
    public string? SortName { get; set; }

    /// <summary>
    /// 获得 排序方式 由 <see cref="Table{TItem}.SortOrder"/> 设置
    /// </summary>
    public SortOrder SortOrder { get; set; }

    /// <summary>
    /// 获得/设置 多列排序集合 默认为 Empty 内部为 "Name" "Age desc" 由 <see cref="Table{TItem}.SortString"/> 设置
    /// </summary>
    public List<string> SortList { get; } = new(10);

    /// <summary>
    /// 获得 搜索条件绑定模型 未设置 <see cref="Table{TItem}.CustomerSearchModel"/> 时为 <see cref="Table{TItem}"/> 泛型模型
    /// </summary>
    public object? SearchModel { get; set; }

    /// <summary>
    /// 获得 当前页码 首页为 第一页
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 获得 请求读取数据开始行 默认 0
    /// </summary>
    /// <remarks><see cref="Table{TItem}.ScrollMode"/> 开启虚拟滚动 <see cref="ScrollMode.Virtual"/> 时使用</remarks>
    public int StartIndex { get; set; }

    /// <summary>
    /// 获得 每页条目数量 由 <see cref="Table{TItem}.PageItems"/> 与 <see cref="Table{TItem}.PageItemsSource"/> 设置
    /// </summary>
    public int PageItems { get; set; } = 20;

    /// <summary>
    /// 获得 是否是分页查询 默认为 false 由 <see cref="Table{TItem}.IsPagination"/> 设置
    /// </summary>
    public bool IsPage { get; set; }

    /// <summary>
    /// 获得 通过列集合中的 <see cref="ITableColumn.Searchable"/> 列与 <see cref="SearchText"/> 拼装 IFilterAction 集合
    /// </summary>
    public List<IFilterAction> Searchs { get; } = new(20);

    /// <summary>
    /// 获得 获得 <see cref="Table{TItem}.CustomerSearchModel"/> 中过滤条件 <see cref="Table{TItem}.SearchTemplate"/> 模板中的条件请使用 <see cref="AdvanceSearchs" />获得
    /// </summary>
    public List<IFilterAction> CustomerSearchs { get; } = new(20);

    /// <summary>
    /// 获得 获得 <see cref="Table{TItem}.SearchModel"/> 中过滤条件
    /// </summary>
    public List<IFilterAction> AdvanceSearchs { get; } = new(20);

    /// <summary>
    /// 获得/设置 过滤条件集合 等同于 <see cref="Table{TItem}.Filters"/> 值
    /// </summary>
    public List<IFilterAction> Filters { get; } = new(20);
}
