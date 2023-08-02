// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CardTest : BootstrapBlazorTestBase
{
    private const string Content = "TestComponent-Card";

    [Fact]
    public void Header_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder => builder.Add(a => a.HeaderTemplate, CreateComponent()));
        Assert.Contains(Content, cut.Markup);
    }

    [Fact]
    public void Body_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder => builder.Add(a => a.BodyTemplate, CreateComponent()));
        Assert.Contains(Content, cut.Markup);
    }

    [Fact]
    public void Footer_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder => builder.Add(a => a.FooterTemplate, CreateComponent()));
        Assert.Contains(Content, cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder => builder.Add(a => a.Color, Color.Primary));
        Assert.Contains("text-primary", cut.Markup);
    }

    [Fact]
    public void IsCenter_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder => builder.Add(a => a.IsCenter, true));
        Assert.Contains("text-center", cut.Markup);
    }

    [Fact]
    public void IsShadow_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder => builder.Add(a => a.IsShadow, true));
        Assert.Contains("shadow", cut.Markup);
    }

    [Fact]
    public void HeaderText_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder => builder.Add(a => a.HeaderText, "Header"));
        Assert.Contains("Header", cut.Markup);
    }

    [Fact]
    public void IsCollapsible_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder =>
        {
            builder.Add(a => a.IsCollapsible, true);
            builder.Add(a => a.HeaderText, "Header");
        });
        Assert.Contains("card-collapse", cut.Markup);
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.RenderComponent<Card>(builder =>
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
        var cut = Context.RenderComponent<Card>(builder =>
        {
            builder.Add(a => a.IsCollapsible, true);
            builder.Add(a => a.HeaderText, "Header");
            builder.Add(a => a.Collapsed, true);
        });
        Assert.Contains("data-bs-toggle=\"collapse\"", cut.Markup);
        Assert.Contains("collapse", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.Collapsed, false));
        Assert.Contains("collapse show", cut.Markup);

        cut.InvokeAsync(() =>
        {
            var header = cut.Find(".card-collapse-bar");
            header.Click();
            Assert.True(cut.Instance.Collapsed);

            header.Click();
            Assert.False(cut.Instance.Collapsed);
        });
    }

    private static RenderFragment CreateComponent() => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddContent(1, Content);
        builder.CloseElement();
    };
}
