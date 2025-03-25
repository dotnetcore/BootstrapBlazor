// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
