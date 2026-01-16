// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace BootstrapBlazor.Components;

class DefaultIconTheme(IOptions<IconThemeOptions> options) : IIconTheme
{
    private readonly IOptions<IconThemeOptions> _options = options;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
#if NET8_0_OR_GREATER
    public FrozenDictionary<ComponentIcons, string> GetIcons()
    {
        if (!_options.Value.Icons.TryGetValue(_options.Value.ThemeKey, out var icons))
        {
            icons = FrozenDictionary<ComponentIcons, string>.Empty;
        }
        return icons;
    }
#else
    public Dictionary<ComponentIcons, string> GetIcons()
    {
        if (!_options.Value.Icons.TryGetValue(_options.Value.ThemeKey, out var icons))
        {
            icons = [];
        }
        return icons;
    }
#endif
}
