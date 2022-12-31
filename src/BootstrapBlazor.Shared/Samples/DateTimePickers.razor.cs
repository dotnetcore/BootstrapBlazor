// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// DateTimePickers
/// </summary>
public sealed partial class DateTimePickers
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePickers>? Localizer { get; set; }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnClickConfirm",
            Description = Localizer["Event1"],
            Type ="Action"
        },
        new EventItem()
        {
            Name = "ValueChanged",
            Description = Localizer["Event2"],
            Type ="EventCallback<DateTime?>"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ShowLabel",
            Description = Localizer["Att1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowSidebar",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["Att3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Format",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "yyyy-MM-dd"
        },
        new AttributeItem() {
            Name = "IsShown",
            Description = Localizer["Att5"],
            Type = "boolean",
            ValueList = "",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["Att6"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Value",
            Description = Localizer["Att8"],
            Type = "TValue",
            ValueList = "DateTime | DateTime?",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ViewMode",
            Description = Localizer["Att9"],
            Type = "DatePickerViewMode",
            ValueList = " Date / DateTime / Year / Month",
            DefaultValue = "Date"
        },
        new AttributeItem() {
            Name = "AutoClose",
            Description = Localizer["AttrAutoClose"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    };
}
