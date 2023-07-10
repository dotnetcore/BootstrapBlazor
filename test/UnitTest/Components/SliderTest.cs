// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SliderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Value_OK()
    {
        var cut = Context.RenderComponent<Slider<double>>(builder => builder.Add(s => s.Value, 10));
    }

    [Fact]
    public async Task ValueChanged_OK()
    {
        var ret = false;
        var cut = Context.RenderComponent<Slider<double>>(builder =>
        {
            builder.Add(s => s.Value, 10);
            builder.Add(s => s.ValueChanged, v =>
            {
                ret = true;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.SetValue(20));
        Assert.True(ret);
        Assert.Equal(20, cut.Instance.Value);
    }

    [Fact]
    public void IsDisabled_OK()
    {
        var cut = Context.RenderComponent<Slider<double>>(builder =>
        {
            builder.Add(s => s.Value, 10);
            builder.Add(s => s.IsDisabled, true);
        });
        cut.Contains("slider-runway disabled");
    }

    [Fact]
    public async Task OnValueChanged_OK()
    {
        var expected = 0d;
        var cut = Context.RenderComponent<Slider<double>>(builder =>
        {
            builder.Add(s => s.Value, 10);
            builder.Add(s => s.Min, 0);
            builder.Add(s => s.Max, 100);
            builder.Add(s => s.OnValueChanged, new Func<double, Task>(v =>
            {
                expected = v;
                return Task.CompletedTask;
            }));
        });
        await cut.InvokeAsync(() => cut.Instance.SetValue(1));
        Assert.Equal(1, expected);
    }
}
