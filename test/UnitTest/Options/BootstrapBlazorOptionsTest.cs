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
            ShowErrorLoggerToast = true,
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

        Assert.Null(options.GetStep<sbyte?>());
        Assert.Null(options.GetStep<byte?>());
        Assert.Null(options.GetStep<short?>());
        Assert.Null(options.GetStep<ushort?>());
        Assert.Null(options.GetStep<int?>());
        Assert.Null(options.GetStep<uint?>());
        Assert.Null(options.GetStep<long?>());
        Assert.Null(options.GetStep<ulong?>());
        Assert.Null(options.GetStep<float?>());
        Assert.Null(options.GetStep<double?>());
        Assert.Null(options.GetStep<decimal?>());

        options.StepSettings.SByte = "1";
        options.StepSettings.Byte = "2";
        options.StepSettings.Short = "1";
        options.StepSettings.UShort = "2";
        options.StepSettings.Int = "2";
        options.StepSettings.UInt = "3";
        options.StepSettings.Long = "3";
        options.StepSettings.ULong = "4";
        options.StepSettings.Float = "0.1";
        options.StepSettings.Double = "0.01";
        options.StepSettings.Decimal = "0.001";

        Assert.Equal("1", options.GetStep<sbyte?>());
        Assert.Equal("2", options.GetStep<byte?>());
        Assert.Equal("1", options.GetStep<short?>());
        Assert.Equal("2", options.GetStep<ushort?>());
        Assert.Equal("2", options.GetStep<int?>());
        Assert.Equal("3", options.GetStep<uint?>());
        Assert.Equal("3", options.GetStep(typeof(long?)));
        Assert.Equal("4", options.GetStep<ulong?>());

        Assert.Equal("0.1", options.GetStep<float?>());
        Assert.Equal("0.01", options.GetStep<double?>());
        Assert.Equal("0.001", options.GetStep<decimal?>());

        options.StepSettings.Float = "any";
        options.StepSettings.Double = "any";
        options.StepSettings.Decimal = "any";

        Assert.Equal("any", options.GetStep<float?>());
        Assert.Equal("any", options.GetStep<double?>());
        Assert.Equal("any", options.GetStep<decimal?>());
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
        exportOptions.EnableAutoFilter = false;
        exportOptions.EnableAutoWidth = false;

        Assert.False(exportOptions.EnableLookup);
        Assert.False(exportOptions.EnableFormat);
        Assert.False(exportOptions.AutoMergeArray);
        Assert.False(exportOptions.UseEnumDescription);
        Assert.False(exportOptions.EnableAutoFilter);
        Assert.False(exportOptions.EnableAutoWidth);

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

    [Fact]
    public void ModalSettings_Ok()
    {
        var options = new BootstrapBlazorOptions();
        Assert.NotNull(options.ModalSettings);

        options.ModalSettings.IsFade = true;

        Assert.True(options.ModalSettings.IsFade);
    }

    [Fact]
    public void GetEditDialogShowConfirmSwal_Ok()
    {
        var options = new BootstrapBlazorOptions();
        options.EditDialogSettings.ShowCloseConfirm = null;
        Assert.False(options.GetEditDialogShowConfirmSwal(null, false));
        Assert.False(options.GetEditDialogShowConfirmSwal(null, true));
        Assert.False(options.GetEditDialogShowConfirmSwal(true, false));
        Assert.True(options.GetEditDialogShowConfirmSwal(true, true));
        Assert.False(options.GetEditDialogShowConfirmSwal(false, false));
        Assert.False(options.GetEditDialogShowConfirmSwal(false, true));

        options.EditDialogSettings.ShowCloseConfirm = false;
        Assert.False(options.GetEditDialogShowConfirmSwal(null, false));
        Assert.False(options.GetEditDialogShowConfirmSwal(null, true));
        Assert.False(options.GetEditDialogShowConfirmSwal(true, false));
        Assert.True(options.GetEditDialogShowConfirmSwal(true, true));
        Assert.False(options.GetEditDialogShowConfirmSwal(false, false));
        Assert.False(options.GetEditDialogShowConfirmSwal(false, true));

        options.EditDialogSettings.ShowCloseConfirm = true;
        Assert.False(options.GetEditDialogShowConfirmSwal(null, false));
        Assert.True(options.GetEditDialogShowConfirmSwal(null, true));
        Assert.False(options.GetEditDialogShowConfirmSwal(true, false));
        Assert.True(options.GetEditDialogShowConfirmSwal(true, true));
        Assert.False(options.GetEditDialogShowConfirmSwal(false, false));
        Assert.False(options.GetEditDialogShowConfirmSwal(false, true));
    }
}
