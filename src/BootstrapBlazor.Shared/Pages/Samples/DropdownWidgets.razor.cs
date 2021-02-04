// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
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
                Description = "挂件图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-bell-o"
            },
            new AttributeItem() {
                Name = "BadgeColor",
                Description = "徽章颜色",
                Type = "Color",
                ValueList = " — ",
                DefaultValue = "Success"
            },
            new AttributeItem() {
                Name = "HeaderColor",
                Description = "Header 颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Info / Warning / Danger ",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "BadgeNumber",
                Description = "徽章显示数量",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowArrow",
                Description = "是否显示小箭头",
                Type = "boolean",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "MenuAlignment",
                Description = "菜单对齐方式",
                Type = "Alignment",
                ValueList = "None / Left / Center / Right ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "HeaderTemplate",
                Description = "Header 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "BodyTemplate",
                Description = "Body 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FooterTemplate",
                Description = "Footer 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
