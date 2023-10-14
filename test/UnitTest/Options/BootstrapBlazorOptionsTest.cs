// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Extensions;

namespace UnitTest.Options;

public class BootstrapBlazorOptionsTest
{
    [Fact]
    public void Options_Ok()
    {
        var options = new BootstrapBlazorOptions()
        {
            EnableErrorLogger = true,
            EnableFallbackCulture = true,
            JSModuleVersion = "1.0",
            TableSettings = new()
        };
        Assert.NotNull(options.GetSupportedCultures());
        Assert.Equal(2, options.Themes.Count);
    }

    [Fact]
    public void Options_Single_Ok()
    {
        var options = new BootstrapBlazorOptions
        {
            SupportedCultures = new List<string> { "zh-CN" }
        };
        Assert.Single(options.GetSupportedCultures());
    }

    [Fact]
    public void Options_IgnoreLocalizerMissing_Null()
    {
        var options = new BootstrapBlazorOptions();
        Assert.False(options.IgnoreLocalizerMissing.HasValue);

        options.IgnoreLocalizerMissing = true;
        Assert.True(options.IgnoreLocalizerMissing.Value);
    }

    [Fact]
    public void Options_StepSettings()
    {
        var options = new BootstrapBlazorOptions();
        Assert.NotNull(options.StepSettings);

        options.StepSettings = new();

        Assert.Null(options.GetStep<short?>());
        Assert.Null(options.GetStep<int?>());
        Assert.Null(options.GetStep<long?>());
        Assert.Null(options.GetStep<float?>());
        Assert.Null(options.GetStep<double?>());
        Assert.Null(options.GetStep<decimal?>());

        options.StepSettings.Short = 1;
        options.StepSettings.Int = 2;
        options.StepSettings.Long = 3;
        options.StepSettings.Float = 0.1f;
        options.StepSettings.Double = 0.01d;
        options.StepSettings.Decimal = 0.001M;

        Assert.Equal("1", options.GetStep<short?>());
        Assert.Equal("2", options.GetStep<int?>());
        Assert.Equal("3", options.GetStep(typeof(long?)));

        Assert.Equal("0.1", options.GetStep<float?>());
        Assert.Equal("0.01", options.GetStep<double?>());
        Assert.Equal("0.001", options.GetStep<decimal?>());
    }
}
