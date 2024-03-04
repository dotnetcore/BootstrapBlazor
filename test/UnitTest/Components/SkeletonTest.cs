// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SkeletonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Circle_Ok()
    {
        var cut = Context.RenderComponent<SkeletonAvatar>(buildr => buildr.Add(s => s.Circle, true));

        var ele = cut.Find(".circle");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Round_Ok()
    {
        var cut = Context.RenderComponent<SkeletonAvatar>(buildr => buildr.Add(s => s.Round, true));

        var ele = cut.Find(".round");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Active_Ok()
    {
        var cut = Context.RenderComponent<SkeletonAvatar>(buildr => buildr.Add(s => s.Active, true));

        var ele = cut.Find(".active");
        Assert.NotNull(ele);
    }

    [Fact]
    public void ShowToolbar_Ok()
    {
        var cut = Context.RenderComponent<SkeletonTable>(buildr => buildr.Add(s => s.ShowToolbar, true));

        var ele = cut.Find(".skeleton-toolbar");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Rows_Ok()
    {
        var cut = Context.RenderComponent<SkeletonTable>(buildr => buildr.Add(s => s.Rows, 10));

        var count = cut.FindAll(".skeleton-row").Count;
        Assert.Equal(10, count);
    }

    [Fact]
    public void Columns_Ok()
    {
        var cut = Context.RenderComponent<SkeletonTable>(buildr => buildr.Add(s => s.Columns, 10));

        var count = cut.FindAll(".skeleton-col").Count;
        Assert.Equal(70, count);
    }

    [Fact]
    public void Tree_Ok()
    {
        var cut = Context.RenderComponent<SkeletonTree>();

        var ele = cut.Find(".tree");
        Assert.NotNull(ele);
    }
}
