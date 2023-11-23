// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 动态示例代码
/// </summary>
public partial class TablesDynamicObject
{
    [NotNull]
    private IEnumerable<CustomDynamicColumnsObjectData>? CustomDynamicItems { get; set; }
    private static List<string> StaticColumnList => new()
    {
        "A",
        "B",
        "C",
        "Z"
    };

    [NotNull]
    private List<string>? DynamicColumnList { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        // 构造动态列
        var now = DateTime.Now;
        DynamicColumnList = Enumerable.Range(1, 5).Select(index => now.AddMinutes(-1 * index).ToString("HH:mm")).ToList();
        CustomDynamicItems = Enumerable.Range(1, 10).Select(index => new CustomDynamicColumnsObjectData(index.ToString(), StaticColumnList.ToDictionary(d => d, d => (object?)$"{d}{index}")));
    }

    private readonly static Random random = new();

    private Task<QueryData<CustomDynamicData>> OnQueryAsync(QueryPageOptions options)
    {
        var items = Enumerable.Range(1, 10).Select(index => new CustomDynamicData(index.ToString(), DynamicColumnList.ToDictionary(d => d.ToString(), d => $"{random.Next(1000, 9999)}")));
        // sort logic ...
        // filter logic ...
        return Task.FromResult(new QueryData<CustomDynamicData>() { Items = items, TotalCount = 10, IsSorted = true, IsFiltered = true });
    }
}
