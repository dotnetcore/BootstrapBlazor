// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class ThemeProviderTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task SetTheme_Ok()
    {
        var themeProviderService = Context.Services.GetRequiredService<IThemeProvider>();
        await themeProviderService.SetThemeAsync("light");
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
