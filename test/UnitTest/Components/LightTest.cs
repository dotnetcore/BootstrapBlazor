// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class LightTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsFlash_Ok()
    {
        var cut = Context.Render<Light>(builder => builder.Add(s => s.IsFlash, true));
        Assert.Contains("bb-light flash", cut.Markup);
        Assert.DoesNotContain("is-flash-flat", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.IsFlat, true);
        });
        Assert.DoesNotContain("bb-light flash", cut.Markup);
        Assert.Contains("is-flat", cut.Markup);
        Assert.Contains("is-flat-flash", cut.Markup);
    }

    [Fact]
    public void Tooltip_Ok()
    {
        var cut = Context.Render<Light>(pb =>
        {
            pb.Add(s => s.TooltipText, "I am Light");
            pb.Add(s => s.TooltipTrigger, "focus");
            pb.Add(s => s.TooltipPlacement, Placement.Top);
        });

        var tooltip = cut.FindComponent<Tooltip>();
        Assert.Contains("data-bs-placement=\"top\"", cut.Markup);
        Assert.Contains("data-bs-trigger=\"focus\"", cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Light>(builder => builder.Add(s => s.Color, Color.Success));

        Assert.Contains("light-success", cut.Markup);
    }
}
