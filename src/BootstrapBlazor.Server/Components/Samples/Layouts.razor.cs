// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Layout 组件示例
/// </summary>
public sealed partial class Layouts
{
    private List<MenuItem>? IconSideMenuItems1 { get; set; }

    private List<MenuItem>? IconSideMenuItems2 { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        IconSideMenuItems1 = await MenusDataGenerator.GetIconSideMenuItemsAsync(LocalizerMenu);
        IconSideMenuItems2 = await MenusDataGenerator.GetIconSideMenuItemsAsync(LocalizerMenu);
    }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Header",
            Description = Localizer["Layouts_Header_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Side",
            Description = Localizer["Layouts_Side_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SideWidth",
            Description = Localizer["Layouts_SideWidth_Description"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "300px"
        },
        new()
        {
            Name = "Main",
            Description = Localizer["Layouts_Main_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Footer",
            Description = Localizer["Layouts_Footer_Description"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Menus",
            Description = Localizer["Layouts_Menus_Description"],
            Type = "IEnumerable<MenuItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsPage",
            Description = Localizer["Layouts_IsPage_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsFullSide",
            Description = Localizer["Layouts_IsFullSide_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsFixedFooter",
            Description = Localizer["Layouts_IsFixedFooter_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsFixedHeader",
            Description = Localizer["Layouts_IsFixedHeader_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsAccordion",
            Description = Localizer["Layouts_IsAccordion_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowCollapseBar",
            Description =  Localizer["Layouts_ShowCollapseBar_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "SidebarMinWidth",
            Description =  Localizer["Layouts_SidebarMinWidth_Description"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SidebarMaxWidth",
            Description =  Localizer["Layouts_SidebarMaxWidth_Description"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowSplitebar",
            Description =  Localizer["Layouts_ShowSplitebar_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowFooter",
            Description =  Localizer["Layouts_ShowFooter_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowGotoTop",
            Description =  Localizer["Layouts_ShowGotoTop_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "UseTabSet",
            Description =  Localizer["Layouts_UseTabSet_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(BootstrapBlazor.Components.Layout.IsFixedTabHeader),
            Description = Localizer["Layouts_IsFixedTabHeader_Description"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "AdditionalAssemblies",
            Description =  Localizer["Layouts_AdditionalAssemblies_Description"],
            Type = "IEnumerable<Assembly>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnCollapsed",
            Description =  Localizer["Layouts_OnCollapsed_Description"],
            Type = "Func<bool, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClickMenu",
            Description =  Localizer["Layouts_OnClickMenu_Description"],
            Type = "Func<bool, MenuItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TabDefaultUrl",
            Description =  Localizer["Layouts_TabDefaultUrl_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
