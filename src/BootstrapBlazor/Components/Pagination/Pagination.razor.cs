// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Pagination
{
    /// <summary>
    /// 获得/设置 页码总数
    /// </summary>
    public int PageCount => Math.Max(1, (int)Math.Ceiling(TotalCount * 1.0 / PageItems));

    /// <summary>
    /// 获得 Pagination 样式
    /// </summary>
    /// <returns></returns>
    protected string? PaginationClass => CssBuilder.Default("pagination")
        .AddClass("d-none", PageCount == 1)
        .Build();

    /// <summary>
    /// 获得 PaginationBar 样式
    /// </summary>
    /// <returns></returns>
    protected string? PaginationBarClass => CssBuilder.Default("pagination-bar")
        .AddClass("d-none", !ShowPaginationInfo)
        .Build();

    /// <summary>
    /// 获得 PaginationItem 样式
    /// </summary>
    /// <param name="active"></param>
    /// <returns></returns>
    protected static string? GetPaginationItemClassName(bool active) => CssBuilder.Default("page-item")
        .AddClass("active", active)
        .Build();

    /// <summary>
    /// 获得 起始记录索引
    /// </summary>
    protected int StarIndex => (PageIndex - 1) * PageItems + 1;

    /// <summary>
    /// 获得 结尾记录索引
    /// </summary>
    protected long EndIndex => Math.Min(PageIndex * PageItems, TotalCount);

    /// <summary>
    /// 获得/设置 开始页码
    /// </summary>
    protected int StartPageIndex => Math.Max(1, Math.Min(PageCount - 4, Math.Max(1, PageIndex - 2)));

    /// <summary>
    /// 获得/设置 结束页码
    /// </summary>
    protected int EndPageIndex => Math.Min(PageCount, Math.Max(5, PageIndex + 3));

    /// <summary>
    /// 获得/设置 数据总数
    /// </summary>
    [Parameter]
    public int TotalCount { get; set; }

    /// <summary>
    /// 获得/设置 当前页码
    /// </summary>
    [Parameter]
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 获得/设置 每页显示数据数量
    /// </summary>
    [Parameter]
    public int PageItems { get; set; }

    /// <summary>
    /// 获得/设置 是否显示分页数据汇总信息 默认为 true 显示
    /// </summary>
    /// <value></value>
    [Parameter] public bool ShowPaginationInfo { get; set; } = true;

    /// <summary>
    /// 获得/设置 每页显示数据数量的外部数据源
    /// </summary>
    /// <value></value>
    [Parameter]
    public IEnumerable<int> PageItemsSource { get; set; } = new int[] { 20, 50, 100, 200, 500, 1000 };

    /// <summary>
    /// 点击页码时回调方法 第一个参数是当前页码，第二个参数是当前每页设置显示的数据项数量
    /// </summary>
    [Parameter]
    public Func<int, int, Task>? OnPageClick { get; set; }

    /// <summary>
    /// 点击设置每页显示数据数量时回调方法 参数是当前每页设置显示的数据项数量
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnPageItemsChanged { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Pagination>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 AiraPageLabel 显示文字 默认为 分页组件
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraPageLabel { get; set; }

    /// <summary>
    /// 获得/设置 AiraPrevPageText 显示文字 默认为 上一页
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraPrevPageText { get; set; }

    /// <summary>
    /// 获得/设置 AiraFirstPageText 显示文字 默认为 第一页
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraFirstPageText { get; set; }

    /// <summary>
    /// 获得/设置 AiraNextPageText 显示文字 默认为 下一页
    /// </summary>
    [Parameter]
    [NotNull]
    public string? AiraNextPageText { get; set; }

    /// <summary>
    /// 获得/设置 PrePageInfoText 显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PrePageInfoText { get; set; }

    /// <summary>
    /// 获得/设置 RowInfoText 显示文字 默认为 条记录
    /// </summary>
    [Parameter]
    [NotNull]
    public string? RowInfoText { get; set; }

    /// <summary>
    /// 获得/设置 PageInfoText 显示文字 默认为 显示第 {0} 到第 {1} 条记录
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PageInfoText { get; set; }

    /// <summary>
    /// 获得/设置 AiraPageLabel 显示文字 默认为 ，总共 {0} 条记录
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TotalInfoText { get; set; }

    /// <summary>
    /// 获得/设置 SelectItemsText 显示文字 默认为 {0} 条/页
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SelectItemsText { get; set; }

    /// <summary>
    /// 获得/设置 LabelString 显示文字 默认为 第 {0} 页
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LabelString { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AiraPageLabel ??= Localizer[nameof(AiraPageLabel)];
        AiraPrevPageText ??= Localizer[nameof(AiraPrevPageText)];
        AiraFirstPageText ??= Localizer[nameof(AiraFirstPageText)];
        AiraNextPageText ??= Localizer[nameof(AiraNextPageText)];
        PrePageInfoText ??= Localizer[nameof(PrePageInfoText)];
        RowInfoText ??= Localizer[nameof(RowInfoText)];
        PageInfoText ??= Localizer[nameof(PageInfoText)];
        TotalInfoText ??= Localizer[nameof(TotalInfoText)];
        SelectItemsText ??= Localizer[nameof(SelectItemsText)];
        LabelString ??= Localizer[nameof(LabelString)];
    }

    /// <summary>
    /// 上一页方法
    /// </summary>
    protected async Task MovePrev(int index)
    {
        var pageIndex = PageIndex > 1 ? Math.Max(1, PageIndex - index) : PageCount;
        await OnPageItemClick(pageIndex);
    }

    /// <summary>
    /// 下一页方法
    /// </summary>
    protected async Task MoveNext(int index)
    {
        var pageIndex = PageIndex < PageCount ? Math.Min(PageCount, PageIndex + index) : 1;
        await OnPageItemClick(pageIndex);
    }

    /// <summary>
    /// 点击页码时回调方法
    /// </summary>
    /// <param name="pageIndex"></param>
    protected async Task OnPageItemClick(int pageIndex)
    {
        PageIndex = pageIndex;
        if (OnPageClick != null)
        {
            await OnPageClick.Invoke(pageIndex, PageItems);
        }
    }

    /// <summary>
    /// 每页显示数据项数量选项更改时回调方法
    /// </summary>
    protected async Task OnPageItemsSelectItemChanged(SelectedItem item)
    {
        if (int.TryParse(item.Value, out var pageItems))
        {
            PageItems = pageItems;
            PageIndex = 1;
            if (OnPageItemsChanged != null)
            {
                await OnPageItemsChanged.Invoke(PageItems);
            }
            StateHasChanged();
        }
    }

    /// <summary>
    /// 获得页码设置集合
    /// </summary>
    /// <returns></returns>
    private IEnumerable<SelectedItem> GetPageItems()
    {
        var ret = new List<SelectedItem>();
        for (var i = 0; i < PageItemsSource.Count(); i++)
        {
            var v = PageItemsSource.ElementAt(i);
            var item = new SelectedItem(v.ToString(), string.Format(SelectItemsText, v));
            ret.Add(item);
            if (v >= TotalCount)
            {
                break;
            }
        }
        return ret;
    }

    private string GetPageInfoText() => string.Format(PageInfoText, StarIndex, EndIndex);

    private string GetTotalInfoText() => string.Format(TotalInfoText, TotalCount);

    private string GetLabelString => string.Format(LabelString, PageIndex);
}
