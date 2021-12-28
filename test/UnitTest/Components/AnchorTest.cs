// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class AnchorTest : TestBase
{
    [Fact]
    public void Target_Ok()
    {
        var cut = Context.RenderComponent<Anchor>(builder => builder.Add(a => a.Target, "anchor"));
        Assert.Contains("data-target=\"#anchor\"", cut.Markup);

        cut = Context.RenderComponent<Anchor>(builder => builder.Add(a => a.Target, ""));
        Assert.DoesNotContain("data-target", cut.Markup);
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
        Assert.Contains("data-container=\"anchor\"", cut.Markup);
    }

    [Fact]
    public void Offset_Ok()
    {
        var cut = Context.RenderComponent<Anchor>(builder => builder.Add(a => a.Offset, 20));
        Assert.Contains("data-offset=\"20\"", cut.Markup);
    }
}
