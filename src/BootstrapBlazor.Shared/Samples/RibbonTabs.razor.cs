// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class RibbonTabs
{
    [NotNull]
    private IEnumerable<RibbonTabItem>? Items { get; set; }

    /// <summary>
    /// OnInitialized method
    /// </summary>
    protected override void OnInitialized()
    {
        Items = new List<RibbonTabItem>()
        {
            new()
            {
                Text = Localizer["ItemsText1"],
                Items = new List<RibbonTabItem>()
                {
                    new() { Text = Localizer["Items1"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                    new() { Text = Localizer["Items2"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                    new() { Text = Localizer["Items3"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                    new() { Text = Localizer["Items4"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                    new() { Text = Localizer["Items5"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] },
                    new() { Text = Localizer["Items6"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName1"] }
                }
            },
            new()
            {
                Text = Localizer["ItemsText2"],
                Items = new List<RibbonTabItem>()
                {
                    new() { Text = Localizer["Items7"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                    new() { Text = Localizer["Items8"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                    new() { Text = Localizer["Items9"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                    new() { Text = Localizer["Items10"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                    new() { Text = Localizer["Items11"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] },
                    new() { Text = Localizer["Items12"], Icon = "fa-solid fa-font-awesome", GroupName = Localizer["ItemsGroupName2"] }
                }
            }
        };

        ActiveTabText = Localizer["ItemsText1"];
    }

    private string? ActiveTabText { get; set; }

    private string? FileClassString => CssBuilder.Default("collapse")
        .AddClass("show", ActiveTabText == Localizer["ItemsText1"])
        .Build();

    private string? EditClassString => CssBuilder.Default("collapse")
        .AddClass("show", ActiveTabText == Localizer["ItemsText2"])
        .Build();

    private Task OnMenuClickAsync(string text, string url)
    {
        ActiveTabText = text;
        StateHasChanged();
        return Task.CompletedTask;
    }

    [NotNull]
    private BlockLogger? Logger { get; set; }

    private Task OnFloatChanged(bool @float)
    {
        Logger.Log($"Float: {@float}");
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = Localizer["Attr1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.OnFloatChanged),
            Description = Localizer["Attr2"],
            Type = "bool",
            ValueList = "Func<bool, Task>",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowUpIcon),
            Description = Localizer["Attr3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-up fa-2x"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowDownIcon),
            Description = Localizer["Attr4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-down fa-2x"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowPinIcon),
            Description = Localizer["Attr5"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-thumbtack fa-rotate-90"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = Localizer["Attr6"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.Items),
            Description = Localizer["Attr7"],
            Type = "IEnumerable<RibbonTabItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.OnItemClickAsync),
            Description = Localizer["Attr8"],
            Type = "Func<RibbonTabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RightButtonsTemplate),
            Description = Localizer["Attr9"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
