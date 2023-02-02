// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// CheckboxLists
/// </summary>
public partial class CheckboxLists
{
    [Inject]
    [NotNull]
    private IStringLocalizer<CheckboxLists>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["Att1"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["Att1"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem(){
            Name = "Value",
            Description = Localizer["Att1"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "IsVertical",
            Description = Localizer["Att1"],
            Type = "boolean",
            ValueList = " true / false ",
            DefaultValue = " false "
        }
    };

    /// <summary>
    /// Get event method
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnSelectedChanged",
            Description = Localizer["Event1"],
            Type ="Func<IEnumerable<SelectedItem>, TValue, Task>"
        }
    };
}
