// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Layouts
/// </summary>
public sealed partial class Layouts
{
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Header",
            Description = Localizer["Layouts_Header_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Side",
            Description = Localizer["Layouts_Side_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "SideWidth",
            Description = Localizer["Layouts_SideWidth_Description"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "300px"
        },
        new AttributeItem() {
            Name = "Main",
            Description = Localizer["Layouts_Main_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Footer",
            Description = Localizer["Layouts_Footer_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Menus",
            Description = Localizer["Layouts_Menus_Description"],
            Type = "IEnumerable<MenuItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsFullSide",
            Description = Localizer["Layouts_IsFullSide_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsPage",
            Description = Localizer["Layouts_IsPage_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsFixedFooter",
            Description = Localizer["Layouts_IsFixedFooter_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsFixedHeader",
            Description = Localizer["Layouts_IsFixedHeader_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsAccordion",
            Description = Localizer["Layouts_IsAccordion_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCollapseBar",
            Description =  Localizer["Layouts_ShowCollapseBar_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description =  Localizer["Layouts_ShowFooter_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowGotoTop",
            Description =  Localizer["Layouts_ShowGotoTop_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "UseTabSet",
            Description =  Localizer["Layouts_UseTabSet_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "AdditionalAssemblies",
            Description =  Localizer["Layouts_AdditionalAssemblies_Description"],
            Type = "IEnumerable<Assembly>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnCollapsed",
            Description =  Localizer["Layouts_OnCollapsed_Description"],
            Type = "Func<bool, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnClickMenu",
            Description =  Localizer["Layouts_OnClickMenu_Description"],
            Type = "Func<bool, MenuItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "TabDefaultUrl",
            Description =  Localizer["Layouts_TabDefaultUrl_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
