// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TextareaTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsShowLabel_OK()
    {
        var cut = Context.RenderComponent<Textarea>(builder => builder.Add(s => s.ShowLabel, true));

        var component = cut.FindComponent<BootstrapLabel>();
        Assert.NotNull(component);
    }

    [Fact]
    public void ShowLabelTooltip_OK()
    {
        var cut = Context.RenderComponent<BootstrapLabel>(pb =>
        {
            pb.Add(a => a.ShowLabelTooltip, null);
        });
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLabelTooltip, false);
        });
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "Test");
            pb.Add(a => a.ShowLabelTooltip, true);
        });
    }

    [Fact]
    public void AutoScrollString_OK()
    {
        var cut = Context.RenderComponent<Textarea>(builder =>
        {
            builder.Add(a => a.IsAutoScroll, true);
        });
        Assert.Contains("data-bb-scroll=\"auto\"", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsAutoScroll, false);
        });
        Assert.DoesNotContain("data-bb-scroll=\"auto\"", cut.Markup);
    }

   
}
