// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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

        AlignmentItems ??=
        [
            new("Left", "Start"),
            new("Center", "Center"),
            new("Right", "End")
        ];

        PageItemsSource =
        [
            new("2", "2条/页"),
            new("4", "4条/页"),
            new("10", "10条/页"),
            new("20", "20条/页")
        ];
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new() {
            Name = "PageIndex",
            Description = Localizer["PaginationsPageIndexAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "1"
        },
        new() {
            Name = "PageCount",
            Description = Localizer["PaginationsPageCountAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "MaxPageLinkCount",
            Description = Localizer["PaginationsMaxPageLinkCountAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "5"
        },
        new() {
            Name = "OnPageLinkClick",
            Description = Localizer["PaginationsOnPageLinkClickAttr"],
            Type = "Func<int, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Alignment",
            Description = Localizer["PaginationsAlignmentAttr"],
            Type = "Alignment",
            ValueList = " — ",
            DefaultValue = "Alignment.Right"
        },
        new() {
            Name = "ShowPageInfo",
            Description = Localizer["PaginationsShowPageInfoAttr"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new() {
            Name = "PageInfoText",
            Description = Localizer["PaginationsPageInfoTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "PageInfoTemplate",
            Description = Localizer["PaginationsPageInfoTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "ShowGotoNavigator",
            Description = Localizer["PaginationsShowGotoNavigatorAttr"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new() {
            Name = "GotoNavigatorLabelText",
            Description = Localizer["PaginationsGotoNavigatorLabelTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "GotoTemplate",
            Description = Localizer["PaginationsGotoTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "PrevPageIcon",
            Description = Localizer["PaginationsPrevPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-left"
        },
        new() {
            Name = "PrevEllipsisPageIcon",
            Description = Localizer["PaginationsPrevEllipsisPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-ellipsis"
        },
        new() {
            Name = "NextPageIcon",
            Description = Localizer["PaginationsNextPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-right"
        },
        new() {
            Name = "NextEllipsisPageIcon",
            Description = Localizer["PaginationsNextEllipsisPageIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-ellipsis"
        }
    ];
}
