// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ListView 组件基类</para>
/// <para lang="en">ListView Component Base</para>
/// </summary>
public partial class ListView<TItem> : BootstrapComponentBase
{
    private string? ClassString => CssBuilder.Default("listview")
        .AddClass("is-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? BodyClassString => CssBuilder.Default("listview-body scroll")
        .AddClass("is-group", GroupName != null)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 CardHeader</para>
    /// <para lang="en">Gets or sets Card Header</para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 获得 <see cref="CollapseItem.Text"/> 值 默认 null 使用分组 Key.ToString() 方法获取</para>
    /// <para lang="en">Gets or sets Get <see cref="CollapseItem.Text"/> value. Default null. Use Group Key.ToString() method to get</para>
    /// </summary>
    [Parameter]
    public Func<object?, string?>? GroupHeaderTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组排序回调方法 默认 null 使用内置</para>
    /// <para lang="en">Gets or sets Group sort callback method. Default null. Use built-in</para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<IGrouping<object?, TItem>>, IOrderedEnumerable<IGrouping<object?, TItem>>>? GroupOrderCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组内项目排序回调方法 默认 null</para>
    /// <para lang="en">Gets or sets Group item sort callback method. Default null</para>
    /// </summary>
    [Parameter]
    public Func<IGrouping<object?, TItem>, IOrderedEnumerable<TItem>>? GroupItemOrderCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 BodyTemplate</para>
    /// <para lang="en">Gets or sets Body Template</para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment<TItem>? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 FooterTemplate 默认 null 未设置 设置值后 <see cref="IsPagination"/> 参数不起作用，请自行实现分页功能</para>
    /// <para lang="en">Gets or sets Footer Template. Default null. If set, <see cref="IsPagination"/> parameter will not work, please implement pagination manually</para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据源</para>
    /// <para lang="en">Gets or sets Data Source</para>
    /// </summary>
    [Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否分页 默认为 false 不分页 设置 <see cref="FooterTemplate"/> 时分页功能自动被禁用</para>
    /// <para lang="en">Gets or sets Whether to page. Default false. Paging is automatically disabled when <see cref="FooterTemplate"/> is set</para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 IsPagination 代替。Deprecated, use IsPagination instead")]
    [ExcludeFromCodeCoverage]
    public bool Pageable { get => IsPagination; set => IsPagination = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否分页 默认为 false 不分页 设置 <see cref="FooterTemplate"/> 时分页功能自动被禁用</para>
    /// <para lang="en">Gets or sets Whether to page. Default false. Paging is automatically disabled when <see cref="FooterTemplate"/> is set</para>
    /// </summary>
    [Parameter]
    public bool IsPagination { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组 Lambda 表达式 默认 null</para>
    /// <para lang="en">Gets or sets Grouping Lambda Expression. Default null</para>
    /// </summary>
    [Parameter]
    public Func<TItem, object?>? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可折叠 默认 false 需要开启分组设置 <see cref="GroupName"/></para>
    /// <para lang="en">Gets or sets Whether it is collapsible. Default false. Need to enable grouping setting <see cref="GroupName"/></para>
    /// </summary>
    [Parameter]
    public bool Collapsible { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否手风琴效果 默认 false 需要开启可收缩设置 <see cref="Collapsible"/></para>
    /// <para lang="en">Gets or sets Accordion effect. Default false. Need to enable collapsible setting <see cref="Collapsible"/></para>
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 CollapseItem 展开收缩时回调方法 默认 false 需要开启可收缩设置 <see cref="Collapsible"/></para>
    /// <para lang="en">Gets or sets Callback method when CollapseItem is expanded/collapsed. Default false. Need to enable collapsible setting <see cref="Collapsible"/></para>
    /// </summary>
    [Parameter]
    public Func<CollapseItem, Task>? OnCollapseChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 首次渲染是否收缩回调委托</para>
    /// <para lang="en">Gets or sets Callback delegate for whether to collapse on first render</para>
    /// </summary>
    [Parameter]
    public Func<object?, bool>? CollapsedGroupCallback { get; set; }

    /// <summary>
    /// <para lang="zh">异步查询回调方法</para>
    /// <para lang="en">Async query callback method</para>
    /// </summary>
    [Parameter]
    public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ListView组件元素点击时回调委托</para>
    /// <para lang="en">Gets or sets Callback delegate when ListView component element is clicked</para>
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnListViewItemClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为竖向排列 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to arrange vertically. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 每页数据数量 默认 20</para>
    /// <para lang="en">Gets or sets Number of items per page. Default 20</para>
    /// </summary>
    [Parameter]
    public int PageItems { get; set; } = 20;

    /// <summary>
    /// <para lang="zh">获得/设置 组件高度 默认 null 未设置高度 如：50% 100px 10rem 10vh 等</para>
    /// <para lang="en">Gets or sets Component height. Default null. Not set. e.g. 50% 100px 10rem 10vh etc.</para>
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 无数据时模板 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Template when no data. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 无数据时显示文字 默认 null 未设置使用资源文件设置文字</para>
    /// <para lang="en">Gets or sets Text to display when no data. Default null. Use resource file to set text if not set</para>
    /// </summary>
    [Parameter]
    public string? EmptyText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前页码</para>
    /// <para lang="en">Gets or sets Current Page Index</para>
    /// </summary>
    private int _pageIndex = 1;

    /// <summary>
    /// <para lang="zh">获得/设置 数据总条目</para>
    /// <para lang="en">Gets or sets Total items</para>
    /// </summary>
    private int _totalCount;

    /// <summary>
    /// <para lang="zh">数据集合内部使用</para>
    /// <para lang="en">Data collection internal use</para>
    /// </summary>
    private List<TItem> Rows => Items?.ToList() ?? [];

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height};", !string.IsNullOrEmpty(Height))
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        if (Items == null)
        {
            await QueryData();
        }
    }

    private bool IsCollapsed(int index, object? groupKey) => CollapsedGroupCallback?.Invoke(groupKey) ?? index > 0;

    /// <summary>
    /// <para lang="zh">点击页码调用此方法</para>
    /// <para lang="en">Call this method when page link is clicked</para>
    /// </summary>
    /// <param name="pageIndex"></param>
    protected Task OnPageLinkClick(int pageIndex) => QueryAsync(pageIndex, true);

    /// <summary>
    /// <para lang="zh">查询按钮调用此方法</para>
    /// <para lang="en">Call this method when query button is clicked</para>
    /// </summary>
    public async Task QueryAsync(int pageIndex = 1, bool triggerByPagination = false)
    {
        _pageIndex = pageIndex;
        await QueryData(triggerByPagination);
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">调用 OnQuery 回调方法获得数据源</para>
    /// <para lang="en">Call OnQuery callback method to get data source</para>
    /// </summary>
    protected async Task QueryData(bool triggerByPagination = false)
    {
        QueryData<TItem>? queryData = null;
        if (OnQueryAsync != null)
        {
            queryData = await OnQueryAsync(new QueryPageOptions()
            {
                IsPage = IsPagination,
                IsTriggerByPagination = triggerByPagination,
                PageIndex = _pageIndex,
                PageItems = PageItems,
            });
        }
        if (queryData != null)
        {
            Items = queryData.Items;
            _totalCount = queryData.TotalCount;
        }
    }

    private int PageCount => (int)Math.Ceiling(_totalCount * 1.0 / PageItems);

    /// <summary>
    /// <para lang="zh">点击元素事件</para>
    /// <para lang="en">Click Element Event</para>
    /// </summary>
    /// <param name="item"></param>
    protected async Task OnClick(TItem item)
    {
        if (OnListViewItemClick != null)
        {
            await OnListViewItemClick(item);
        }
    }

    private RenderFragment RenderCollapsibleItems(Func<TItem, object?> groupFunc) => builder =>
    {
        var index = 0;
        foreach (var key in GetGroupItems(groupFunc))
        {
            var i = index++;
            builder.AddContent(i, RenderGroupItem(key, i));
        }
    };

    private IEnumerable<(object? GroupName, IOrderedEnumerable<TItem> Items)> GetGroupItems(Func<TItem, object?> groupFunc)
    {
        var groupItems = Rows.GroupBy(groupFunc);
        var groupOrderItems = GroupOrderCallback == null ? groupItems.OrderBy(i => i.Key) : GroupOrderCallback(groupItems);
        return GroupItemOrderCallback == null
            ? groupOrderItems.Select(i => (i.Key, i.OrderBy(g => i.Key)))
            : groupOrderItems.Select(i => (i.Key, GroupItemOrderCallback(i)));
    }

    private string? GetGroupName(object? key) => GroupHeaderTextCallback?.Invoke(key) ?? key?.ToString();
}
