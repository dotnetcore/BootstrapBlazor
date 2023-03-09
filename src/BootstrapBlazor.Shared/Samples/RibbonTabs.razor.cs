// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// RibbonTabs
/// </summary>
public partial class RibbonTabs
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = Localizer["RibbonTabsShowFloatButtonAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.OnFloatChanged),
            Description = Localizer["RibbonTabsOnFloatChanged"],
            Type = "bool",
            ValueList = "Func<bool, Task>",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowUpIcon),
            Description = Localizer["RibbonTabsRibbonArrowUpIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-up fa-2x"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowDownIcon),
            Description = Localizer["RibbonTabsRibbonArrowDownIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-down fa-2x"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowPinIcon),
            Description = Localizer["RibbonTabsRibbonArrowPinIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-thumbtack fa-rotate-90"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = Localizer["RibbonTabsShowFloatButton"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.Items),
            Description = Localizer["RibbonTabsItems"],
            Type = "IEnumerable<RibbonTabItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.OnItemClickAsync),
            Description = Localizer["RibbonTabsOnItemClickAsync"],
            Type = "Func<RibbonTabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.OnMenuClickAsync),
            Description = Localizer["OnMenuClickAsyncAttr"],
            Type = "Func<RibbonTabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RightButtonsTemplate),
            Description = Localizer["RibbonTabsRightButtonsTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
