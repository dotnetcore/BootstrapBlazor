// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// ThemeMode 页面
/// </summary>
public partial class ThemeMode
{
    [Inject, NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? GetLightIconClassString => CssBuilder.Default("icon-light")
        .AddClass(_lightModeIcon)
        .Build();

    private string? GetDarkIconClassString => CssBuilder.Default("icon-dark")
        .AddClass(_darkModeIcon)
        .Build();

    private string? _darkModeIcon;

    private string? _lightModeIcon;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _darkModeIcon ??= IconTheme.GetIconByKey(ComponentIcons.ThemeProviderDarkModeIcon);
        _lightModeIcon ??= IconTheme.GetIconByKey(ComponentIcons.ThemeProviderLightModeIcon);
    }
}
