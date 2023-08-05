// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Web;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// RibbonTabs
/// </summary>
public partial class RibbonTabs
{
    [NotNull]
    private IEnumerable<RibbonTabItem>? NormalItems { get; set; }

    [NotNull]
    private IEnumerable<RibbonTabItem>? FloatItems { get; set; }

    [NotNull]
    private IEnumerable<RibbonTabItem>? RightItems { get; set; }

    [NotNull]
    private IEnumerable<RibbonTabItem>? HeaderItems { get; set; }

    [NotNull]
    private IEnumerable<RibbonTabItem>? AnchorItems { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    /// <summary>
    /// OnInitialized method
    /// </summary>
    protected override void OnInitialized()
    {
        NormalItems = GenerateRibbonTabs();
        FloatItems = GenerateRibbonTabs();
        RightItems = GenerateRibbonTabs();
        HeaderItems = GenerateRibbonTabs();
        AnchorItems = GenerateRibbonTabs();
    }

    private string? FileClassString => CssBuilder.Default("collapse")
    .AddClass("show", ActiveTabText == Localizer["RibbonTabsItemsText1"])
    .Build();

    private string? EditClassString => CssBuilder.Default("collapse")
        .AddClass("show", ActiveTabText == Localizer["RibbonTabsItemsText2"])
        .Build();

    private Task OnMenuClickAsync(RibbonTabItem item)
    {
        ActiveTabText = item.Text;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private string? ActiveTabText { get; set; }

    private Task OnFloatChanged(bool @float)
    {
        Logger.Log($"Float: {@float}");
        return Task.CompletedTask;
    }

    private static string? EncodeAnchorCallback(string url, string? text)
    {
        // 获取地址栏中的锚点 取 - 前面一部分
        var hash = url.Split('#').LastOrDefault()?.Split('-').FirstOrDefault();
        if (!string.IsNullOrEmpty(hash))
        {
            hash = HttpUtility.UrlEncode($"{HttpUtility.UrlDecode(hash)}-{text}");
        }
        return $"{url.Split('#').FirstOrDefault()}#{hash}";
    }

    private static string? DecodeAnchorCallback(string url) => HttpUtility.UrlDecode(url.Split('#').LastOrDefault())?.Split('-').LastOrDefault();

    private List<RibbonTabItem> GenerateRibbonTabs() => new()
    {
        new()
        {
            Text = Localizer["RibbonTabsItemsText1"],
            Items = new List<RibbonTabItem>()
            {
                new() { Text = Localizer["RibbonTabsItems1"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                new() { Text = Localizer["RibbonTabsItems2"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                new() { Text = Localizer["RibbonTabsItems3"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                new() { Text = Localizer["RibbonTabsItems4"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                new() { Text = Localizer["RibbonTabsItems5"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                new() { Text = Localizer["RibbonTabsItems6"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] }
            }
        },
        new()
        {
            Text = Localizer["RibbonTabsItemsText2"],
            Items = new List<RibbonTabItem>()
            {
                new() { Text = Localizer["RibbonTabsItems7"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                new() { Text = Localizer["RibbonTabsItems8"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                new() { Text = Localizer["RibbonTabsItems9"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                new() { Text = Localizer["RibbonTabsItems10"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                new() { Text = Localizer["RibbonTabsItems11"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                new() { Text = Localizer["RibbonTabsItems12"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] }
            }
        }
    };

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = Localizer["RibbonTabsShowFloatButtonAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(RibbonTab.OnFloatChanged),
            Description = Localizer["RibbonTabsOnFloatChanged"],
            Type = "bool",
            ValueList = "Func<bool, Task>",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(RibbonTab.RibbonArrowUpIcon),
            Description = Localizer["RibbonTabsRibbonArrowUpIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-up fa-2x"
        },
        new()
        {
            Name = nameof(RibbonTab.RibbonArrowDownIcon),
            Description = Localizer["RibbonTabsRibbonArrowDownIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-down fa-2x"
        },
        new()
        {
            Name = nameof(RibbonTab.RibbonArrowPinIcon),
            Description = Localizer["RibbonTabsRibbonArrowPinIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-thumbtack fa-rotate-90"
        },
        new()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = Localizer["RibbonTabsShowFloatButton"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(RibbonTab.Items),
            Description = Localizer["RibbonTabsItems"],
            Type = "IEnumerable<RibbonTabItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(RibbonTab.OnItemClickAsync),
            Description = Localizer["RibbonTabsOnItemClickAsync"],
            Type = "Func<RibbonTabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(RibbonTab.OnMenuClickAsync),
            Description = Localizer["OnMenuClickAsyncAttr"],
            Type = "Func<RibbonTabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(RibbonTab.RightButtonsTemplate),
            Description = Localizer["RibbonTabsRightButtonsTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
