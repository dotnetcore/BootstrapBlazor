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
    public void HeightCallback_Ok()
    {
        var cut = Context.RenderComponent<TimePicker>();
        var cell = cut.FindComponent<TimePickerCell>();
        cut.InvokeAsync(() => cell.Instance.OnHeightCallback(16));
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, TimeSpan.FromSeconds(1));
        });
    }

    [Fact]
    public async Task ValueChanged_Ok()
    {
        var val = new TimeSpan(10, 10, 10);
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<TimeSpan>(this, ts =>
            {
                val = ts;
                return Task.CompletedTask;
            }));
        });
        var button = cut.Find(".confirm");
        await cut.InvokeAsync(() => button.Click());
        Assert.Equal(new TimeSpan(10, 10, 10), cut.Instance.Value);
        Assert.Equal(new TimeSpan(10, 10, 10), val);
    }

    [Fact]
    public void HasSeconds_Ok()
    {
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.HasSeconds, false);
        });
        var cells = cut.FindComponents<TimePickerCell>();
        Assert.Equal(2, cells.Count);
    }

    [Fact]
    public async Task OnClickClose_Ok()
    {
        var close = false;
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.OnClose, () =>
            {
                close = true;
                return Task.CompletedTask;
            });
        });
        var btn = cut.Find(".cancel");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(close);
    }

    [Fact]
    public async Task OnClickConfirm_Ok()
    {
        var confirm = false;
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.OnConfirm, ts =>
            {
                confirm = true;
                return Task.CompletedTask;
            });
        });
        var btn = cut.Find(".confirm");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(confirm);
    }
}
