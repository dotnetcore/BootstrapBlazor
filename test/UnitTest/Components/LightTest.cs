// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class LightTest : TestBase
{
    [Fact]
    public void IsFlash_Ok()
    {
        var cut = Context.RenderComponent<Light>(builder => builder.Add(s => s.IsFlash, true));

        Assert.Contains("flash", cut.Markup);
    }

    [Fact]
    public void Title_Ok()
    {
        var cut = Context.RenderComponent<Light>(builder => builder.Add(s => s.Title, "I am Light"));

        Assert.Contains("I am Light", cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Light>(builder => builder.Add(s => s.Color, Color.Success));

        Assert.Contains("light-success", cut.Markup);
    }
}
