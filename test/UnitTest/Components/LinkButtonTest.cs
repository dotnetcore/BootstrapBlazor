// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class LinkButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.Text, "TestButton"));

        Assert.Contains("TestButton", cut.Markup);
    }

    [Fact]
    public void Url_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.Url, "https://www.blazor.zone"));

        Assert.Contains("https://www.blazor.zone", cut.Markup);
    }

    [Fact]
    public void ImageUrl_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.ImageUrl, "Argo-C.png"));

        Assert.Contains("Argo-C.png", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.Icon, "fa fa-fa"));

        Assert.Contains("fa fa-fa", cut.Markup);

        Assert.Contains("link-primary", cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.Color, Color.None));

        Assert.DoesNotContain("link-primary", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.Color, Color.Danger));
        Assert.Contains("link-danger", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsDisabled, true));
        Assert.DoesNotContain("link-danger", cut.Markup);
    }

    [Fact]
    public void Title_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(builder =>
        {
            builder.Add(s => s.Title, "Tooltip");
            builder.Add(s => s.TooltipPlacement, Placement.Bottom);
        });
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.ChildContent, b =>
        {
            b.AddContent(0, new MarkupString("<div>Test</div>"));
        }));

        Assert.Contains("<div>Test</div>", cut.Markup);
    }

    [Fact]
    public void OnClick_Ok()
    {
        var click = false;
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.OnClick, () => click = true));

        cut.Find("a").Click();
        Assert.True(click);
    }

    [Fact]
    public void OnClickWithoutRender_Ok()
    {
        var click = false;
        var cut = Context.RenderComponent<LinkButton>(builder => builder.Add(s => s.OnClickWithoutRender, () =>
        {
            click = true;
            return Task.CompletedTask;
        }));

        cut.Find("a").Click();
        Assert.True(click);
    }

    [Fact]
    public void IsVertical_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(pb =>
        {
            pb.Add(a => a.IsVertical, true);
        });
        cut.Contains("btn-vertical");
    }

    [Fact]
    public void Disabled_Ok()
    {
        var cut = Context.RenderComponent<LinkButton>(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("button");
    }
}
