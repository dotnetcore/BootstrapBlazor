// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultThemeProvider(IJSRuntime jsRuntime) : IThemeProvider
{
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
}
