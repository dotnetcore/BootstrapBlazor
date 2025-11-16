// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class AnchorLinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Id_Ok()
    {
        var cut = Context.Render<AnchorLink>(builder => builder.Add(a => a.Id, "anchorlink"));
        Assert.Contains("id=\"anchorlink\"", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        // 未设置 id 时使用父类自动生成 Id
        var cut = Context.Render<AnchorLink>(builder => builder.Add(a => a.Icon, "test-icon"));
        Assert.Contains("test-icon", cut.Markup);

        cut.Render(pb => pb.Add(a => a.Id, "anchorlink"));
        Assert.Contains("test-icon", cut.Markup);
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<AnchorLink>(builder => builder.Add(a => a.Text, "anchorlink"));
        Assert.Contains("anchorlink", cut.Markup);
    }

    [Fact]
    public void TooltipText_Ok()
    {
        var cut = Context.Render<AnchorLink>(builder => builder.Add(a => a.TooltipText, "anchorlink"));
        Assert.Contains("data-bb-title=\"anchorlink\"", cut.Markup);
    }
}
