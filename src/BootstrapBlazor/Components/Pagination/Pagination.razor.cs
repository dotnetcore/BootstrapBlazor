// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Pagination 组件</para>
/// <para lang="en">Pagination Component</para>
/// </summary>
public partial class Pagination
{
    /// <summary>
    /// <para lang="zh">获得/设置 页码总数</para>
    /// <para lang="en">Get/Set Total Page Count</para>
    /// </summary>
    protected int InternalPageCount => Math.Max(1, PageCount);

    /// <summary>
    /// <para lang="zh">获得 组件 样式</para>
    /// <para lang="en">Get Component Style</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("nav nav-pages")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 组件 样式</para>
    /// <para lang="en">Get Component Style</para>
    /// </summary>
    protected string? PaginationClassString => CssBuilder.Default("pagination")
        .AddClass($"justify-content-{Alignment.ToDescriptionString()}", Alignment != Alignment.None)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 开始页码</para>
    /// <para lang="en">Get/Set Start Page Index</para>
    /// </summary>
    protected int StartPageIndex => Math.Max(2, Math.Min(InternalPageCount - MaxPageLinkCount, InternalPageIndex - MaxPageLinkCount / 2));

    /// <summary>
    /// <para lang="zh">获得/设置 结束页码</para>
    /// <para lang="en">Get/Set End Page Index</para>
    /// </summary>
    protected int EndPageIndex => Math.Min(InternalPageCount - 1, StartPageIndex + MaxPageLinkCount - 1);

    /// <summary>
    /// <para lang="zh">获得/设置 对齐方式 默认 Alignment.Right</para>
    /// <para lang="en">Get/Set Alignment. Default Alignment.Right</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; } = Alignment.Right;

    /// <summary>
    /// <para lang="zh">获得/设置 上一页图标</para>
    /// <para lang="en">Get/Set Previous Page Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PrevPageIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上一页图标</para>
    /// <para lang="en">Get/Set Previous Page Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PrevEllipsisPageIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下一页图标</para>
    /// <para lang="en">Get/Set Next Page Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? NextPageIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上一页图标</para>
    /// <para lang="en">Get/Set Previous Page Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? NextEllipsisPageIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前页码</para>
    /// <para lang="en">Get/Set Current Page Index</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// <para lang="zh">获得/设置 页码总数</para>
    /// <para lang="en">Get/Set Total Page Count</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public int PageCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Page up/down 页码数量 默认 5</para>
    /// <para lang="en">Get/Set Page up/down link count. Default 5</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int MaxPageLinkCount { get; set; } = 5;

    /// <summary>
    /// <para lang="zh">点击页码时回调方法 参数是当前页码</para>
    /// <para lang="en">Callback method when page link is clicked. Parameter is current page index</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnPageLinkClick { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Pagination>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示分页信息文字 默认为 null</para>
    /// <para lang="en">Get/Set Pagination Info Text. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PageInfoText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Goto 导航模板 默认 null</para>
    /// <para lang="en">Get/Set Goto Navigation Template. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? GotoTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Goto 跳转导航器 默认 false</para>
    /// <para lang="en">Get/Set Whether to show Goto Navigator. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowGotoNavigator { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Goto 导航标签显示文字 默认 导航到/Goto</para>
    /// <para lang="en">Get/Set Goto Navigator Label Text. Default Goto</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? GotoNavigatorLabelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 分页信息 默认 true</para>
    /// <para lang="en">Get/Set Whether to show Page Info. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowPageInfo { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 分页信息模板 默认 null</para>
    /// <para lang="en">Get/Set Page Info Template. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? PageInfoTemplate { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private int InternalPageIndex => Math.Min(InternalPageCount, PageIndex);

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        GotoNavigatorLabelText ??= Localizer[nameof(GotoNavigatorLabelText)];
        PrevPageIcon ??= IconTheme.GetIconByKey(ComponentIcons.PaginationPrevPageIcon);
        NextPageIcon ??= IconTheme.GetIconByKey(ComponentIcons.PaginationNextPageIcon);
        PrevEllipsisPageIcon ??= IconTheme.GetIconByKey(ComponentIcons.PaginationPrevEllipsisPageIcon);
        NextEllipsisPageIcon ??= IconTheme.GetIconByKey(ComponentIcons.PaginationNextEllipsisPageIcon);
    }

    private async Task OnClick(int index)
    {
        await OnPageItemClick(index);
    }

    private async Task OnGoto(int index)
    {
        var pageIndex = Math.Max(1, Math.Min(index, PageCount));
        if (pageIndex != InternalPageIndex)
        {
            await OnPageItemClick(pageIndex);
        }
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">上一页方法</para>
    /// <para lang="en">Move Previous Method</para>
    /// </summary>
    protected async Task MovePrev(int index)
    {
        var pageIndex = InternalPageIndex - index;
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }
        await OnPageItemClick(pageIndex);
    }

    /// <summary>
    /// <para lang="zh">下一页方法</para>
    /// <para lang="en">Move Next Method</para>
    /// </summary>
    protected async Task MoveNext(int index)
    {
        var pageIndex = InternalPageIndex + index;
        if (pageIndex > InternalPageCount)
        {
            pageIndex = InternalPageCount;
        }
        await OnPageItemClick(pageIndex);
    }

    /// <summary>
    /// <para lang="zh">点击页码时回调方法</para>
    /// <para lang="en">Callback method when page link is clicked</para>
    /// </summary>
    /// <param name="pageIndex"></param>
    protected async Task OnPageItemClick(int pageIndex)
    {
        PageIndex = pageIndex;
        if (OnPageLinkClick != null)
        {
            await OnPageLinkClick(pageIndex);
        }
    }
}
