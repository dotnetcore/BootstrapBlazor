// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class AnchorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Target_Ok()
    {
        var cut = Context.RenderComponent<Anchor>(builder => builder.Add(a => a.Target, "anchor"));
        Assert.Contains("data-bb-target=\"anchor\"", cut.Markup);

        cut = Context.RenderComponent<Anchor>(builder => builder.Add(a => a.Target, ""));
        Assert.DoesNotContain("data-bb-target", cut.Markup);
    }

    [Fact]
    public void Container_Ok()
    {
        var cut = Context.RenderComponent<Anchor>(builder =>
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
        var cut = Context.RenderComponent<Anchor>(builder =>
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
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsAnimation, true);
        });
        cut.Contains("data-bb-animation=\"true\"");
    }

    [Fact]
    public void Offset_Ok()
    {
        var cut = Context.RenderComponent<Anchor>(builder => builder.Add(a => a.Offset, 20));
        Assert.Contains("data-bb-offset=\"20\"", cut.Markup);
    }
}
