// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public partial class TablesDynamicObject
{
    private readonly List<DateTime> Times = new();

    private readonly string[] DynamicList = new[] { "A", "B", "C", "Z" };

    [NotNull]
    private List<BootstrapBlazorDynamicObjectData>? BootstrapDynamicItems { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        const int count = 10;

        var now = DateTime.Now;
        Times.AddRange(Enumerable.Range(1, 5).Select(e => now.AddMinutes(-1 * e)));

        BootstrapDynamicItems = Enumerable.Range(1, count)
            .Select(e => new BootstrapBlazorDynamicObjectData(
                e.ToString(),
                DynamicList.ToDictionary(d => d, d => d.ToString() + e)))
            .ToList();
    }

    private Task<QueryData<CustomDynamicData>> OnQueryAsync(QueryPageOptions options)
    {
        Random random = new();
        const int count = 10;
        var items = Enumerable.Range(1, count)
            .Select(e => new CustomDynamicData(
                e.ToString(),
                Times.ToDictionary(d => d.ToString(), d => ((e * 1000) + d.Minute).ToString())))
            .ToList();

        // sort logic ...
        // filter logic ...

        return Task.FromResult(new QueryData<CustomDynamicData>()
        {
            Items = items,
            TotalCount = count,
            IsSorted = true,
            IsFiltered = true
        });
    }
}
