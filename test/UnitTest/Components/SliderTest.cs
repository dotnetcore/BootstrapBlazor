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
    public void ShowLabel_OK()
    {
        var cut = Context.RenderComponent<Slider<double>>(builder =>
        {
            builder.Add(s => s.ShowLabel, true);
            builder.Add(s => s.DisplayText, "label-slider-text");
            builder.Add(s => s.Value, 10);
        });
        Assert.Contains("label-slider-text", cut.Markup);
    }

    [Fact]
    public void Group_OK()
    {
        var cut = Context.RenderComponent<BootstrapInputGroup>(pb =>
        {
            pb.AddChildContent<BootstrapInputGroupLabel>(pb =>
            {
                pb.Add(a => a.DisplayText, "GroupLabel");
            });
            pb.AddChildContent<Slider<double>>(pb =>
            {
                pb.Add(s => s.ShowLabel, true);
                pb.Add(s => s.DisplayText, "label-slider-text");
                pb.Add(s => s.Value, 10);
                pb.Add(s => s.UseInputEvent, true);
            });
        });
        Assert.Contains("GroupLabel", cut.Markup);
        Assert.DoesNotContain("label-slider-text", cut.Markup);
    }

    [Fact]
    public void Step_Ok()
    {
        var cut = Context.RenderComponent<Slider<double>>(pb =>
        {
            pb.Add(a => a.Value, 15);
            pb.Add(a => a.Min, 10);
            pb.Add(a => a.Max, 20);
            pb.Add(a => a.Step, 5);
        });
        cut.Contains("min=\"10\"");
        cut.Contains("max=\"20\"");
        cut.Contains("step=\"5\"");
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
        cut.Contains("disabled");
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
