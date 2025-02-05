// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    }

    [Fact]
    public void Options_Single_Ok()
    {
        var options = new BootstrapBlazorOptions
        {
            SupportedCultures = ["zh-CN"]
        };
        Assert.Single(options.GetSupportedCultures());
    }

    [Fact]
    public void ContextMenuOptions_Ok()
    {
        var options = new BootstrapBlazorOptions
        {
            ContextMenuOptions = new ContextMenuOptions() { OnTouchDelay = 500 }
        };
        Assert.NotNull(options.ContextMenuOptions);
        Assert.Equal(500, options.ContextMenuOptions.OnTouchDelay);
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

    [Fact]
    public void Options_TableExportOptions()
    {
        var options = new BootstrapBlazorOptions();
        Assert.NotNull(options.TableSettings.TableExportOptions);

        var exportOptions = options.TableSettings.TableExportOptions;

        exportOptions.EnableLookup = false;
        exportOptions.EnableFormat = false;
        exportOptions.AutoMergeArray = false;
        exportOptions.ArrayDelimiter = ",";
        exportOptions.UseEnumDescription = false;

        Assert.False(exportOptions.EnableLookup);
        Assert.False(exportOptions.EnableFormat);
        Assert.False(exportOptions.AutoMergeArray);
        Assert.False(exportOptions.UseEnumDescription);

        Assert.Equal(",", exportOptions.ArrayDelimiter);
    }

    [Fact]
    public void CacheManagerOptions_Ok()
    {
        var options = new BootstrapBlazorOptions();
        Assert.NotNull(options.CacheManagerOptions);

        options.CacheManagerOptions.Enable = true;
        options.CacheManagerOptions.SlidingExpiration = TimeSpan.FromSeconds(1);
        options.CacheManagerOptions.AbsoluteExpiration = TimeSpan.FromSeconds(1);

        Assert.Equal(TimeSpan.FromSeconds(1), options.CacheManagerOptions.AbsoluteExpiration);
        Assert.Equal(TimeSpan.FromSeconds(1), options.CacheManagerOptions.SlidingExpiration);
        Assert.True(options.CacheManagerOptions.Enable);
    }
}
