// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class CircleTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Value_Ok()
    {
        var cut = Context.Render<Circle>(builder => builder.Add(a => a.Value, 100));

        Assert.Contains("100%", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.Render<Circle>(builder => builder.Add(a => a.Width, 100));

        Assert.Contains("width: 100px", cut.Markup);
    }

    [Fact]
    public void StrokeWidth_Ok()
    {
        var cut = Context.Render<Circle>(builder => builder.Add(a => a.StrokeWidth, 5));

        Assert.Contains("stroke-width=\"5\"", cut.Markup);

        // 增加代码覆盖率
        //Width / 2 < StrokeWidth
        cut.Render(pb =>
        {
            pb.Add(a => a.Width, 6);
            pb.Add(a => a.StrokeWidth, 6);
        });
        Assert.Equal(2, cut.Instance.StrokeWidth);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Circle>(builder => builder.Add(a => a.Color, Color.Success));

        var element = cut.Find(".circle-success");

        Assert.NotNull(element);
    }

    [Fact]
    public void ShowProgress_Ok()
    {
        var cut = Context.Render<Circle>(builder =>
        {
            builder.Add(a => a.Value, 75);
            builder.Add(a => a.ShowProgress, true);
        });
        cut.Contains("75%");

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowProgress, false);
        });
        cut.DoesNotContain("75%");
    }

    [Fact]
    public void Value_ChildContent()
    {
        var cut = Context.Render<Circle>(builder => builder.Add(a => a.ChildContent, s =>
        {
            s.OpenElement(1, "div");
            s.AddContent(2, "I am cricle");
            s.CloseElement();
        }));

        Assert.Contains("I am cricle", cut.Markup);
    }
}
