// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultIconTheme(IOptions<IconThemeOptions> options) : IIconTheme
{
    private readonly IOptions<IconThemeOptions> _options = options;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Dictionary<ComponentIcons, string> GetIcons()
    {
        if (!_options.Value.Icons.TryGetValue(_options.Value.ThemeKey, out var icons))
        {
            icons = [];
        }
        return icons;
    }
}
