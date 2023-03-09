// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Radios
/// </summary>
public sealed partial class Radios
{
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["RadiosDisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "GroupName",
            Description = Localizer["RadiosGroupName"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "NullItemText",
            Description = Localizer["RadiosNullItemText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["RadiosIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsVertical",
            Description = Localizer["RadiosIsVertical"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(RadioList<string>.IsButton),
            Description = Localizer["RadiosIsButton"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsAutoAddNullItem",
            Description = Localizer["RadiosIsAutoAddNullItem"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["RadiosItems"],
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = "—"
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
            Name = "OnSelectedChanged",
            Description = Localizer["RadiosOnSelectedChangedEvent"],
            Type ="Func<IEnumerable<SelectedItem>, TValue, Task>"
        }
    };
}
