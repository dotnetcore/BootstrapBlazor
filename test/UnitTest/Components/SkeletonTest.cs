// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SkeletonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Circle_Ok()
    {
        var cut = Context.Render<SkeletonAvatar>(buildr => buildr.Add(s => s.Circle, true));

        var ele = cut.Find(".circle");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Round_Ok()
    {
        var cut = Context.Render<SkeletonAvatar>(buildr => buildr.Add(s => s.Round, true));

        var ele = cut.Find(".round");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Active_Ok()
    {
        var cut = Context.Render<SkeletonAvatar>(buildr => buildr.Add(s => s.Active, true));

        var ele = cut.Find(".active");
        Assert.NotNull(ele);
    }

    [Fact]
    public void ShowToolbar_Ok()
    {
        var cut = Context.Render<SkeletonTable>(buildr => buildr.Add(s => s.ShowToolbar, true));

        var ele = cut.Find(".skeleton-toolbar");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Rows_Ok()
    {
        var cut = Context.Render<SkeletonTable>(buildr => buildr.Add(s => s.Rows, 10));

        var count = cut.FindAll(".skeleton-row").Count;
        Assert.Equal(10, count);
    }

    [Fact]
    public void Columns_Ok()
    {
        var cut = Context.Render<SkeletonTable>(buildr => buildr.Add(s => s.Columns, 10));

        var count = cut.FindAll(".skeleton-col").Count;
        Assert.Equal(70, count);
    }

    [Fact]
    public void Tree_Ok()
    {
        var cut = Context.Render<SkeletonTree>();

        var ele = cut.Find(".tree");
        Assert.NotNull(ele);
    }
}
