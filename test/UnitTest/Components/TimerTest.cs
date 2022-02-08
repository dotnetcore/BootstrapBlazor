// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Timer = BootstrapBlazor.Components.Timer;

namespace UnitTest.Components;

public class TimerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Value_Ok()
    {
        var cut = Context.RenderComponent<Timer>(builder => builder.Add(a => a.Value, TimeSpan.FromSeconds(10)));

        Assert.Contains("circle-body", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.Zero);
        });
        Assert.DoesNotContain("circle-body", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Timer>(builder =>
        {
            builder.Add(a => a.Value, TimeSpan.FromSeconds(5));
            builder.Add(a => a.Width, 100);
        });

        Assert.Contains("width: 100px", cut.Markup);
    }

    [Fact]
    public void StrokeWidth_Ok()
    {
        var cut = Context.RenderComponent<Timer>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(5));
            pb.Add(a => a.StrokeWidth, 5);
        });
        Assert.Contains("stroke-width=\"5\"", cut.Markup);

        // 增加代码覆盖率
        //Width / 2 < StrokeWidth
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Width, 6);
            pb.Add(a => a.StrokeWidth, 6);
        });
        Assert.Equal(2, cut.Instance.StrokeWidth);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Timer>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(5));
            pb.Add(a => a.Color, Color.Success);
        });
        Assert.Contains("circle-success", cut.Markup);
    }

    [Fact]
    public void ShowProgress_Ok()
    {
        var cut = Context.RenderComponent<Timer>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(5));
            pb.Add(a => a.ShowProgress, false);
        });
        Assert.Contains("circle-title d-none", cut.Markup);
    }

    [Fact]
    public void IsVibrate_Ok()
    {
        var cut = Context.RenderComponent<Timer>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(5));
            pb.Add(a => a.IsVibrate, false);
        });
    }

    [Fact]
    public async Task OnStart_Ok()
    {
        var timeout = false;
        var cut = Context.RenderComponent<Timer>(pb =>
        {
            pb.Add(a => a.OnTimeout, () =>
            {
                timeout = true;
                return Task.CompletedTask;
            });
        });
        var downs = cut.FindAll(".time-spinner-arrow.fa-angle-down");
        downs[2].Click();
        cut.Find(".time-panel-btn.confirm").Click();

        await Task.Delay(2000);
        Assert.True(timeout);
    }

    [Fact]
    public async Task OnCancel_Ok()
    {
        var cancelled = false;
        var cut = Context.RenderComponent<Timer>(pb =>
        {
            pb.Add(a => a.OnCancel, () =>
            {
                cancelled = true;
                return Task.CompletedTask;
            });
        });
        var downs = cut.FindAll(".time-spinner-arrow.fa-angle-down");
        downs[0].Click();
        cut.Find(".time-panel-btn.confirm").Click();
        await Task.Delay(1000);
        var buttons = cut.FindAll(".timer-buttons button");
        // pause
        Assert.True(buttons[1].ClassList.Contains("btn-warning"));
        buttons[1].Click();
        await Task.Delay(500);

        // resume
        buttons = cut.FindAll(".timer-buttons button");
        Assert.True(buttons[1].ClassList.Contains("btn-success"));
        buttons[1].Click();

        // cancel
        buttons = cut.FindAll(".timer-buttons button");
        buttons[0].Click();
        Assert.True(cancelled);

        // 代码覆盖率 Cancel 后再 Star
        downs = cut.FindAll(".time-spinner-arrow.fa-angle-down");
        downs[0].Click();
        cut.Find(".time-panel-btn.confirm").Click();
        await Task.Delay(1000);
    }
}
