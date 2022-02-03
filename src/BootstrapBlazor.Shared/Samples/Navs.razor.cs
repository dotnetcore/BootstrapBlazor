// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Navs
{
    private IEnumerable<NavLink> Items => GetItems();

    private IEnumerable<NavLink> GetItems()
    {
        var ret = new List<NavLink>();
        var link = new NavLink();
        var parameters = new Dictionary<string, object?>()
        {
            ["href"] = WebsiteOption.Value.AdminUrl,
            ["class"] = "nav-link nav-item",
            ["target"] = "_blank",
            ["ChildContent"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "BootstrapAdmin");
            })
        };
        // TODO: NET6.0 移除 ! 断言
        link.SetParametersAsync(ParameterView.FromDictionary(parameters!));
        ret.Add(link);
        return ret;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["Desc1"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Alignment",
                Description = Localizer["Desc2"],
                Type = "Alignment",
                ValueList = "Left|Center|Right",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsVertical",
                Description = Localizer["Desc3"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsPills",
                Description = Localizer["Desc4"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsFill",
                Description = Localizer["Desc5"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsJustified",
                Description = Localizer["Desc6"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
    };
}
