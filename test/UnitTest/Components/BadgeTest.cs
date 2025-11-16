// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class BadgeTest : TestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Badge>(builder => builder.Add(a => a.Color, Color.Primary));
        Assert.Contains("primary", cut.Markup);
    }

    [Fact]
    public void IsPill_Ok()
    {
        var cut = Context.Render<Badge>(builder => builder.Add(a => a.IsPill, true));
        Assert.Contains("rounded-pill", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<Badge>(builder => builder.Add(a => a.ChildContent, CreateComponent()));
        Assert.Contains("badge", cut.Markup);

        static RenderFragment CreateComponent() => builder =>
        {
            builder.OpenElement(1, "div");
            builder.AddContent(2, "badge");
            builder.CloseComponent();
        };
    }
}
