// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ScrollTest : TestBase
{
    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Scroll>(builder => builder.Add(a => a.Height, "500px"));

        Assert.Contains("height: 500px;", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Scroll>(builder => builder.Add(a => a.Width, "500px"));

        Assert.Contains("width: 500px;", cut.Markup);
    }

    [Fact]
    public void ScrollWidth_Ok()
    {
        var cut = Context.RenderComponent<Scroll>(builder =>
        {
            builder.Add(a => a.Width, "500px");
        });

        Assert.Equal("<div class=\"scroll\" style=\"width: 500px; --bb-scroll-width: 5px; --bb-scroll-hover-width: 5px;\"></div>", cut.Markup);

        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ScrollWidth, 6);
        });
        Assert.Equal("<div class=\"scroll\" style=\"width: 500px; --bb-scroll-width: 6px; --bb-scroll-hover-width: 5px;\"></div>", cut.Markup);

        cut.SetParametersAndRender(builder =>
        {
            builder.Add(a => a.ScrollWidth, 6);
            builder.Add(a => a.ScrollHoverWidth, 12);
        });
        Assert.Equal("<div class=\"scroll\" style=\"width: 500px; --bb-scroll-width: 6px; --bb-scroll-hover-width: 12px;\"></div>", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Scroll>(builder => builder.Add(a => a.ChildContent, r =>
        {
            r.OpenElement(0, "div");
            r.AddContent(1, "I am scroll");
            r.CloseComponent();
        }));

        Assert.Contains("I am scroll", cut.Markup);
    }
}
