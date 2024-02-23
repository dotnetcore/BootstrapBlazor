// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TimePickerBodyTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ButtonClick_Ok()
    {
        bool closed = false;
        bool confirmed = false;
        TimeSpan val = TimeSpan.Zero;
        var cut = Context.RenderComponent<TimePickerBody>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<TimeSpan>(this, ts =>
            {
                val = ts;
                return Task.CompletedTask;
            }));
            pb.Add(a => a.OnClose, () =>
            {
                closed = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnConfirm, ts =>
            {
                confirmed = true;
                return Task.CompletedTask;
            });
        });
        var down = cut.Find(".time-down");
        await cut.InvokeAsync(() => down.Click());
        var button = cut.Find(".cancel");
        await cut.InvokeAsync(() => button.Click());
        Assert.Equal(new TimeSpan(10, 10, 10), cut.Instance.Value);
        Assert.True(closed);

        await cut.InvokeAsync(() => down.Click());
        button = cut.Find(".confirm");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(confirmed);
        Assert.Equal(new TimeSpan(11, 10, 10), cut.Instance.Value);
        Assert.Equal(new TimeSpan(11, 10, 10), val);
    }

    [Fact]
    public void HasSeconds_Ok()
    {
        var cut = Context.RenderComponent<TimePickerBody>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.HasSeconds, true);
        });
        cut.Contains("has-seconds");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.HasSeconds, false);
        });
        cut.Contains("havenot-seconds");
    }
}
