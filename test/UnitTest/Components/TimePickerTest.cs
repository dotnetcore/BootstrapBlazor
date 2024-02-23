// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TimePickerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void TimePicker_Ok()
    {
        var cut = Context.RenderComponent<TimePicker>();
        cut.MarkupMatches("""
            <div class="bb-time-picker" diff:ignore>
                <div class="bb-time-panel">
                    <div class="bb-time-header"></div>
                    <div class="bb-time-body"></div>
                    <div class="bb-time-footer"></div>
                </div>
            </div>
        """);
    }

    [Fact]
    public async Task ValueChanged_Ok()
    {
        var changed = false;
        var val = new TimeSpan(10, 10, 10);
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.OnValueChanged, ts =>
            {
                changed = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<TimeSpan>(this, ts =>
            {
                val = ts;
                return Task.CompletedTask;
            }));
        });
        var body = cut.FindComponent<TimePickerBody>();
        body.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(11, 11, 11));
        });
        var button = cut.Find(".confirm");
        await cut.InvokeAsync(() => button.Click());
        Assert.Equal(new TimeSpan(11, 11, 11), cut.Instance.Value);
        Assert.Equal(new TimeSpan(11, 11, 11), val);
        Assert.True(changed);
    }
}
