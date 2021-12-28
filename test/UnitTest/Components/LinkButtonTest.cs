// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using UnitTest.Core;
using Xunit;

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
}
