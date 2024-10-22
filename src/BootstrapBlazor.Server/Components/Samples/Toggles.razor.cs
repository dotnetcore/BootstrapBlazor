// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Toggles
/// </summary>
public sealed partial class Toggles
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private bool BindValue { get; set; } = true;

    private Foo Model { get; set; } = new Foo();

    private void OnValueChanged(bool val)
    {
        BindValue = val;
        Logger.Log($"Toggle CurrentValue: {val}");
    }

    private class Foo
    {
        [DisplayName("绑定标签")]
        public bool BindValue { get; set; }
    }
    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new EventItem()
        {
            Name = "ValueChanged",
            Description = Localizer["ValueChanged"],
            Type = "EventCallback<bool>"
        }
    ];

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Color",
            Description = Localizer["Color"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Success"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["IsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OffText",
            Description = Localizer["OffTextAttr"],
            Type = "string",
            ValueList = "—",
            DefaultValue = Localizer["OffTextDefaultValue"]!
        },
        new()
        {
            Name = "OnText",
            Description = Localizer["OnTextAttr"],
            Type = "string",
            ValueList = "—",
            DefaultValue = Localizer["OnTextDefaultValue"]!
        },
        new()
        {
            Name = "Width",
            Description = Localizer["Width"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "120"
        },
        new()
        {
            Name = "Value",
            Description = Localizer["Value"],
            Type = "boolean",
            ValueList = " ",
            DefaultValue = "None"
        },
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["ShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["DisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnValueChanged",
            Description = Localizer["OnValueChanged"],
            Type = "Func<bool, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
