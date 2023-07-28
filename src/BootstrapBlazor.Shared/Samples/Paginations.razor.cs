// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Paginations
/// </summary>
public sealed partial class Paginations
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnPageClick(int pageIndex)
    {
        Logger.Log($"PageIndex: {pageIndex}");
        return Task.CompletedTask;
    }

    private Alignment Alignment { get; set; } = Alignment.Right;

    [NotNull]
    private List<SelectedItem>? AlignmentItems { get; set; }

    [NotNull]
    private List<SelectedItem>? PageItemsSource { get; set; }

    private int PageItems { get; set; } = 2;

    private int PageCount => 200 / PageItems;

    private string PageInfoText => $"每页 {PageItems} 条 共 {PageCount} 页";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AlignmentItems ??= new List<SelectedItem>()
        {
            new("Left", "Start"),
            new("Center", "Center"),
            new("Right", "End")
        };

        PageItemsSource = new List<SelectedItem>()
        {
            new("2", "2条/页"),
            new("4", "4条/页"),
            new("10", "10条/页"),
            new("20", "20条/页")
        };
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "PageIndex",
            Description = Localizer["PaginationsPageIndexAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "PageCount",
            Description = Localizer["PaginationsPageCountAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "MaxPageLinkCount",
            Description = Localizer["PaginationsMaxPageLinkCountAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "5"
        },
        new AttributeItem() {
            Name = "OnPageLinkClick",
            Description = Localizer["PaginationsOnPageLinkClickAttr"],
            Type = "Func<int, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Alignment",
            Description = Localizer["PaginationsAlignmentAttr"],
            Type = "Alignment",
            ValueList = " — ",
            DefaultValue = "Alignment.Right"
        },
        new AttributeItem() {
            Name = "ShowPageInfo",
            Description = Localizer["PaginationsShowPageInfoAttr"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "PageInfoText",
            Description = Localizer["PaginationsPageInfoTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "PageInfoTemplate",
            Description = Localizer["PaginationsPageInfoTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ShowGotoNavigator",
            Description = Localizer["PaginationsShowGotoNavigatorAttr"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "GotoNavigatorLabelText",
            Description = Localizer["PaginationsGotoNavigatorLabelTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "GotoTemplate",
            Description = Localizer["PaginationsGotoTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "PrevPageIcon",
            Description = Localizer["PaginationsPrevPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-left"
        },
        new AttributeItem() {
            Name = "PrevEllipsisPageIcon",
            Description = Localizer["PaginationsPrevEllipsisPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-ellipsis"
        },
        new AttributeItem() {
            Name = "NextPageIcon",
            Description = Localizer["PaginationsNextPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-right"
        },
        new AttributeItem() {
            Name = "NextEllipsisPageIcon",
            Description = Localizer["PaginationsNextEllipsisPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-ellipsis"
        }
    };
}
