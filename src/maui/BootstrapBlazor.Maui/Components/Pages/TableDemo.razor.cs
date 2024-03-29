// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Maui.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Maui.Components.Pages;

/// <summary>
/// 
/// </summary>
public partial class TableDemo : ComponentBase
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private readonly ConcurrentDictionary<Foo, IEnumerable<SelectedItem>> _cache = new();

    private IEnumerable<SelectedItem> GetHobbies(Foo item) => _cache.GetOrAdd(item, f => Foo.GenerateHobbies(Localizer));

    /// <summary>
    /// 
    /// </summary>
    private static IEnumerable<int> PageItemsSource => new int[] { 20, 40 };
}
