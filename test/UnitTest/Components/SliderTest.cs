// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class SliderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Value_OK()
    {
        var cut = Context.Render<Slider<double>>(builder => builder.Add(s => s.Value, 10));
    }

    [Fact]
    public void ShowLabel_OK()
    {
        var cut = Context.Render<Slider<double>>(builder =>
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
        var cut = Context.Render<BootstrapInputGroup>(pb =>
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
        Assert.Contains("label-slider-text", cut.Markup);
    }

    [Fact]
    public void Step_Ok()
    {
        var cut = Context.Render<Slider<double>>(pb =>
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
        var cut = Context.Render<Slider<double>>(builder =>
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
        var cut = Context.Render<Slider<double>>(builder =>
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
        var cut = Context.Render<Slider<double>>(builder =>
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

    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        var blur = false;
        var cut = Context.Render<Slider<int>>(builder =>
        {
            builder.Add(a => a.OnBlurAsync, v =>
            {
                blur = true;
                return Task.CompletedTask;
            });
        });
        var input = cut.Find("input");
        await cut.InvokeAsync(() => { input.Blur(); });
        Assert.True(blur);
    }

    [Fact]
    public void Range_OK()
    {
        var model = new SliderModel();
        var cut = Context.Render<Slider<int>>(builder =>
        {
            builder.Add(s => s.Value, 10);
            builder.Add(s => s.ValueExpression, Utility.GenerateValueExpression(model, "Value", typeof(int)));
        });
        cut.Contains("min=\"10\" max=\"200\"");
    }

    public class SliderModel
    {
        [Range(10, 200)]
        public int Value { get; set; }
    }
}
