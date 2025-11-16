// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class AnchorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Target_Ok()
    {
        var cut = Context.Render<Anchor>(builder => builder.Add(a => a.Target, "anchor"));
        Assert.Contains("data-bb-target=\"anchor\"", cut.Markup);

        cut = Context.Render<Anchor>(builder => builder.Add(a => a.Target, ""));
        Assert.DoesNotContain("data-bb-target", cut.Markup);
    }

    [Fact]
    public void Container_Ok()
    {
        var cut = Context.Render<Anchor>(builder =>
        {
            builder.Add(a => a.Container, "anchor");
            builder.Add(a => a.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenElement(1, "div");
                builder.AddAttribute(2, "id", "anchor");
                builder.CloseElement();
            }));
        });
        Assert.Contains("data-bb-container=\"anchor\"", cut.Markup);
    }

    [Fact]
    public void IsAnimation_Ok()
    {
        var cut = Context.Render<Anchor>(builder =>
        {
            builder.Add(a => a.IsAnimation, false);
            builder.Add(a => a.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenElement(1, "div");
                builder.AddAttribute(2, "id", "anchor");
                builder.CloseElement();
            }));
        });
        cut.DoesNotContain("data-bb-animation");
        cut.Render(pb =>
        {
            pb.Add(a => a.IsAnimation, true);
        });
        cut.Contains("data-bb-animation=\"true\"");
    }

    [Fact]
    public void Offset_Ok()
    {
        var cut = Context.Render<Anchor>(builder => builder.Add(a => a.Offset, 20));
        Assert.Contains("data-bb-offset=\"20\"", cut.Markup);
    }
}
