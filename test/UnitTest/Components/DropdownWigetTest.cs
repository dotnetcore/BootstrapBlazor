// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class DropdownWigetTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder => builder.Add(s => s.Items, GetItems()));
    }

    [Fact]
    public void ChildContent_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
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

    [Fact]
    public void Icon_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.Icon), "fa-regular fa-bell");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".fa-bell");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Title_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.Title), "Wiget Title");
                builder.CloseComponent();
            }));
        });

        Assert.Contains("Wiget Title", cut.Markup);
    }

    [Fact]
    public void BadgeColor_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.BadgeColor), Color.Success);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BadgeNumber), "4");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".bg-success");
        Assert.NotNull(ele);
    }

    [Fact]
    public void BadgeNumber_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.BadgeColor), Color.Success);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BadgeNumber), "4");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".dropdown-toggle").Children.Last();
        var count = ele.TextContent;
        Assert.Equal("4", count);
    }

    [Fact]
    public void ShowArrow_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.ShowArrow), true);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BadgeNumber), "4");
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".dropdown-arrow");
        Assert.NotNull(ele);
    }

    [Fact]
    public void BodyTemplate_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.BodyTemplate), new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });

        var component = cut.FindComponent<Button>();
        Assert.NotNull(component);
    }

    [Fact]
    public void HeaderTemplate_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.HeaderTemplate), new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });

        var component = cut.FindComponent<Button>();
        Assert.NotNull(component);
    }

    [Fact]
    public void HeaderColor_OK()
    {
        var cut = Context.RenderComponent<DropdownWidget>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<DropdownWidgetItem>(0);
                builder.AddAttribute(1, nameof(DropdownWidgetItem.HeaderColor), Color.Success);
                builder.AddAttribute(2, nameof(DropdownWidgetItem.HeaderTemplate), new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        });

        var ele = cut.Find(".bg-success");
        Assert.NotNull(ele);
    }

    private static IEnumerable<DropdownWidgetItem> GetItems()
    {
        var ret = new List<DropdownWidgetItem>();
        var wiget = new DropdownWidgetItem();
        var parameters = new Dictionary<string, object?>()
        {
            ["Icon"] = "fa-regular fa-bell",
            ["Title"] = "Title",
            ["BadgeColor"] = Color.Success,
            ["HeaderColor"] = Color.Primary,
            ["BadgeNumber"] = "10",
            ["ShowArrow"] = true,
            ["HeaderTemplate"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "HeaderTemplate");
            }),
            ["BodyTemplate"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "BodyTemplate");
            }),
            ["FooterTemplate"] = new RenderFragment(builder =>
            {
                builder.AddContent(0, "FooterTemplate");
            }),

        };
        wiget.SetParametersAsync(ParameterView.FromDictionary(parameters!));
        ret.Add(wiget);
        return ret;
    }
}
