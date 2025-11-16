// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class LinkButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<LinkButton>(builder => builder.Add(s => s.Text, "TestButton"));

        Assert.Contains("TestButton", cut.Markup);
    }

    [Fact]
    public void Url_Ok()
    {
        var cut = Context.Render<LinkButton>(builder => builder.Add(s => s.Url, "https://www.blazor.zone"));

        Assert.Contains("https://www.blazor.zone", cut.Markup);
    }

    [Fact]
    public void ImageUrl_Ok()
    {
        var cut = Context.Render<LinkButton>(builder => builder.Add(s => s.ImageUrl, "Argo-C.png"));

        Assert.Contains("Argo-C.png", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.Render<LinkButton>(builder => builder.Add(s => s.Icon, "fa-solid fa-font-awesome"));

        Assert.Contains("fa-solid fa-font-awesome", cut.Markup);

        Assert.Contains("link-primary", cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<LinkButton>(builder => builder.Add(s => s.Color, Color.None));

        Assert.DoesNotContain("link-primary", cut.Markup);

        cut.Render(pb => pb.Add(a => a.Color, Color.Danger));
        cut.WaitForAssertion(() => Assert.Contains("link-danger", cut.Markup));

        cut.Render(pb => pb.Add(a => a.IsDisabled, true));
        cut.WaitForAssertion(() => Assert.DoesNotContain("link-danger", cut.Markup));
    }

    [Fact]
    public void Title_Ok()
    {
        var cut = Context.Render<LinkButton>(builder =>
        {
            builder.Add(s => s.TooltipText, "Tooltip");
            builder.Add(s => s.TooltipPlacement, Placement.Bottom);
        });
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<LinkButton>(builder => builder.Add(s => s.ChildContent, b =>
        {
            b.AddContent(0, new MarkupString("<div>Test</div>"));
        }));

        Assert.Contains("<div>Test</div>", cut.Markup);
    }

    [Fact]
    public async Task OnClick_Ok()
    {
        var click = false;
        var cut = Context.Render<LinkButton>(pb =>
        {
            pb.Add(a => a.IsAsync, true);
            pb.Add(s => s.OnClick, async () =>
            {
                click = true;
                await Task.Yield();
            });
        });

        var link = cut.Find("a");
        await cut.InvokeAsync(() => link.Click());
        Assert.True(click);
    }

    [Fact]
    public void OnClickWithoutRender_Ok()
    {
        var click = false;
        var cut = Context.Render<LinkButton>(builder => builder.Add(s => s.OnClickWithoutRender, () =>
        {
            click = true;
            return Task.CompletedTask;
        }));

        cut.InvokeAsync(() =>
        {
            cut.Find("a").Click();
            Assert.True(click);
        });
    }

    [Fact]
    public void IsVertical_Ok()
    {
        var cut = Context.Render<LinkButton>(pb =>
        {
            pb.Add(a => a.IsVertical, true);
        });
        cut.Contains("btn-vertical");
    }

    [Fact]
    public void Disabled_Ok()
    {
        var cut = Context.Render<LinkButton>(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("button");
    }

    [Fact]
    public void ImageCss_Ok()
    {
        var cut = Context.Render<LinkButton>(pb =>
        {
            pb.Add(a => a.ImageUrl, "test-img");
            pb.Add(a => a.ImageCss, "image-css");
        });
        cut.Contains("image-css");
    }

    [Fact]
    public void Target_Ok()
    {
        var cut = Context.Render<LinkButton>(pb =>
        {
            pb.Add(a => a.Target, "_blank");
        });
        cut.Contains("target=\"_blank\"");
    }
}
