// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TabLinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Click_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<TabLink>(pb =>
        {
            pb.Add(a => a.Url, "/Cat");
            pb.Add(a => a.Text, "Cat");
            pb.Add(a => a.Icon, "fa fa-fa");
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
        var cut = Context.RenderComponent<TabLink>(pb =>
        {
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Body"));
        });
        Assert.Contains("Body", cut.Markup);
    }
}
