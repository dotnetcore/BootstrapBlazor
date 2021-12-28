// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class SpinnerTest : TestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Spinner>(builder => builder.Add(s => s.Color, Color.Primary));

        Assert.Contains("text-primary", cut.Markup);
    }

    [Fact]
    public void Size_Ok()
    {
        var cut = Context.RenderComponent<Spinner>(builder => builder.Add(s => s.Size, Size.Small));

        Assert.Contains("spinner-border-sm", cut.Markup);
    }

    [Fact]
    public void SpinnerType_Ok()
    {
        var cut = Context.RenderComponent<Spinner>(builder => builder.Add(s => s.SpinnerType, SpinnerType.Grow));

        Assert.Contains("spinner-grow", cut.Markup);
    }
}
