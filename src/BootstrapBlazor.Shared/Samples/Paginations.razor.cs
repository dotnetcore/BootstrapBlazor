// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Paginations
{
    private BlockLogger? Trace { get; set; }

    private Task OnPageClick(int pageIndex, int pageItems)
    {
        Trace?.Log($"PageIndex: {pageIndex} PageItems: {pageItems}");
        return Task.CompletedTask;
    }

    private Task OnPageItemsChanged(int pageItems)
    {
        Trace?.Log($"PageItems: {pageItems}");
        return Task.CompletedTask;
    }

    private static IEnumerable<int> PageItems => new int[] { 3, 10, 20, 40 };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "PageIndex",
            Description = Localizer["Desc1"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "PageItems",
            Description = Localizer["Desc2"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "PageItemsSource",
            Description = Localizer["Desc3"],
            Type = "IEnumerable<int>",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "ShowPaginationInfo",
            Description = Localizer["Desc4"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "AiraPageLabel",
            Description = Localizer["AiraPageLabel"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["AiraPageLabel"]
        },
        new AttributeItem() {
            Name = "AiraPrevPageText",
            Description = Localizer["AiraPrevPageText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["AiraPrevPageText"]
        },
        new AttributeItem() {
            Name = "AiraFirstPageText",
            Description = Localizer["AiraFirstPageText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["AiraFirstPageText"]
        },
        new AttributeItem() {
            Name = "AiraNextPageText",
            Description = Localizer["AiraNextPageText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["AiraNextPageText"]
        },
        new AttributeItem() {
            Name = "PrePageInfoText",
            Description = Localizer["PrePageInfoText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["PrePageInfoText"]
        },
        new AttributeItem() {
            Name = "RowInfoText",
            Description = Localizer["RowInfoText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["RowInfoText"]
        },
        new AttributeItem() {
            Name = "PageInfoText",
            Description = Localizer["PageInfoText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["PageInfoText"]
        },
        new AttributeItem() {
            Name = "TotalInfoText",
            Description = Localizer["TotalInfoText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["TotalInfoText"]
        },
        new AttributeItem() {
            Name = "SelectItemsText",
            Description = Localizer["SelectItemsText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["SelectItemsText"]
        },
        new AttributeItem() {
            Name = "LabelString",
            Description = Localizer["LabelString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = LocalizerPagination["LabelString"]
        }
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnPageClick",
            Description= Localizer["Event1"],
            Type ="Action<int, int>"
        },
        new EventItem()
        {
            Name = "OnPageItemsChanged",
            Description= Localizer["Event2"],
            Type ="Action<int>"
        }
    };
}
