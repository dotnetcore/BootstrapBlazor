// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultThemeProvider(IJSRuntime jsRuntime) : IThemeProvider
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<string, Task>? ThemeChangedAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="themeName"></param>
    public async ValueTask SetThemeAsync(string themeName)
    {
        var module = await jsRuntime.LoadUtility();
        await module.SetThemeAsync(themeName);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask<string?> GetThemeAsync()
    {
        var module = await jsRuntime.LoadUtility();
        return await module.GetThemeAsync();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void TriggerThemeChanged(string themeName)
    {
        ThemeChangedAsync?.Invoke(themeName);
    }
}
