// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Web;

namespace UnitTest.Components;

public class GoTopTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Target_Ok()
    {
        var cut = Context.Render<GoTop>(pb =>
        {
            pb.Add(c => c.Target, "#top");
        });
        Assert.Equal("#top", cut.Instance.Target);
        Assert.Contains("data-bb-target", cut.Markup);
    }

    [Fact]
    public void TooltipText_Ok()
    {
        var cut = Context.Render<GoTop>();
        Assert.Contains("返回顶端", HttpUtility.HtmlDecode(cut.Markup));

        cut.Render(pb =>
        {
            pb.Add(c => c.TooltipText, "TooltipText");
        });
        Assert.Contains("TooltipText", cut.Markup);
    }

    [Fact]
    public void Behavior_Ok()
    {
        var cut = Context.Render<GoTop>(pb =>
        {
            pb.Add(c => c.ScrollBehavior, ScrollIntoViewBehavior.Smooth);
        });
        Assert.DoesNotContain("data-bb-behavior", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.ScrollBehavior, ScrollIntoViewBehavior.Auto);
        });
        Assert.Contains("data-bb-behavior=\"auto\"", cut.Markup);
    }
}
