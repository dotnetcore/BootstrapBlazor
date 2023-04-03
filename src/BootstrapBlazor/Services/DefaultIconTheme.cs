// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class DefaultIconTheme : IIconTheme
{
    private IOptions<IconThemeOptions> Options { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public DefaultIconTheme(IOptions<IconThemeOptions> options)
    {
        Options = options;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Dictionary<ComponentIcons, string> GetIcons()
    {
        Options.Value.Icons.TryGetValue(Options.Value.ThemeKey, out var icons);
        return icons ?? new Dictionary<ComponentIcons, string>();
    }
}
