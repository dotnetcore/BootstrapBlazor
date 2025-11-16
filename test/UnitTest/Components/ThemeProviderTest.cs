// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ThemeProviderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ThemeProvider_Ok()
    {
        var cut = Context.Render<ThemeProvider>(pb =>
        {
            pb.Add(a => a.ShowShadow, false);
            pb.Add(a => a.Alignment, Alignment.Center);
        });
        cut.Contains("dropdown bb-theme-mode");
        cut.DoesNotContain("dropdown-menu-center shadow");
    }

    [Fact]
    public async Task OnThemeChanged_Ok()
    {
        var v = ThemeValue.Auto;
        var cut = Context.Render<ThemeProvider>(pb =>
        {
            pb.Add(a => a.OnThemeChangedAsync, val =>
            {
                v = val;
                return Task.CompletedTask;
            });
        });
        await cut.Instance.OnThemeChanged(ThemeValue.Dark);
        Assert.Equal(ThemeValue.Dark, v);
    }

    [Fact]
    public async Task ThemeValueChanged_Ok()
    {
        var v = ThemeValue.Auto;
        var cut = Context.Render<ThemeProvider>(pb =>
        {
            pb.Add(a => a.ThemeValue, ThemeValue.Light);
            pb.Add(a => a.ThemeValueChanged, EventCallback.Factory.Create<ThemeValue>(this, val => v = val));
        });
        await cut.Instance.OnThemeChanged(ThemeValue.Dark);
        Assert.Equal(ThemeValue.Dark, v);
    }
}
