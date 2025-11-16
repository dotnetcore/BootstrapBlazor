// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class MarqueeTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Marquee_Ok()
    {
        var cut = Context.Render<Marquee>();
        Assert.Equal("<div class=\"marquee\" style=\"background-color: #fff; color: #000; font-size: 72px;\"><span class=\"marquee-text marquee-text-left\" style=\"animation-duration: 14s; animation-name: LeftToRight;\"></span></div>", cut.Markup);
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<Marquee>(pb =>
        {
            pb.Add(a => a.Text, "Test");
        });
        Assert.Equal("<div class=\"marquee\" style=\"background-color: #fff; color: #000; font-size: 72px;\"><span class=\"marquee-text marquee-text-left\" style=\"animation-duration: 14s; animation-name: LeftToRight;\">Test</span></div>", cut.Markup);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Marquee>(pb =>
        {
            pb.Add(a => a.Text, "Test");
            pb.Add(a => a.Color, "#ddd");
        });
        Assert.Contains("color: #ddd;", cut.Markup);
    }

    [Fact]
    public void BackgroundColor_Ok()
    {
        var cut = Context.Render<Marquee>(pb =>
        {
            pb.Add(a => a.Text, "Test");
            pb.Add(a => a.BackgroundColor, "#ddd");
        });
        Assert.Contains("background-color: #ddd;", cut.Markup);
    }

    [Fact]
    public void FontSize_Ok()
    {
        var cut = Context.Render<Marquee>(pb =>
        {
            pb.Add(a => a.Text, "Test");
            pb.Add(a => a.FontSize, 18);
        });
        Assert.Contains("font-size: 18px;", cut.Markup);
    }

    [Fact]
    public void Duration_Ok()
    {
        var cut = Context.Render<Marquee>(pb =>
        {
            pb.Add(a => a.Text, "Test");
            pb.Add(a => a.Duration, 20);
        });
        Assert.Contains("animation-duration: 20s;", cut.Markup);
    }

    [Theory]
    [InlineData(MarqueeDirection.LeftToRight, "LeftToRight")]
    [InlineData(MarqueeDirection.RightToLeft, "RightToLeft")]
    [InlineData(MarqueeDirection.TopToBottom, "TopToBottom")]
    [InlineData(MarqueeDirection.BottomToTop, "BottomToTop")]
    public void Direction_Ok(MarqueeDirection direction, string expected)
    {
        var cut = Context.Render<Marquee>(pb =>
        {
            pb.Add(a => a.Text, "Test");
            pb.Add(a => a.Direction, direction);
        });
        Assert.Contains($"animation-name: {expected};", cut.Markup);
    }
}
