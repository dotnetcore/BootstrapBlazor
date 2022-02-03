// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class Pagination
{
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
            if (v >= TotalCount) break;
        }
        return ret;
    }

    private string GetPageInfoText() => string.Format(PageInfoText, StarIndex, EndIndex);

    private string GetTotalInfoText() => string.Format(TotalInfoText, TotalCount);

    private string GetLabelString => string.Format(LabelString, PageIndex);
}
