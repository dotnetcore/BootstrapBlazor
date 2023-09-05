// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CountUpTest : BootstrapBlazorTestBase
{
    [Fact]
    public void CountUp_Ok()
    {
        var cut = Context.RenderComponent<CountUp<int>>(pb =>
        {
            pb.Add(a => a.Value, 1234);
        });
        cut.Contains("<span id=");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, 23);
        });
    }

    [Fact]
    public void Class_Ok()
    {
        var cut = Context.RenderComponent<CountUp<int>>(pb =>
        {
            pb.Add(a => a.Value, 1234);
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>() { { "class", "test1" } });
        });
        cut.Contains("<span class=\"test1\" id=");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, 23);
        });
    }

    [Fact]
    public void CountUp_Error()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            Context.RenderComponent<CountUp<string>>();
        });
    }

    [Fact]
    public void OnCompleted_Ok()
    {
        var completed = false;
        var cut = Context.RenderComponent<CountUp<int>>(pb =>
        {
            pb.Add(a => a.Value, 1234);
            pb.Add(a => a.Option, new CountUpOption());
            pb.Add(a => a.OnCompleted, () =>
            {
                completed = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.OnCompleteCallback());
        Assert.True(completed);
    }

    [Fact]
    public void CountUpOption_Ok()
    {
        var option = new CountUpOption()
        {
            StartValue = 1000,
            Duration = 2,
            DecimalPlaces = 2,
            Decimal = ",",
            Separator = ".",
            Prefix = "$",
            Suffix = "元",
            UseEasing = true,
            UseGrouping = true,
            UseIndianSeparators = true,
            EnableScrollSpy = false,
            ScrollSpyDelay = 200,
            ScrollSpyOnce = false,
            SmartEasingAmount = 333,
            SmartEasingThreshold = 999,
            Numerals = new char[] { '0', '1' }
        };
        Assert.Equal(1000, option.StartValue);
        Assert.Equal(2, option.Duration);
        Assert.Equal(2, option.DecimalPlaces);
        Assert.Equal(",", option.Decimal);
        Assert.Equal(".", option.Separator);
        Assert.Equal("$", option.Prefix);
        Assert.Equal("元", option.Suffix);
        Assert.True(option.UseEasing);
        Assert.True(option.UseGrouping);
        Assert.True(option.UseIndianSeparators);
        Assert.False(option.EnableScrollSpy);
        Assert.Equal(200, option.ScrollSpyDelay);
        Assert.False(option.ScrollSpyOnce);
        Assert.Equal(333, option.SmartEasingAmount);
        Assert.Equal(999, option.SmartEasingThreshold);
        Assert.Equal(new char[] { '0', '1' }, option.Numerals);
    }
}
