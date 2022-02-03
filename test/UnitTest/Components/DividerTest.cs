// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class DividerTest : TestBase
{
    [Fact]
    public void TextIcon_Ok()
    {
        var cut = Context.RenderComponent<Divider>(pb =>
        {
            pb.Add(a => a.Text, "Test");
            pb.Add(a => a.Icon, "fa fa-fa");
        });
        Assert.Contains("Test", cut.Markup);
        Assert.Contains("fa fa-fa", cut.Markup);
    }

    [Fact]
    public void IsVertical_Ok()
    {
        var cut = Context.RenderComponent<Divider>(pb =>
        {
            pb.Add(a => a.IsVertical, true);
            pb.Add(a => a.Text, "Test");
        });
        Assert.Contains("divider-vertical", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Divider>(pb =>
        {
            pb.Add(a => a.ChildContent, new RenderFragment(builder => builder.AddContent(0, "Test")));
        });
        Assert.Contains("Test", cut.Markup);
    }

    [Fact]
    public void Alignment_Ok()
    {
        var cut = Context.RenderComponent<Divider>(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Left);
            pb.Add(a => a.Text, "Test");
        });
        Assert.Contains("is-left", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Center);
        });
        Assert.Contains("is-center", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Right);
        });
        Assert.Contains("is-right", cut.Markup);
    }
}
