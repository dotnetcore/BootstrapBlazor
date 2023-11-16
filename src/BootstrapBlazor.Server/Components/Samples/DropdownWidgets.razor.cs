// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class DropdownWidgets
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Icon",
            Description = Localizer["Icon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-bell"
        },
        new()
        {
            Name = "BadgeColor",
            Description = Localizer["BadgeColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Success"
        },
        new()
        {
            Name = "HeaderColor",
            Description = Localizer["HeaderColor"],
            Type = "Color",
            ValueList = "Primary / Secondary / Info / Warning / Danger ",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "BadgeNumber",
            Description = Localizer["BadgeNumber"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowArrow",
            Description = Localizer["ShowArrow"],
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "MenuAlignment",
            Description = Localizer["MenuAlignment"],
            Type = "Alignment",
            ValueList = "None / Left / Center / Right ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["HeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BodyTemplate",
            Description = Localizer["BodyTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FooterTemplate",
            Description = Localizer["FooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
