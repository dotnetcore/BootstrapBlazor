// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TimelineTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Css_Ok()
    {
        var cut = Context.RenderComponent<Timeline>(builder =>
        {
            builder.Add(x => x.IsLeft, true);
        });
        Assert.Contains("is-left", cut.Markup);

        cut.SetParametersAndRender(builder =>
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
            new TimelineItem()
            {
                Color = Color.Danger, Content = "first item", Description = "first description", Icon = "fa fa-home"
            },
            new TimelineItem()
            {
                Color = Color.None, Content = "no color item", Description = "first description", Icon = "fa fa-home"
            },
            new TimelineItem()
            {
                Color = Color.Dark, Component = BootstrapDynamicComponent.CreateComponent<Card>()
            }
        };

        var cut = Context.RenderComponent<Timeline>();

        // Null Items
        Assert.Contains("timeline", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(x => x.Items, items);
            pb.Add(x => x.IsReverse, true);
        });
        var html = cut.Markup;
        Assert.Contains("first item", html);
        Assert.Contains("first description", html);
        Assert.Contains("text-danger", html);
        Assert.Contains("fa fa-home", html);
        Assert.Contains("card-body", html);
        Assert.Matches("bg-dark(.*?)text-danger", html.Replace("\r", "").Replace("\n", ""));
    }
}
