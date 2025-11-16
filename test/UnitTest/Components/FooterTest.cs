// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class FooterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Footer_Ok()
    {
        var cut = Context.Render<Footer>(pb =>
        {
            pb.Add(a => a.Text, "test-footer-text");
            pb.Add(a => a.Target, "#test-footer-target1");
        });
        Assert.Contains("test-footer-text", cut.Markup);
    }

    [Fact]
    public void Footer_ShowGoto()
    {
        var cut = Context.Render<Footer>(pb =>
        {
            pb.Add(a => a.Text, "test-footer-text");
            pb.Add(a => a.ShowGoto, true);
        });
        Assert.Contains("layout-gotop", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowGoto, false);
        });
        Assert.DoesNotContain("layout-gotop", cut.Markup);
    }

    [Fact]
    public void Footer_ChildContent()
    {
        var cut = Context.Render<Footer>(pb =>
        {
            pb.Add(a => a.Text, "test-footer-text");
            pb.Add(a => a.ChildContent, builder =>
            {
                builder.AddContent(0, "test-child-content");
            });
        });
        Assert.Contains("test-child-content", cut.Markup);
        Assert.DoesNotContain("test-footer-text", cut.Markup);
    }
}
