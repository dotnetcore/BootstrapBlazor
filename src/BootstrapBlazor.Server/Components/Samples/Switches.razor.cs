// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Switches
/// </summary>
public sealed partial class Switches
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private bool BindValue { get; set; } = true;

    private bool? NullValue { get; set; }

    private void OnValueChanged(bool val)
    {
        BindValue = val;
        Logger.Log($"Switch CurrentValue: {val}");
    }

    private class Foo
    {
        [DisplayName("绑定标签")]
        public bool BindValue { get; set; }
    }

    private Foo Model { get; set; } = new Foo();

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Class",
            Description = Localizer["SwitchesAttributeClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Height",
            Description = Localizer["SwitchesAttributeHeight"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "20"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["SwitchesAttributeIsDisabled"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OffColor",
            Description = Localizer["SwitchesAttributeOffColor"],
            Type = "Color",
            ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
            DefaultValue = "None"
        },
        new()
        {
            Name = "OnColor",
            Description = Localizer["SwitchesAttributeOnColor"],
            Type = "Color",
            ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
            DefaultValue = "Color.Success"
        },
        new()
        {
            Name = "OnText",
            Description = Localizer["SwitchesAttributeOnTextAttr"],
            Type = "string",
            ValueList = "—",
            DefaultValue = "—"
        },
        new()
        {
            Name = "OffText",
            Description = Localizer["SwitchesAttributeOffTextAttr"],
            Type = "string",
            ValueList = "—",
            DefaultValue = "—"
        },
        new()
        {
            Name = "OnInnerText",
            Description = Localizer["SwitchesAttributeOnInnerTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SwitchesAttributeOnInnerTextDefaultValue"]!
        },
        new()
        {
            Name = "OffInnerText",
            Description = Localizer["SwitchesAttributeOffInnerTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SwitchesAttributeOffInnerTextDefaultValue"]!
        },
        new()
        {
            Name = "ShowInnerText",
            Description = Localizer["SwitchesAttributeShowInnerText"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Width",
            Description = Localizer["SwitchesAttributeWidth"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "40"
        },
        new()
        {
            Name = "Value",
            Description = Localizer["SwitchesAttributeValue"],
            Type = "boolean",
            ValueList = " ",
            DefaultValue = "None"
        },
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["SwitchesAttributeShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["SwitchesAttributeDisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnValueChanged",
            Description = Localizer["SwitchesAttributeOnValueChanged"],
            Type = "Func<bool, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "ValueChanged",
            Description = Localizer["SwitchesEventValueChanged"],
            Type = "EventCallback<bool>"
        }
    ];
}
