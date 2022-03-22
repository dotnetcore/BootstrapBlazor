// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Routing;

namespace UnitTest.Components;

public class NavTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_OK()
    {
        var cut = Context.RenderComponent<Nav>(builder => builder.Add(s => s.Items, GetItems()));
        Assert.Contains("href=\"/Admin/Index\"", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.Items, null));
    }

    [Fact]
    public void Alignment_OK()
    {
        var cut = Context.RenderComponent<Nav>(builder =>
        {
            builder.Add(s => s.Items, GetItems());
            builder.Add(s => s.Alignment, Alignment.Right);
        });

        var ele = cut.Find(".justify-content-end");
        Assert.NotNull(ele);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Center);
        });
        ele = cut.Find(".justify-content-center");
        Assert.NotNull(ele);
    }

    [Fact]
    public void IsVertical_OK()
    {
        var cut = Context.RenderComponent<Nav>(builder =>
        {
            builder.Add(s => s.Items, GetItems());
            builder.Add(s => s.IsVertical, true);
        });

        var ele = cut.Find(".flex-column");
        Assert.NotNull(ele);
    }

    [Fact]
    public void IsPills_OK()
    {
        var cut = Context.RenderComponent<Nav>(builder =>
        {
            builder.Add(s => s.Items, GetItems());
            builder.Add(s => s.IsPills, true);
        });

        var ele = cut.Find(".nav-pills");
        Assert.NotNull(ele);
    }

    [Fact]
    public void IsFill_OK()
    {
        var cut = Context.RenderComponent<Nav>(builder =>
        {
            builder.Add(s => s.Items, GetItems());
            builder.Add(s => s.IsPills, true);
            builder.Add(s => s.IsFill, true);
        });

        var ele = cut.Find(".nav-fill");
        Assert.NotNull(ele);
    }

    [Fact]
    public void IsJustified_OK()
    {
        var cut = Context.RenderComponent<Nav>(builder =>
        {
            builder.Add(s => s.Items, GetItems());
            builder.Add(s => s.IsPills, true);
            builder.Add(s => s.IsFill, true);
            builder.Add(s => s.IsJustified, true);
        });

        var ele = cut.Find(".nav-justified");
        Assert.NotNull(ele);
    }

    [Fact]
    public void ChildContent_OK()
    {
        var cut = Context.RenderComponent<Nav>(builder =>
        {
            builder.Add(s => s.Items, GetItems());
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.CloseComponent();
            }));
        });

        var component = cut.FindComponent<Button>();
        Assert.NotNull(component);
    }

    private IEnumerable<NavLink> GetItems()
    {
        var ret = new List<NavLink>();
        var link = new NavLink();
        var parameters = new Dictionary<string, object?>()
        {
            ["href"] = "/Admin/Index",
            ["class"] = "nav-link nav-item",
            ["target"] = "_blank",
            ["ChildContent"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "BootstrapAdmin");
            })
        };
        link.SetParametersAsync(ParameterView.FromDictionary(parameters!));
        ret.Add(link);
        return ret;
    }
}
