// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// DateTimeRanges
/// </summary>
public sealed partial class DateTimeRanges
{
    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnConfirm",
            Description="Confirm callback delegate",
            Type ="Action"
        },
        new EventItem()
        {
            Name = "OnClearValue",
            Description="Clear callback delegate",
            Type ="Action"
        },
        new EventItem()
        {
            Name = "OnValueChanged",
            Description="ValueChanged callback delegate",
            Type ="Func<DateTimeRangeValue,Task>"
        }
    };

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ShowLabel",
            Description = "Whether to show the pre-label",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowSidebar",
            Description = "Whether to show the shortcut sidebar",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowToday",
            Description = "Whether to show today shortcut button",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = "Whether to disable default is false",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "ShowSidebar",
            Description = "Whether to display the shortcut sidebar The default is fasle",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "Placement",
            Description = "Set the popup location",
            Type = "Placement",
            ValueList = "top|bottom|left|right",
            DefaultValue = "auto"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = "Pre-label display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "DateFormat",
            Description = "Date format string defaults to yyyy-MM-dd",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "yyyy-MM-dd"
        },
        new AttributeItem() {
            Name = "Value",
            Description = "Custom class containing start time end time",
            Type = "DateTimeRangeValue",
            ValueList = "",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "SidebarItems",
            Description = "Sidebar shortcut options collection",
            Type = "IEnumerable<DateTimeRangeSidebarItem>",
            ValueList = "",
            DefaultValue = " — "
        }
    };
}
