// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;
using System.Globalization;

namespace BootstrapBlazor.Server.Services;

internal class MenuService(IStringLocalizer<NavMenu> localizer)
{
    private ConcurrentDictionary<string, List<MenuItem>> MenuCache { get; } = new();

    public List<MenuItem> GetMenus() => MenuCache.GetOrAdd(CultureInfo.CurrentCulture.Name, key => localizer.GenerateMenus());
}
