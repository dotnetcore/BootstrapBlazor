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
    public async Task OnValueChanged_Ok()
    {
        var changed = false;
        var cut = Context.RenderComponent<TimePicker>(pb =>
        {
            pb.Add(a => a.Value, new TimeSpan(10, 10, 10));
            pb.Add(a => a.OnValueChanged, ts =>
            {
                changed = true;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() => cut.Instance.SetValue(new TimeSpan(11, 11, 11)));
        Assert.True(changed);
        Assert.Equal(new TimeSpan(11, 11, 11), cut.Instance.Value);
    }
}
