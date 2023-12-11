// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <param name="options"></param>
internal class DefaultIconTheme(IOptions<IconThemeOptions> options) : IIconTheme
{
    private IOptions<IconThemeOptions> Options { get; set; } = options;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Dictionary<ComponentIcons, string> GetIcons()
    {
        if (!Options.Value.Icons.TryGetValue(Options.Value.ThemeKey, out var icons))
        {
            icons = [];
        }
        return icons;
    }
}
