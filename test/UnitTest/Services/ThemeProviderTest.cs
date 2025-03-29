// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class ThemeProviderTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task SetTheme_Ok()
    {
        var themeName = "";
        var themeProviderService = Context.Services.GetRequiredService<IThemeProvider>();
        themeProviderService.TriggerThemeChanged("light");

        themeProviderService.ThemeChangedAsync = async theme =>
        {
            themeName = theme;
            await Task.CompletedTask;
        };
        await themeProviderService.SetThemeAsync("light");
        themeProviderService.TriggerThemeChanged("light");
        Assert.Equal("light", themeName);
    }

    [Fact]
    public async Task GetTheme_Ok()
    {
        Context.JSInterop.Setup<string>("getTheme").SetResult("light");
        var themeProviderService = Context.Services.GetRequiredService<IThemeProvider>();
        var theme = await themeProviderService.GetThemeAsync();
        Assert.Equal("light", theme);
    }
}
