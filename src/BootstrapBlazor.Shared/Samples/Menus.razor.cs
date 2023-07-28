// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Menus
/// </summary>
public sealed partial class Menus
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Items",
            Description = Localizer["MenusAttr_Items"],
            Type = "IEnumerable<MenuItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsVertical",
            Description = Localizer["MenusAttr_IsVertical"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsBottom",
            Description = Localizer["MenusAttr_IsBottom"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsAccordion",
            Description = Localizer["MenusAttr_IsAccordion"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "DisableNavigation",
            Description = Localizer["MenusAttr_DisableNavigation"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "OnClick",
            Description = Localizer["MenusAttr_OnClick"],
            Type = "Func<MenuItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
