// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class PopoverTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Content_Ok()
    {
        var cut = Context.RenderComponent<Popover>(pb =>
        {
            pb.Add(a => a.Title, "test_popover");
            pb.Add(a => a.Content, "test_content");
        });
        Assert.Contains("data-bs-original-title=\"test_popover\"", cut.Markup);
        Assert.Contains("data-bs-toggle=\"popover\"", cut.Markup);
        Assert.Contains("data-bs-placement=\"top\" data-bs-custom-class=\"shadow\" data-bs-trigger=\"focus hover\"", cut.Markup);
    }

    [Fact]
    public void ShowShadow_OK()
    {
        var cut = Context.RenderComponent<Popover>(pb =>
        {
            pb.Add(a => a.Content, "test_content");
            pb.Add(a => a.ShowShadow, false);
        });
        Assert.DoesNotContain("data-bs-custom-class=\"shadow\"", cut.Markup);
    }

    [Fact]
    public void Content_OK()
    {
        var cut = Context.RenderComponent<Popover>(pb =>
        {
            pb.Add(a => a.Title, "test_content");
            pb.Add(a => a.Content, "test_content");
        });
        Assert.Contains("data-bs-original-title=\"test_content\"", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Title, "test");
            pb.Add(a => a.Content, "test");
        });
        Assert.Contains("data-bs-original-title=\"test\"", cut.Markup);
    }
}
