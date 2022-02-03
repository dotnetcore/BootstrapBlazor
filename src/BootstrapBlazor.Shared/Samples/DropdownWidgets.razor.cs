// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class DropdownWidgets
{

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Icon",
                Description = Localizer["Icon"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-bell-o"
            },
            new AttributeItem() {
                Name = "BadgeColor",
                Description = Localizer["BadgeColor"],
                Type = "Color",
                ValueList = " — ",
                DefaultValue = "Success"
            },
            new AttributeItem() {
                Name = "HeaderColor",
                Description = Localizer["HeaderColor"],
                Type = "Color",
                ValueList = "Primary / Secondary / Info / Warning / Danger ",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "BadgeNumber",
                Description = Localizer["BadgeNumber"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowArrow",
                Description = Localizer["ShowArrow"],
                Type = "boolean",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "MenuAlignment",
                Description = Localizer["MenuAlignment"],
                Type = "Alignment",
                ValueList = "None / Left / Center / Right ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "HeaderTemplate",
                Description = Localizer["HeaderTemplate"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "BodyTemplate",
                Description = Localizer["BodyTemplate"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FooterTemplate",
                Description = Localizer["FooterTemplate"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
