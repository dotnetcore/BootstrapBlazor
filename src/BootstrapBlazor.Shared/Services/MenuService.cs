// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;
using System.Globalization;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// 
/// </summary>
public class MenuService
{
    private ConcurrentDictionary<string, List<MenuItem>> MenuCache { get; } = new();

    private readonly IStringLocalizer<NavMenu> _localization;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="localizer"></param>
    public MenuService(IStringLocalizer<NavMenu> localizer) => _localization = localizer;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<MenuItem> GetMenus() => MenuCache.GetOrAdd(CultureInfo.CurrentCulture.Name, key => _localization.GenerateMenus());
}
