// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class AnchorLinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Id_Ok()
    {
        var cut = Context.RenderComponent<AnchorLink>(builder => builder.Add(a => a.Id, "anchorlink"));
        Assert.Contains("id=\"anchorlink\"", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<AnchorLink>(builder => builder.Add(a => a.Icon, "test-icon"));
        Assert.DoesNotContain("test-icon", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.Id, "anchorlink"));
        Assert.Contains("test-icon", cut.Markup);
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<AnchorLink>(builder => builder.Add(a => a.Text, "anchorlink"));
        Assert.Contains("anchorlink", cut.Markup);
    }

    [Fact]
    public void TooltipText_Ok()
    {
        var cut = Context.RenderComponent<AnchorLink>(builder => builder.Add(a => a.TooltipText, "anchorlink"));
        Assert.Contains("anchorlink", cut.Markup);
    }
}
