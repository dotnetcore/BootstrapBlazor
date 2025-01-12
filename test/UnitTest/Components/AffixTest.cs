﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class AffixTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Position_Ok()
    {
        var cut = Context.RenderComponent<Affix>(pb =>
        {
            pb.Add(a => a.Offset, 100);
            pb.Add(a => a.ZIndex, 10);
            pb.Add(a => a.Position, AffixPosition.Bottom);
            pb.Add(a => a.ChildContent, pb => pb.AddMarkupContent(0, "<button>Test</button>"));
        });
        Assert.Contains("bb-affix", cut.Markup);
        Assert.Contains("position: sticky;", cut.Markup);
        Assert.Contains("z-index: 10;", cut.Markup);
        Assert.Contains("bottom: 100px;", cut.Markup);
    }
}