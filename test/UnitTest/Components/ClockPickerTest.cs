// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ClockPickerTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task IsAutoSwitch_Ok()
    {
        var cut = Context.Render<ClockPicker>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(12.5));
            pb.Add(a => a.IsAutoSwitch, true);
        });

        // 当前表盘时小时
        cut.Contains("data-bb-mode=\"Hour\"");

        // 模拟 JSInvoke 调用 SetTime 方法
        await cut.InvokeAsync(() =>
        {
            cut.Instance.SetTime(11, 0, 0);
        });
        cut.Contains("data-bb-mode=\"Minute\"");

        // 模拟 JSInvoke 调用 SetTime 方法
        await cut.InvokeAsync(() =>
        {
            cut.Instance.SetTime(11, 0, 0);
        });
        cut.Contains("data-bb-mode=\"Second\"");
    }

    [Fact]
    public async Task ShowMinute_Ok()
    {
        var cut = Context.Render<ClockPicker>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(12.5));
            pb.Add(a => a.IsAutoSwitch, true);
            pb.Add(a => a.ShowMinute, false);
        });

        // 当前表盘时小时
        cut.Contains("data-bb-mode=\"Hour\"");

        // 模拟 JSInvoke 调用 SetTime 方法
        await cut.InvokeAsync(() =>
        {
            cut.Instance.SetTime(11, 0, 0);
        });
        cut.Contains("data-bb-mode=\"Hour\"");
    }

    [Fact]
    public async Task ShowSecond_Ok()
    {
        var cut = Context.Render<ClockPicker>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(12.5));
            pb.Add(a => a.IsAutoSwitch, true);
            pb.Add(a => a.ShowSecond, false);
        });

        // 当前表盘时小时
        cut.Contains("data-bb-mode=\"Hour\"");

        // 模拟 JSInvoke 调用 SetTime 方法
        await cut.InvokeAsync(() =>
        {
            cut.Instance.SetTime(11, 0, 0);
        });
        cut.Contains("data-bb-mode=\"Minute\"");

        // 模拟 JSInvoke 调用 SetTime 方法
        await cut.InvokeAsync(() =>
        {
            cut.Instance.SetTime(11, 0, 0);
        });
        cut.Contains("data-bb-mode=\"Minute\"");
    }

    [Fact]
    public async Task SwitchView_Ok()
    {
        var cut = Context.Render<ClockPicker>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(12.5));
        });
        var span = cut.Find(".hour");
        await cut.InvokeAsync(() =>
        {
            span.Click();
        });
        cut.Contains("data-bb-mode=\"Hour\"");

        span = cut.Find(".minute");
        await cut.InvokeAsync(() =>
        {
            span.Click();
        });
        cut.Contains("data-bb-mode=\"Minute\"");

        span = cut.Find(".second");
        await cut.InvokeAsync(() =>
        {
            span.Click();
        });
        cut.Contains("data-bb-mode=\"Second\"");
    }

    [Fact]
    public async Task SetTime_Ok()
    {
        var cut = Context.Render<ClockPicker>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(11));
        });
        await cut.InvokeAsync(() =>
        {
            cut.Instance.SetTime(12, 0, 0);
        });
        Assert.Equal(TimeSpan.FromHours(0), cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(11));
        });
        var button = cut.Find(".btn-am");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });
        Assert.Equal(TimeSpan.FromHours(11), cut.Instance.Value);

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(20));
        });
        button = cut.Find(".btn-pm");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });
        Assert.Equal(TimeSpan.FromHours(20), cut.Instance.Value);
    }

    [Fact]
    public async Task SetTimePeriod_Ok()
    {
        var cut = Context.Render<ClockPicker>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(12.5));
        });
        cut.Contains("btn-pm active");

        var button = cut.Find(".btn-am");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });
        cut.Contains("btn-am active");

        button = cut.Find(".btn-pm");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });
        cut.Contains("btn-pm active");
    }

    [Fact]
    public void ShowClockScale_Ok()
    {
        var cut = Context.Render<ClockPicker>(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromHours(12.5));
            pb.Add(a => a.ShowClockScale, true);
        });
        cut.Contains("bb-clock-panel bb-clock-panel-scale");
    }

    [Fact]
    public async Task DatePicker_Ok()
    {
        var cut = Context.Render<DateTimePicker<DateTime>>(pb =>
        {
            pb.Add(a => a.Value, DateTime.Now);
            pb.Add(a => a.ViewMode, DatePickerViewMode.DateTime);
        });
        var span = cut.Find(".bb-time-text");
        await cut.InvokeAsync(() =>
        {
            span.Click();
        });
        cut.Contains("data-bb-mode=\"Hour\"");
    }
}
