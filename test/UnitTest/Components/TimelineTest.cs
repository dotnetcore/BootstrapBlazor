// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TimelineTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Css_Ok()
    {
        var cut = Context.Render<Timeline>(builder =>
        {
            builder.Add(x => x.IsLeft, true);
        });
        Assert.Contains("is-left", cut.Markup);

        cut.Render(builder =>
        {
            builder.Add(x => x.IsLeft, false);
            builder.Add(x => x.IsAlternate, true);
        });
        Assert.DoesNotContain("is-left", cut.Markup);
        Assert.Contains("is-alternate", cut.Markup);
    }

    [Fact]
    public void Items_Ok()
    {
        var items = new List<TimelineItem>()
        {
            new()
            {
                Color = Color.Danger, Content = "first item", Description = "first description", Icon = "fa-solid fa-house"
            },
            new()
            {
                Color = Color.None, Content = "no color item", Description = "first description", Icon = "fa-solid fa-house"
            },
            new()
            {
                Color = Color.Dark, Component = BootstrapDynamicComponent.CreateComponent<Card>()
            }
        };

        var cut = Context.Render<Timeline>();

        // Null Items
        Assert.Contains("timeline", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(x => x.Items, items);
            pb.Add(x => x.IsReverse, true);
        });
        var html = cut.Markup;
        Assert.Contains("first item", html);
        Assert.Contains("first description", html);
        Assert.Contains("text-danger", html);
        Assert.Contains("fa-solid fa-house", html);
        Assert.Contains("card-body", html);
        Assert.Matches("bg-dark(.*?)text-danger", html.Replace("\r", "").Replace("\n", ""));
    }

    [Fact]
    public void ItemDescriptionTemplate_Ok()
    {
        var items = new List<TimelineItem>()
        {
            new()
            {
                Color = Color.Danger, Content = "first item", Icon = "fa-solid fa-house", DescriptionTemplate = pb =>
                {
                    pb.OpenElement(0, "div");
                    pb.AddContent(1, "first description template");
                    pb.CloseElement();
                }
            },
            new()
            {
                Color = Color.None, Content = "no color item", Description = "first description", Icon = "fa-solid fa-house"
            }
        };

        var cut = Context.Render<Timeline>(pb =>
        {
            pb.Add(x => x.Items, items);
        });
        Assert.Contains("first description template", cut.Markup);
    }
}
