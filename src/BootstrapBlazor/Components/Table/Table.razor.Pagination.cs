// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否分页，默认值为 false</para>
    /// <para lang="en">Gets or sets Whether to allow pagination. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsPagination { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分页上下翻页的页码数量，默认为 5</para>
    /// <para lang="en">Gets or sets Page up/down page link count. Default 5</para>
    /// </summary>
    [Parameter]
    public int MaxPageLinkCount { get; set; } = 5;

    /// <summary>
    /// <para lang="zh">获得/设置 是否在顶端显示分页，默认值为 false</para>
    /// <para lang="en">Gets or sets Whether to show pagination at top. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowTopPagination { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示行号列 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to show line number column. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowLineNo { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 行号列标题文字 默认为 行号</para>
    /// <para lang="en">Gets or sets Line Number Column Header Text. Default "Line No"</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LineNoText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 每页显示数据数量的外部数据源</para>
    /// <para lang="en">Gets or sets External data source for items per page</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<int>? PageItemsSource { get; set; }

    /// <summary>
    /// <para lang="zh">异步查询回调方法，设置 <see cref="Items"/> 后无法触发此回调方法</para>
    /// <para lang="en">Async query callback method. Cannot trigger this when <see cref="Items"/> is set</para>
    /// </summary>
    [Parameter]
    public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据总条目</para>
    /// <para lang="en">Gets or sets Total items count</para>
    /// </summary>
    protected int TotalCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分页页码总数 内置规则 PageCount > 1 时显示分页组件</para>
    /// <para lang="en">Gets or sets Total page count. Internal rule: show pagination when PageCount > 1</para>
    /// </summary>
    protected int PageCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前页码 默认 1</para>
    /// <para lang="en">Gets or sets Current page index. Default 1</para>
    /// </summary>
    protected int PageIndex { get; set; } = 1;

    /// <summary>
    /// <para lang="zh">获得/设置 默认每页数据数量 默认 null 使用 <see cref="PageItemsSource"/> 第一个值</para>
    /// <para lang="en">Gets or sets Default items per page. Default null (Use first value of <see cref="PageItemsSource"/>)</para>
    /// </summary>
    [Parameter]
    public int? PageItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Goto 跳转导航</para>
    /// <para lang="en">Gets or sets Whether to show Goto navigator</para>
    /// </summary>
    [Parameter]
    public bool ShowGotoNavigator { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Goto 跳转导航文本信息 默认 null</para>
    /// <para lang="en">Gets or sets Whether to show Goto navigator label text. Default null</para>
    /// </summary>
    [Parameter]
    public string? GotoNavigatorLabelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Goto 导航模板</para>
    /// <para lang="en">Gets or sets Goto navigator template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? GotoTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 PageInfo 内容 默认 true 显示</para>
    /// <para lang="en">Gets or sets Whether to show PageInfo. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowPageInfo { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 分页信息文字 默认 null</para>
    /// <para lang="en">Gets or sets Page Info Text. Default null</para>
    /// </summary>
    [Parameter]
    public string? PageInfoText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分页信息模板</para>
    /// <para lang="en">Gets or sets Page Info Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? PageInfoTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分页信息内容模板 默认 null</para>
    /// <para lang="en">Gets or sets Page Info Body Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? PageInfoBodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前行</para>
    /// <para lang="en">Gets or sets Start Index</para>
    /// </summary>
    protected int StartIndex { get; set; }

    /// <summary>
    /// <para lang="zh">当前每页项目数量设置，默认值为 null，内部使用</para>
    /// <para lang="en">Current items per page setting. Default null (Internal Use)</para>
    /// </summary>
    private int _pageItems;

    private int? _originPageItems;

    /// <summary>
    /// <para lang="zh">内部 分页信息模板</para>
    /// <para lang="en">Internal Page Info Template</para>
    /// </summary>
    protected RenderFragment InternalPageInfoTemplate => builder =>
    {
        if (PageInfoTemplate != null)
        {
            builder.AddContent(0, PageInfoTemplate);
        }
        else if (!string.IsNullOrEmpty(PageInfoText))
        {
            builder.OpenElement(1, "div");
            builder.AddAttribute(2, "class", "page-info");
            builder.AddContent(3, PageInfoText);
            builder.CloseElement();
        }
        else
        {
            builder.AddContent(4, RenderPageInfo);
        }
    };

    private bool _shouldScrollTop = false;
    /// <summary>
    /// <para lang="zh">点击页码调用此方法</para>
    /// <para lang="en">Page Link Click Callback</para>
    /// </summary>
    /// <param name="pageIndex"></param>
    protected async Task OnPageLinkClick(int pageIndex)
    {
        if (pageIndex != PageIndex)
        {
            PageIndex = pageIndex;

            if (!IsKeepSelectedRows)
            {
                SelectedRows.Clear();
            }

            await QueryAsync(false, triggerByPagination: true);
            await OnSelectedRowsChanged();

            if (IsAutoScrollTopWhenClickPage)
            {
                _shouldScrollTop = true;
            }
        }
    }

    /// <summary>
    /// <para lang="zh">每页记录条数变化时调用此方法</para>
    /// <para lang="en">Page Items Value Changed Callback</para>
    /// </summary>
    protected async Task OnPageItemsValueChanged(int pageItems)
    {
        if (_pageItems != pageItems)
        {
            PageIndex = 1;
            _pageItems = pageItems;
            await QueryAsync();
        }
    }

    private List<SelectedItem>? _pageItemsSource;

    /// <summary>
    /// <para lang="zh">获得 分页数据源</para>
    /// <para lang="en">Get Page Items Source</para>
    /// </summary>
    protected List<SelectedItem> GetPageItemsSource()
    {
        _pageItemsSource ??= PageItemsSource.Select(i => new SelectedItem($"{i}", Localizer["PageItemsText", i].Value)).ToList();
        return _pageItemsSource;
    }
}
