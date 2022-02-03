// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Layouts
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Menus>? LocalizerMenu { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Layouts>? Localizer { get; set; }

    private IEnumerable<MenuItem>? IconSideMenuItems { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        IconSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(LocalizerMenu);
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Header",
                Description = Localizer["Desc1"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Side",
                Description = Localizer["Desc2"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SideWidth",
                Description = Localizer["Desc3"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "300px"
            },
            new AttributeItem() {
                Name = "Main",
                Description = Localizer["Desc4"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Footer",
                Description = Localizer["Desc2"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Menus",
                Description = Localizer["Desc6"],
                Type = "IEnumerable<MenuItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsFullSide",
                Description = Localizer["Desc7"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsPage",
                Description = Localizer["Desc8"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsFixedFooter",
                Description = Localizer["Desc9"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsFixedHeader",
                Description = Localizer["Desc10"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowCollapseBar",
                Description =  Localizer["Desc11"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowFooter",
                Description =  Localizer["Desc12"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowGotoTop",
                Description =  Localizer["Desc13"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "UseTabSet",
                Description =  Localizer["Desc14"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "AdditionalAssemblies",
                Description =  Localizer["Desc15"],
                Type = "IEnumerable<Assembly>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnCollapsed",
                Description =  Localizer["Desc16"],
                Type = "Func<bool, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnClickMenu",
                Description =  Localizer["Desc17"],
                Type = "Func<bool, MenuItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TabDefaultUrl",
                Description =  Localizer["TabDefaultUrl"],
                Type = "string?",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
