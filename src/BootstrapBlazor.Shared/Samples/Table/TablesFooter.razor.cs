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
public sealed partial class TablesFooter
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TablesFooter>? LocalizerFooter { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 2, 4, 10, 20 };

    [NotNull]
    private IEnumerable<Foo>? Items { get; set; }

    [NotNull]
    private string? Left { get; set; }

    [NotNull]
    private string? Center { get; set; }

    [NotNull]
    private string? Right { get; set; }

    private Alignment Align { get; set; }

    private AggregateType Aggregate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);
        Left ??= LocalizerFooter[nameof(Left)];
        Center ??= LocalizerFooter[nameof(Center)];
        Right ??= LocalizerFooter[nameof(Right)];
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 设置记录总数
        var total = Items.Count();

        // 内存分页
        var items = Items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        });
    }

    private static double GetAverage(IEnumerable<Foo> items) => items.Any() ? items.Average(i => i.Count) : 0;

    private static int GetSum(IEnumerable<Foo> items) => items.Any() ? items.Sum(i => i.Count) : 0;
}
