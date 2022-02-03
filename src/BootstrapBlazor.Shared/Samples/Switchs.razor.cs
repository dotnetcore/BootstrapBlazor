// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public sealed partial class Switchs
{
    private class Foo
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("绑定标签")]
        public bool BindValue { get; set; }
    }

    private Foo Model { get; set; } = new Foo();

    /// <summary>
    /// 
    /// </summary>
    private bool BindValue { get; set; } = true;

    [NotNull]
    private BlockLogger? Trace { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="val"></param>
    private void OnValueChanged(bool val)
    {
        BindValue = val;
        Trace.Log($"Switch CurrentValue: {val}");
    }

    private bool? NullValue { get; set; }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Class",
                Description = Localizer["Class"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Height",
                Description = Localizer["Height"],
                Type = "int",
                ValueList = "—",
                DefaultValue = "20"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = Localizer["IsDisabled"],
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OffColor",
                Description = Localizer["OffColor"],
                Type = "Color",
                ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "OnColor",
                Description = Localizer["OnColor"],
                Type = "Color",
                ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
                DefaultValue = "Color.Success"
            },
            new AttributeItem() {
                Name = "OnText",
                Description = Localizer["OnTextAttr"],
                Type = "string",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "OffText",
                Description = Localizer["OffTextAttr"],
                Type = "string",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "OnInnerText",
                Description = Localizer["OnInnerTextAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["OnInnerTextDefaultValue"]!
            },
            new AttributeItem() {
                Name = "OffInnerText",
                Description = Localizer["OffInnerTextAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["OffInnerTextDefaultValue"]!
            },
            new AttributeItem() {
                Name = "ShowInnerText",
                Description = Localizer["ShowInnerText"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Width",
                Description = Localizer["Width"],
                Type = "int",
                ValueList = "—",
                DefaultValue = "40"
            },
            new AttributeItem() {
                Name = "Value",
                Description = Localizer["Value"],
                Type = "boolean",
                ValueList = " ",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "ShowLabel",
                Description = Localizer["ShowLabel"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = Localizer["DisplayText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnValueChanged",
                Description = Localizer["OnValueChanged"],
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
                Description = Localizer["ValueChanged"],
                Type = "EventCallback<bool>"
            }
    };
}
