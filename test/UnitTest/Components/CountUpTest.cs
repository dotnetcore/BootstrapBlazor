// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class CountUpTest : BootstrapBlazorTestBase
{
    [Fact]
    public void CountUp_Ok()
    {
        var cut = Context.Render<CountUp<int>>(pb =>
        {
            pb.Add(a => a.Value, 1234);
        });
        cut.Contains("<span id=");

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 23);
        });
    }

    [Fact]
    public void Class_Ok()
    {
        var cut = Context.Render<CountUp<int>>(pb =>
        {
            pb.Add(a => a.Value, 1234);
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>() { { "class", "test1" } });
        });
        cut.Contains("<span class=\"test1\" id=");

        cut.Render(pb =>
        {
            pb.Add(a => a.Value, 23);
        });
    }

    [Fact]
    public void CountUp_Error()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            Context.Render<CountUp<string>>();
        });
    }

    [Fact]
    public void OnCompleted_Ok()
    {
        var completed = false;
        var cut = Context.Render<CountUp<int>>(pb =>
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
            Numerals = ['0', '1']
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
