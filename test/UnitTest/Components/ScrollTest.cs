// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ScrollTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Height_Ok()
    {
        var cut = Context.Render<Scroll>(builder => builder.Add(a => a.Height, "500px"));

        Assert.Contains("height: 500px;", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.Render<Scroll>(builder => builder.Add(a => a.Width, "500px"));

        Assert.Contains("width: 500px;", cut.Markup);
    }

    [Fact]
    public void ScrollWidth_Ok()
    {
        var cut = Context.Render<Scroll>(builder =>
        {
            builder.Add(a => a.Width, "500px");
        });

        Assert.Contains("style=\"width: 500px; --bb-scroll-width: 5px; --bb-scroll-hover-width: 5px;\"", cut.Markup);

        cut.Render(builder =>
        {
            builder.Add(a => a.ScrollWidth, 6);
        });
        Assert.Contains("style=\"width: 500px; --bb-scroll-width: 6px; --bb-scroll-hover-width: 5px;\"", cut.Markup);

        cut.Render(builder =>
        {
            builder.Add(a => a.ScrollWidth, 6);
            builder.Add(a => a.ScrollHoverWidth, 12);
        });
        Assert.Contains("style=\"width: 500px; --bb-scroll-width: 6px; --bb-scroll-hover-width: 12px;\"", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<Scroll>(builder => builder.Add(a => a.ChildContent, r =>
        {
            r.OpenElement(0, "div");
            r.AddContent(1, "I am scroll");
            r.CloseComponent();
        }));

        Assert.Contains("I am scroll", cut.Markup);
    }

    [Theory]
    [InlineData("bottom")]
    [InlineData("top")]
    [InlineData("left")]
    [InlineData("right")]
    public async Task ScrollTo_Ok(string direction)
    {
        var cut = Context.Render<Scroll>();
        var handler = Context.JSInterop.SetupVoid("scrollTo", cut.Instance.Id, direction);
        handler.SetVoidResult();

        await cut.InvokeAsync(() => cut.Instance.ScrollTo(Enum.Parse<ScrollDirection>(direction, true)));

        handler.VerifyInvoke("scrollTo");
    }
}
