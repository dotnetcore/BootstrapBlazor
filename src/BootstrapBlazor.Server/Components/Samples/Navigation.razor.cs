// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// navigation
/// </summary>
public sealed partial class Navigation
{
    private IEnumerable<NavLink> Items => GetItems();

    private List<NavLink> GetItems()
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
        link.SetParametersAsync(ParameterView.FromDictionary(parameters));
        ret.Add(link);
        return ret;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
}
