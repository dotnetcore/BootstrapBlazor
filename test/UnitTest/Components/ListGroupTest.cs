// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ListGroupTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<ListGroup<Foo>>();
        cut.MarkupMatches("<div class=\"list-group\"><div class=\"list-group-body scroll\"></div></div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>()
            {
                new() { Name = "Test 1" },
                new() { Name = "Test 1" }
            });
        });

        var items = cut.FindAll(".list-group-item");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void ClickItem_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<ListGroup<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>()
            {
                new() { Name = "Test 1" },
                new() { Name = "Test 1" }
            });
            pb.Add(a => a.OnClickItem, foo =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        var item = cut.Find(".list-group-item");
        item.Click();
        cut.WaitForState(() => clicked);
    }

    [Fact]
    public void GetItemText_Ok()
    {
        var cut = Context.RenderComponent<ListGroup<Foo?>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo?>()
            {
                new() { Name = "Test 1" },
                new() { Name = "Test 1" },
                (Foo?)null
           });
            pb.Add(a => a.GetItemDisplayText, foo => foo?.Name ?? "");
        });
        var item = cut.Find(".list-group-item");
        Assert.Equal("Test 1", item.TextContent);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.GetItemDisplayText, null);
        });
        cut.WaitForAssertion(() => cut.MarkupMatches("<div class=\"list-group-item\" diff:ignore></div>"));
    }

    [Fact]
    public void HeaderText_Ok()
    {
        var cut = Context.RenderComponent<ListGroup<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>()
            {
                new() { Name = "Test 1" },
                new() { Name = "Test 1" }
            });
            pb.Add(a => a.GetItemDisplayText, foo => foo.Name ?? "");
            pb.Add(a => a.HeaderText, "Text-Header");
        });
        cut.Contains("Text-Header");
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.RenderComponent<ListGroup<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>()
            {
                new() { Name = "Test 1" },
                new() { Name = "Test 1" }
            });
            pb.Add(a => a.GetItemDisplayText, foo => foo.Name ?? "");
            pb.Add(a => a.HeaderTemplate, new RenderFragment(builder =>
            {
                builder.AddContent(0, "Header-Template");
            }));
        });
        cut.Contains("Header-Template");
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<ListGroup<Foo>>(pb =>
        {
            pb.Add(a => a.Items, new List<Foo>()
            {
                new() { Name = "Test 1" },
                new() { Name = "Test 2" }
            });
            pb.Add(a => a.GetItemDisplayText, foo => foo.Name ?? "");
            pb.Add(a => a.ItemTemplate, foo => builder =>
            {
                builder.AddContent(0, $"{foo.Name} - item - template");
            });
        });
        cut.Contains("Test 1 - item - template");
        cut.Contains("Test 2 - item - template");
    }
}
