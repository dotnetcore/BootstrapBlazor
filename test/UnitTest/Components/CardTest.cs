// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class CardTest : BootstrapBlazorTestBase
{
    private const string Content = "TestComponent-Card";

    [Fact]
    public void Header_Ok()
    {
        var cut = Context.Render<Card>(builder => builder.Add(a => a.HeaderTemplate, CreateComponent()));
        Assert.Contains(Content, cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.HeaderPaddingY, "0.25rem");
        });
        cut.Contains("--bs-card-cap-padding-y: 0.25rem;");
    }

    [Fact]
    public void Body_Ok()
    {
        var cut = Context.Render<Card>(builder => builder.Add(a => a.BodyTemplate, CreateComponent()));
        Assert.Contains(Content, cut.Markup);
    }

    [Fact]
    public void Footer_Ok()
    {
        var cut = Context.Render<Card>(builder => builder.Add(a => a.FooterTemplate, CreateComponent()));
        Assert.Contains(Content, cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Card>(builder => builder.Add(a => a.Color, Color.Primary));
        Assert.Contains("text-primary", cut.Markup);
    }

    [Fact]
    public void IsCenter_Ok()
    {
        var cut = Context.Render<Card>(builder => builder.Add(a => a.IsCenter, true));
        Assert.Contains("text-center", cut.Markup);
    }

    [Fact]
    public void IsShadow_Ok()
    {
        var cut = Context.Render<Card>(builder => builder.Add(a => a.IsShadow, true));
        Assert.Contains("shadow", cut.Markup);
    }

    [Fact]
    public void HeaderText_Ok()
    {
        var cut = Context.Render<Card>(builder => builder.Add(a => a.HeaderText, "Header"));
        Assert.Contains("Header", cut.Markup);
    }

    [Fact]
    public void IsCollapsible_Ok()
    {
        var cut = Context.Render<Card>(builder =>
        {
            builder.Add(a => a.IsCollapsible, true);
            builder.Add(a => a.HeaderText, "Header");
        });
        Assert.Contains("card-collapse", cut.Markup);
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.Render<Card>(builder =>
        {
            builder.Add(a => a.IsCollapsible, true);
            builder.Add(a => a.HeaderText, "Header");
            builder.Add(a => a.HeaderTemplate, CreateComponent());
        });
        Assert.Contains("card-collapse", cut.Markup);
        Assert.Contains("TestComponent-Card", cut.Markup);
    }

    [Fact]
    public void Collapsed_Ok()
    {
        bool collapsed = false;
        var cut = Context.Render<Card>(builder =>
        {
            builder.Add(a => a.IsCollapsible, true);
            builder.Add(a => a.HeaderText, "Header");
            builder.Add(a => a.Collapsed, true);
            builder.Add(a => a.CollapsedChanged, v =>
            {
                collapsed = v;
            });
        });
        Assert.Contains("data-bs-toggle=\"collapse\"", cut.Markup);
        Assert.Contains("collapse", cut.Markup);

        cut.Render(pb => pb.Add(a => a.Collapsed, false));
        Assert.Contains("collapse show", cut.Markup);


        cut.InvokeAsync(async () =>
        {
            await cut.Instance.ToggleCollapse(true);
            Assert.True(cut.Instance.Collapsed);
        });
    }

    private static RenderFragment CreateComponent() => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddContent(1, Content);
        builder.CloseElement();
    };
}
