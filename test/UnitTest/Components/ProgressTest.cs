// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class ProgressTest : TestBase
{
    [Fact]
    public void ShowValue_Ok()
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.IsShowValue, true);
            pb.Add(a => a.Value, 50);
        });

        Assert.Contains("50%", cut.Markup);
    }

    [Fact]
    public void IsStriped_Ok()
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.IsStriped, true);
            pb.Add(a => a.Value, 50);
        });

        Assert.Contains("progress-bar-striped", cut.Markup);
    }

    [Fact]
    public void IsAnimated_Ok()
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.IsAnimated, true);
            pb.Add(a => a.Value, 50);
        });

        Assert.Contains("progress-bar-animated", cut.Markup);
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.Value, 50);
            pb.Add(a => a.IsShowValue, true);
            pb.Add(a => a.Text, "Text-Value");
        });

        Assert.Contains("Text-Value", cut.Markup);
    }

    [Theory]
    [InlineData(10.035, 2, MidpointRounding.AwayFromZero, "10.04")]
    [InlineData(10.034, 2, MidpointRounding.AwayFromZero, "10.03")]
    [InlineData(10.036, 2, MidpointRounding.AwayFromZero, "10.04")]
    [InlineData(10.125, 2, MidpointRounding.ToEven, "10.12")]
    [InlineData(10.124, 2, MidpointRounding.ToEven, "10.12")]
    [InlineData(10.126, 2, MidpointRounding.ToEven, "10.13")]
    public void Round_Ok(double value, int round, MidpointRounding mode, string expected)
    {
        var cut = Context.RenderComponent<Progress>(pb =>
        {
            pb.Add(a => a.Value, value);
            pb.Add(a => a.Round, round);
            pb.Add(a => a.MidpointRounding, mode);
        });

        Assert.Contains(expected, cut.Markup);
    }
}
