// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Switchs
/// </summary>
public sealed partial class Switchs
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Class",
            Description = Localizer["SwitchsAttributeClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Height",
            Description = Localizer["SwitchsAttributeHeight"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "20"
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["SwitchsAttributeIsDisabled"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "OffColor",
            Description = Localizer["SwitchsAttributeOffColor"],
            Type = "Color",
            ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "OnColor",
            Description = Localizer["SwitchsAttributeOnColor"],
            Type = "Color",
            ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
            DefaultValue = "Color.Success"
        },
        new AttributeItem() {
            Name = "OnText",
            Description = Localizer["SwitchsAttributeOnTextAttr"],
            Type = "string",
            ValueList = "—",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "OffText",
            Description = Localizer["SwitchsAttributeOffTextAttr"],
            Type = "string",
            ValueList = "—",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "OnInnerText",
            Description = Localizer["SwitchsAttributeOnInnerTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SwitchsAttributeOnInnerTextDefaultValue"]!
        },
        new AttributeItem() {
            Name = "OffInnerText",
            Description = Localizer["SwitchsAttributeOffInnerTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SwitchsAttributeOffInnerTextDefaultValue"]!
        },
        new AttributeItem() {
            Name = "ShowInnerText",
            Description = Localizer["SwitchsAttributeShowInnerText"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Width",
            Description = Localizer["SwitchsAttributeWidth"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "40"
        },
        new AttributeItem() {
            Name = "Value",
            Description = Localizer["SwitchsAttributeValue"],
            Type = "boolean",
            ValueList = " ",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "ShowLabel",
            Description = Localizer["SwitchsAttributeShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["SwitchsAttributeDisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnValueChanged",
            Description = Localizer["SwitchsAttributeOnValueChanged"],
            Type = "Func<bool, Task>",
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
            Name = "ValueChanged",
            Description = Localizer["SwitchsEventValueChanged"],
            Type = "EventCallback<bool>"
        }
    };
}
