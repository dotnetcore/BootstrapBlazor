// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Globalization;

namespace BootstrapBlazor.Server.Services;

internal class MenuService(IStringLocalizer<NavMenu> localizer)
{
    private ConcurrentDictionary<string, List<MenuItem>> MenuCache { get; } = new();

    public List<MenuItem> GetMenus() => MenuCache.GetOrAdd(CultureInfo.CurrentCulture.Name, key => localizer.GenerateMenus());
}
