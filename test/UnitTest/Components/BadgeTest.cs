// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class BadgeTest : TestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Badge>(builder => builder.Add(a => a.Color, Color.Primary));
        Assert.Contains("primary", cut.Markup);
    }

    [Fact]
    public void IsPill_Ok()
    {
        var cut = Context.RenderComponent<Badge>(builder => builder.Add(a => a.IsPill, true));
        Assert.Contains("rounded-pill", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Badge>(builder => builder.Add(a => a.ChildContent, CreateComponent()));
        Assert.Contains("badge", cut.Markup);

        static RenderFragment CreateComponent() => builder =>
        {
            builder.OpenElement(1, "div");
            builder.AddContent(2, "badge");
            builder.CloseComponent();
        };
    }
}
