// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class LightTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsFlash_Ok()
    {
        var cut = Context.RenderComponent<Light>(builder => builder.Add(s => s.IsFlash, true));
        Assert.Contains("flash", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsFlat, true);
        });
        Assert.DoesNotContain("flash", cut.Markup);
        Assert.Contains("is-flat", cut.Markup);
    }

    [Fact]
    public void Tooltip_Ok()
    {
        var cut = Context.RenderComponent<Light>(pb =>
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
        var cut = Context.RenderComponent<Light>(builder => builder.Add(s => s.Color, Color.Success));

        Assert.Contains("light-success", cut.Markup);
    }
}
