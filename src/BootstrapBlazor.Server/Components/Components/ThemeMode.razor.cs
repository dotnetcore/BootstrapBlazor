// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class ThemeMode
{
    private bool _isDark;

    private string _imageUrl => _isDark ? "./images/theme-dark.svg" : "./images/theme-light.svg";

    private async Task ToggleThemeMode()
    {
        _isDark = !_isDark;

        await InvokeVoidAsync("setTheme", _isDark ? "light" : "dark");
    }
}
