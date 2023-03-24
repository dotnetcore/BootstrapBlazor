// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    protected int InternalPageCount => Math.Max(1, PageCount);

    /// <summary>
    /// 获得 组件 样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("nav nav-pages")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 组件 样式
    /// </summary>
    protected string? PaginationClassString => CssBuilder.Default("pagination")
        .AddClass($"justify-content-{Alignment.ToDescriptionString()}", Alignment != Alignment.None)
        .Build();

    /// <summary>
    /// 获得/设置 开始页码
    /// </summary>
    protected int StartPageIndex => Math.Max(2, Math.Min(InternalPageCount - MaxPageLinkCount, InternalPageIndex - MaxPageLinkCount / 2));

    /// <summary>
    /// 获得/设置 结束页码
    /// </summary>
    protected int EndPageIndex => Math.Min(InternalPageCount - 1, StartPageIndex + MaxPageLinkCount - 1);

    /// <summary>
    /// 获得/设置 对齐方式 默认 Alignment.Right
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; } = Alignment.Right;

    /// <summary>
    /// 获得/设置 上一页图标
    /// </summary>
    [Parameter]
    public string? PrevPageIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一页图标
    /// </summary>
    [Parameter]
    public string? PrevEllipsisPageIcon { get; set; }

    /// <summary>
    /// 获得/设置 下一页图标
    /// </summary>
    [Parameter]
    public string? NextPageIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一页图标
    /// </summary>
    [Parameter]
    public string? NextEllipsisPageIcon { get; set; }

    /// <summary>
    /// 获得/设置 当前页码
    /// </summary>
    [Parameter]
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 获得/设置 页码总数
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public int PageCount { get; set; }

    /// <summary>
    /// 获得/设置 Page up/down 页码数量 默认 5
    /// </summary>
    [Parameter]
    public int MaxPageLinkCount { get; set; } = 5;

    /// <summary>
    /// 点击页码时回调方法 参数是当前页码
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnPageLinkClick { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Pagination>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 显示分页信息文字 默认为 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PageInfoText { get; set; }

    /// <summary>
    /// 获得/设置 Goto 导航模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? GotoTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Goto 跳转导航器 默认 false
    /// </summary>
    [Parameter]
    public bool ShowGotoNavigator { get; set; }

    /// <summary>
    /// 获得/设置 Goto 导航标签显示文字 默认 导航到/Goto
    /// </summary>
    [Parameter]
    public string? GotoNavigatorLabelText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 分页信息 默认 true
    /// </summary>
    [Parameter]
    public bool ShowPageInfo { get; set; } = true;

    /// <summary>
    /// 获得/设置 分页信息模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? PageInfoTemplate { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private int InternalPageIndex => Math.Min(InternalPageCount, PageIndex);

    /// <summary>
    /// <inheritdoc/>
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
    /// 上一页方法
    /// </summary>
    protected async Task MovePrev(int index)
    {
        var pageIndex = InternalPageIndex - index;
        if (pageIndex < 1)
        {
            pageIndex = InternalPageCount;
        }
        await OnPageItemClick(pageIndex);
    }

    /// <summary>
    /// 下一页方法
    /// </summary>
    protected async Task MoveNext(int index)
    {
        var pageIndex = InternalPageIndex + index;
        if (pageIndex > InternalPageCount)
        {
            pageIndex = 1;
        }
        await OnPageItemClick(pageIndex);
    }

    /// <summary>
    /// 点击页码时回调方法
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
