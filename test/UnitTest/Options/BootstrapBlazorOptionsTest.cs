// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Options;

public class BootstrapBlazorOptionsTest
{
    [Fact]
    public void Options_Ok()
    {
        var options = new BootstrapBlazorOptions();
        Assert.NotNull(options.GetSupportedCultures());
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
}
