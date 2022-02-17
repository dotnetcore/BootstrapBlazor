// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CarouselTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Images_Ok()
    {
        var cut = Context.RenderComponent<Carousel>();
        Assert.Contains("carousel slide", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Carousel>(pb =>
        {
            pb.Add(b => b.Width, 100);
        });

        Assert.Contains("width: 100px;", cut.Markup);
    }

    [Fact]
    public void IsFade_Ok()
    {
        var cut = Context.RenderComponent<Carousel>(pb =>
        {
            pb.Add(b => b.IsFade, false);
        });
        Assert.DoesNotContain("carousel-fade", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.IsFade, true);
        });
        Assert.Contains("carousel-fade", cut.Markup);
    }

    [Fact]
    public void OnClick_Ok()
    {
        var url = "";
        var cut = Context.RenderComponent<Carousel>(pb =>
        {
            pb.Add(b => b.Images, new List<string>() { "test1.jpg", null!, "test3.jpg", "test4.jpg" });
        });
        cut.InvokeAsync(() => cut.Find("img").Click());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.OnClick, v =>
            {
                url = v;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Find("img").Click());
        Assert.Equal("test1.jpg", url);

        cut.InvokeAsync(() => cut.FindAll("img")[1].Click());
        Assert.Equal("", url);
    }
}
