// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Transfers
/// </summary>
public sealed partial class Transfers : ComponentBase
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["Items"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "LeftButtonText",
            Description = Localizer["LeftButtonTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "LeftPanelText",
            Description = Localizer["LeftPanelTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["LeftPanelDefaultValue"]!
        },
        new AttributeItem() {
            Name = "RightButtonText",
            Description = Localizer["RightButtonTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "RightPanelText",
            Description = Localizer["RightPanelTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["RightPanelTextDefaultValue"]!
        },
        new AttributeItem() {
            Name = "ShowSearch",
            Description = "",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "LeftPannelSearchPlaceHolderString",
            Description = Localizer["LeftPannelSearchPlaceHolderString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "RightPannelSearchPlaceHolderString",
            Description = Localizer["RightPannelSearchPlaceHolderString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["IsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "LeftHeaderTemplate",
            Description = Localizer["LeftHeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "LeftItemTemplate",
            Description = Localizer["LeftItemTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "RightHeaderTemplate",
            Description = Localizer["RightHeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "RightItemTemplate",
            Description = Localizer["RightItemTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
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
            Name = nameof(Transfer<string>.OnSelectedItemsChanged),
            Description = Localizer["OnSelectedItemsChanged"],
            Type = "Func<IEnumerable<SelectedItem>, Task>"
        },
        new EventItem()
        {
            Name = "OnSetItemClass",
            Description = Localizer["OnSetItemClass"],
            Type = "Func<SelectedItem, string?>"
        }
    };
}
