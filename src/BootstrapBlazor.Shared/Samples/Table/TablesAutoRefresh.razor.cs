// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public partial class TablesAutoRefresh
{
    private static readonly Random random = new();

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private List<Foo> AutoItems { get; set; } = new List<Foo>();

    private List<Foo> ManualItems { get; set; } = new List<Foo>();

    private bool IsAutoRefresh { get; set; }

    private void ToggleAuto() => IsAutoRefresh = !IsAutoRefresh;

    private Task<QueryData<Foo>> OnAutoQueryAsync(QueryPageOptions options) => GenerateFoos(options, AutoItems);

    private Task<QueryData<Foo>> OnManualQueryAsync(QueryPageOptions options) => GenerateFoos(options, ManualItems);

    private Task<QueryData<Foo>> GenerateFoos(QueryPageOptions options, List<Foo> foos)
    {
        // 设置记录总数
        var total = foos.Count;
        var foo = Foo.Generate(Localizer);
        foo.Id = total++;
        foo.Name = Localizer["Foo.Name", total.ToString("D4")];
        foo.Address = Localizer["Foo.Address", $"{random.Next(1000, 2000)}"];

        foos.Insert(0, foo);

        if (foos.Count > 10)
        {
            foos.RemoveRange(10, 1);
            total = 10;
        }

        // 内存分页
        var items = foos.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total
        });
    }
}
