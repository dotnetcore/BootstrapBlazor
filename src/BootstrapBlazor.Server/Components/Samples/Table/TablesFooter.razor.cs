﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 统计合并文档
/// </summary>
public partial class TablesFooter
{
    private static IEnumerable<int> PageItemsSource => new int[] { 10, 20, 40 };

    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
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
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo);
        Left ??= "Left";
        Center ??= "Center";
        Right ??= "Right";
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
