// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using static BootstrapBlazor.Shared.Pages.Menus;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Layouts
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Menus>? Localizer { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Layouts>? LayoutsLocalizer { get; set; }

        private IEnumerable<MenuItem>? IconSideMenuItems { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IconSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(Localizer);
        }

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Header",
                Description = LayoutsLocalizer["Desc1"]!,
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Side",
                Description = LayoutsLocalizer["Desc2"]!,
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SideWidth",
                Description = LayoutsLocalizer["Desc3"]!,
                Type = "string",
                ValueList = " — ",
                DefaultValue = "300px"
            },
            new AttributeItem() {
                Name = "Main",
                Description = LayoutsLocalizer["Desc4"]!,
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Footer",
                Description = LayoutsLocalizer["Desc2"]!,
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Menus",
                Description = LayoutsLocalizer["Desc6"]!,
                Type = "IEnumerable<MenuItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsFullSide",
                Description = LayoutsLocalizer["Desc7"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsPage",
                Description = LayoutsLocalizer["Desc8"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsFixedFooter",
                Description = LayoutsLocalizer["Desc9"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsFixedHeader",
                Description = LayoutsLocalizer["Desc10"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowCollapseBar",
                Description =  LayoutsLocalizer["Desc11"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowFooter",
                Description =  LayoutsLocalizer["Desc12"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowGotoTop",
                Description =  LayoutsLocalizer["Desc13"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "UseTabSet",
                Description =  LayoutsLocalizer["Desc14"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "AdditionalAssemblies",
                Description =  LayoutsLocalizer["Desc15"]!,
                Type = "IEnumerable<Assembly>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnCollapsed",
                Description =  LayoutsLocalizer["Desc16"]!,
                Type = "Func<bool, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnClickMenu",
                Description =  LayoutsLocalizer["Desc17"]!,
                Type = "Func<bool, MenuItem>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
