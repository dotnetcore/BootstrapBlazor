// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class FlipClockTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ShowHour_Ok()
    {
        var cut = Context.RenderComponent<FlipClock>();
        cut.Contains("bb-flip-clock-list hour");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowHour, false);
        });
        cut.DoesNotContain("bb-flip-clock-list hour");
    }

    [Fact]
    public void ShowMinute_Ok()
    {
        var cut = Context.RenderComponent<FlipClock>();
        cut.Contains("bb-flip-clock-list minute");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowMinute, false);
        });
        cut.DoesNotContain("bb-flip-clock-list minute");
    }

    [Fact]
    public void ShowSecond_Ok()
    {
        var cut = Context.RenderComponent<FlipClock>();
        cut.Contains("bb-flip-clock-list second");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowSecond, false);
        });
        cut.DoesNotContain("bb-flip-clock-list second");
    }

    [Fact]
    public async Task ViewMode_Ok()
    {
        var completed = false;
        var cut = Context.RenderComponent<FlipClock>(pb =>
        {
            pb.Add(a => a.ViewMode, FlipClockViewMode.CountDown);
            pb.Add(a => a.StartValue, TimeSpan.FromSeconds(2));
            pb.Add(a => a.OnCompletedAsync, () =>
            {
                completed = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.OnCompleted());
        Assert.True(completed);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.StartValue, null);
        });
    }

    [Theory]
    [InlineData("Height", "100px", "--bb-flip-clock-height: 100px;")]
    [InlineData("BackgroundColor", "100px", "--bb-flip-clock-bg: 100px;")]
    [InlineData("FontSize", "100px", "--bb-flip-clock-font-size: 100px;")]
    [InlineData("CardWidth", "100px", "--bb-flip-clock-item-width: 100px;")]
    [InlineData("CardHeight", "100px", "--bb-flip-clock-item-height: 100px;")]
    [InlineData("CardColor", "100px", "--bb-flip-clock-number-color: 100px;")]
    [InlineData("CardBackgroundColor", "100px", "--bb-flip-clock-number-bg: 100px;")]
    [InlineData("CardDividerHeight", "100px", "--bb-flip-clock-number-line-height: 100px;")]
    [InlineData("CardDividerColor", "100px", "--bb-flip-clock-number-line-bg: 100px;")]
    [InlineData("CardMargin", "100px", "--bb-flip-clock-item-margin: 100px;")]
    [InlineData("CardGroupMargin", "100px", "--bb-flip-clock-list-margin-right: 100px;")]
    public void FlipParameter_Ok(string parameterName, string value, string expected)
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent(builder =>
            {
                builder.OpenComponent<FlipClock>(0);
                builder.AddAttribute(1, parameterName, value);
                builder.CloseComponent();
            });
        });
        cut.Contains(expected);
    }
}
