// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public partial class TablesDynamicObject
{
    private readonly string[] DynamicList = new[] { "A", "B", "C", "Z" };

    [NotNull]
    private List<CustomDynamicData>? DynamicItems { get; set; }

    [NotNull]
    private List<BootstrapBlazorDynamicObjectData>? BootstrapDynamicItems { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        const int count = 10;
        DynamicItems = Enumerable.Range(1, count)
            .Select(e => new CustomDynamicData(
                e.ToString(),
                DynamicList.ToDictionary(d => d, d => d.ToString() + e)))
            .ToList();

        BootstrapDynamicItems = Enumerable.Range(1, count)
            .Select(e => new BootstrapBlazorDynamicObjectData(
                e.ToString(),
                DynamicList.ToDictionary(d => d, d => d.ToString() + e)))
            .ToList();
    }
}
