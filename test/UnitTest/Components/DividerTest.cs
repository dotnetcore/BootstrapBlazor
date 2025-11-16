// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DividerTest : TestBase
{
    [Fact]
    public void TextIcon_Ok()
    {
        var cut = Context.Render<Divider>(pb =>
        {
            pb.Add(a => a.Text, "Test");
            pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
        });
        Assert.Contains("Test", cut.Markup);
        Assert.Contains("fa-solid fa-font-awesome", cut.Markup);
    }

    [Fact]
    public void IsVertical_Ok()
    {
        var cut = Context.Render<Divider>(pb =>
        {
            pb.Add(a => a.IsVertical, true);
            pb.Add(a => a.Text, "Test");
        });
        Assert.Contains("divider-vertical", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<Divider>(pb =>
        {
            pb.Add(a => a.ChildContent, new RenderFragment(builder => builder.AddContent(0, "Test")));
        });
        Assert.Contains("Test", cut.Markup);
    }

    [Fact]
    public void Alignment_Ok()
    {
        var cut = Context.Render<Divider>(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Left);
            pb.Add(a => a.Text, "Test");
        });
        Assert.Contains("is-left", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Center);
        });
        Assert.Contains("is-center", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Right);
        });
        Assert.Contains("is-right", cut.Markup);
    }
}
