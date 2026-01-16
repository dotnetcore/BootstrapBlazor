// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">查询条件实体类</para>
///  <para lang="en">Query condition entity class</para>
/// </summary>
[JsonConverter(typeof(JsonQueryPageOptionsConverter))]
public class QueryPageOptions
{
    /// <summary>
    ///  <para lang="zh">获得/设置 模糊查询关键字</para>
    ///  <para lang="en">Get/Set search text</para>
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 排序字段名称 由 <see cref="Table{TItem}.SortName"/> 设置</para>
    ///  <para lang="en">Get sort name set by <see cref="Table{TItem}.SortName"/></para>
    /// </summary>
    public string? SortName { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 排序方式 由 <see cref="Table{TItem}.SortOrder"/> 设置</para>
    ///  <para lang="en">Get sort order set by <see cref="Table{TItem}.SortOrder"/></para>
    /// </summary>
    public SortOrder SortOrder { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 多列排序集合 默认为 Empty 内部为 "Name" "Age desc" 由 <see cref="Table{TItem}.SortString"/> 设置</para>
    ///  <para lang="en">Get/Set multi-column sort list default Empty internal "Name" "Age desc" set by <see cref="Table{TItem}.SortString"/></para>
    /// </summary>
    public List<string> SortList { get; } = new(10);

    /// <summary>
    ///  <para lang="zh">获得/设置 自定义多列排序集合 默认为 Empty 内部为 "Name" "Age desc" 由 <see cref="Table{TItem}.AdvancedSortItems"/> 设置</para>
    ///  <para lang="en">Get/Set custom multi-column sort list default Empty internal "Name" "Age desc" set by <see cref="Table{TItem}.AdvancedSortItems"/></para>
    /// </summary>
    public List<string> AdvancedSortList { get; } = new(10);

    /// <summary>
    ///  <para lang="zh">获得 搜索条件绑定模型 未设置 <see cref="Table{TItem}.CustomerSearchModel"/> 时为 <see cref="Table{TItem}"/> 泛型模型</para>
    ///  <para lang="en">Get search model when <see cref="Table{TItem}.CustomerSearchModel"/> is not set, it is <see cref="Table{TItem}"/> generic model</para>
    /// </summary>
    public object? SearchModel { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 当前页码 首页为 第一页</para>
    ///  <para lang="en">Get current page index, first page is 1</para>
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    ///  <para lang="zh">获得 请求读取数据开始行 默认 0</para>
    ///  <para lang="en">Get start index default 0</para>
    /// </summary>
    /// <remarks><para lang="zh"><see cref="Table{TItem}.ScrollMode"/> 开启虚拟滚动 <see cref="ScrollMode.Virtual"/> 时使用</para><para lang="en">Used when <see cref="Table{TItem}.ScrollMode"/> enables virtual scroll <see cref="ScrollMode.Virtual"/></para></remarks>
    public int StartIndex { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 每页条目数量 由 <see cref="Table{TItem}._pageItems"/> 与 <see cref="Table{TItem}.PageItemsSource"/> 设置</para>
    ///  <para lang="en">Get items per page set by <see cref="Table{TItem}._pageItems"/> and <see cref="Table{TItem}.PageItemsSource"/></para>
    /// </summary>
    public int PageItems { get; set; } = 20;

    /// <summary>
    ///  <para lang="zh">获得 是否分页查询模式 默认为 false 由 <see cref="Table{TItem}.IsPagination"/> 设置</para>
    ///  <para lang="en">Get whether is pagination query mode default false set by <see cref="Table{TItem}.IsPagination"/></para>
    /// </summary>
    public bool IsPage { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 是否为虚拟滚动查询模式 默认为 false 由 <see cref="Table{TItem}.ScrollMode"/> 设置</para>
    ///  <para lang="en">Get whether is virtual scroll query mode default false set by <see cref="Table{TItem}.ScrollMode"/></para>
    /// </summary>
    public bool IsVirtualScroll { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 通过列集合中的 <see cref="ITableColumn.Searchable"/> 列与 <see cref="SearchText"/> 拼装 IFilterAction 集合</para>
    ///  <para lang="en">Get IFilterAction collection assembled by <see cref="ITableColumn.Searchable"/> columns in column collection and <see cref="SearchText"/></para>
    /// </summary>
    [Obsolete("This property is obsolete. Use Searches instead. 已过期，请使用 Searches 参数")]
    [ExcludeFromCodeCoverage]
    public List<IFilterAction> Searchs => Searches;

    /// <summary>
    ///  <para lang="zh">获得 通过列集合中的 <see cref="ITableColumn.Searchable"/> 列与 <see cref="SearchText"/> 拼装 IFilterAction 集合</para>
    ///  <para lang="en">Get IFilterAction collection assembled by <see cref="ITableColumn.Searchable"/> columns in column collection and <see cref="SearchText"/></para>
    /// </summary>
    public List<IFilterAction> Searches { get; } = new(20);

    /// <summary>
    ///  <para lang="zh">获得 <see cref="Table{TItem}.CustomerSearchModel"/> 中过滤条件 <see cref="Table{TItem}.SearchTemplate"/> 模板中的条件请使用 <see cref="AdvanceSearches" />获得</para>
    ///  <para lang="en">Gets <see cref="Table{TItem}.CustomerSearchModel"/> 中过滤条件 <see cref="Table{TItem}.SearchTemplate"/> template中的条件请使用 <see cref="AdvanceSearches" />Gets</para>
    /// </summary>
    [Obsolete("This property is obsolete. Use CustomerSearches instead. 已过期，请使用 CustomerSearches 参数")]
    [ExcludeFromCodeCoverage]
    public List<IFilterAction> CustomerSearchs => CustomerSearches;

    /// <summary>
    ///  <para lang="zh">获得 <see cref="Table{TItem}.CustomerSearchModel"/> 中过滤条件 <see cref="Table{TItem}.SearchTemplate"/> 模板中的条件请使用 <see cref="AdvanceSearches" />获得</para>
    ///  <para lang="en">Get filter conditions in <see cref="Table{TItem}.CustomerSearchModel"/> please use <see cref="AdvanceSearches" /> to get conditions in <see cref="Table{TItem}.SearchTemplate"/> template</para>
    /// </summary>
    public List<IFilterAction> CustomerSearches { get; } = new(20);

    /// <summary>
    ///  <para lang="zh">获得 <see cref="Table{TItem}.SearchModel"/> 中过滤条件</para>
    ///  <para lang="en">Get filter conditions in <see cref="Table{TItem}.SearchModel"/></para>
    /// </summary>
    [Obsolete("This property is obsolete. Use AdvanceSearches instead. 已过期，请使用 AdvanceSearches 参数")]
    [ExcludeFromCodeCoverage]
    public List<IFilterAction> AdvanceSearchs => AdvanceSearches;

    /// <summary>
    ///  <para lang="zh">获得 <see cref="Table{TItem}.SearchModel"/> 中过滤条件</para>
    ///  <para lang="en">Get filter conditions in <see cref="Table{TItem}.SearchModel"/></para>
    /// </summary>
    public List<IFilterAction> AdvanceSearches { get; } = new(20);

    /// <summary>
    ///  <para lang="zh">获得 过滤条件集合 等同于 <see cref="Table{TItem}.Filters"/> 值</para>
    ///  <para lang="en">Get filter condition collection equivalent to <see cref="Table{TItem}.Filters"/> value</para>
    /// </summary>
    public List<IFilterAction> Filters { get; } = new(20);

    /// <summary>
    ///  <para lang="zh">获得 是否为首次查询 默认 false</para>
    ///  <para lang="en">Get whether is first query default false</para>
    /// </summary>
    /// <remarks><see cref="Table{TItem}"/> 组件首次查询数据时为 true</remarks>
    [Obsolete("This property is obsolete. Use IsFirstQuery. 已弃用单词拼写错误，请使用 IsFirstQuery")]
    [ExcludeFromCodeCoverage]
    public bool IsFristQuery { get => IsFirstQuery; set => IsFirstQuery = value; }

    /// <summary>
    ///  <para lang="zh">获得 是否为首次查询 默认 false</para>
    ///  <para lang="en">Get whether is first query default false</para>
    /// </summary>
    /// <remarks><see cref="Table{TItem}"/> 组件首次查询数据时为 true</remarks>
    public bool IsFirstQuery { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 是否为刷新分页查询 默认 false</para>
    ///  <para lang="en">Get whether is refresh pagination query default false</para>
    /// </summary>
    public bool IsTriggerByPagination { get; set; }
}
