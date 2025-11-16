// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TabLinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Click_Ok()
    {
        var clicked = false;
        var cut = Context.Render<TabLink>(pb =>
        {
            pb.Add(a => a.Url, "/Cat");
            pb.Add(a => a.Text, "Cat");
            pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
            pb.Add(a => a.Closable, false);
            pb.Add(a => a.OnClick, () =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        cut.Find("a").Click();
        Assert.True(clicked);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<TabLink>(pb =>
        {
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Body"));
        });
        Assert.Contains("Body", cut.Markup);
    }
}
