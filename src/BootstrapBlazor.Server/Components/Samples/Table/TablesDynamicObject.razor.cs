// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 动态示例代码
/// </summary>
public partial class TablesDynamicObject
{
    private IEnumerable<CustomDynamicColumnsObjectData> _customDynamicItems = [];

    private static List<string> StaticColumnList =>
    [
        "A",
        "B",
        "C",
        "Z"
    ];

    private List<string> _dynamicColumnList = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 构造动态列
        var now = DateTime.Now;
        _dynamicColumnList = Enumerable.Range(1, 5).Select(index => now.AddMinutes(-1 * index).ToString("HH:mm")).ToList();
        _customDynamicItems = GenerateDynamicColumnsObjectData();
    }

    private static IEnumerable<CustomDynamicColumnsObjectData> GenerateDynamicColumnsObjectData() => Enumerable.Range(1, 10)
        .Select(index => new CustomDynamicColumnsObjectData(index.ToString(), GenerateRowData(index)));

    private static Dictionary<string, object?> GenerateRowData(int index)
    {
        var ret = new Dictionary<string, object?>();
        for (int i = 0; i < StaticColumnList.Count; i++)
        {
            var columnName = StaticColumnList[i];
            object? value = null;
            if (columnName == "A")
            {
                value = $"Template - A{index}";
            }
            else if (columnName == "B")
            {
                value = index * 100;
            }
            else if (columnName == "C")
            {
                value = DateTime.Now.AddDays(index);
            }
            else if (columnName == "Z")
            {
                value = i % 2 == 0;
            }
            ret.Add(columnName, value);
        }
        return ret;
    }

    private readonly static Random random = new();

    private Task<QueryData<CustomDynamicData>> OnQueryAsync(QueryPageOptions options)
    {
        var items = Enumerable.Range(1, 10).Select(index => new CustomDynamicData(index.ToString(), GenerateDynamicRowData(index)));
        // sort logic ...
        // filter logic ...
        return Task.FromResult(new QueryData<CustomDynamicData>() { Items = items, TotalCount = 10, IsSorted = true, IsFiltered = true });
    }

    private Dictionary<string, object?> GenerateDynamicRowData(int index)
    {
        var ret = new Dictionary<string, object?>();
        for (int i = 0; i < _dynamicColumnList.Count; i++)
        {
            var columnName = _dynamicColumnList[i];
            object? value = random.Next(1000, 9999);
            ret.Add(columnName, value);
        }
        return ret;
    }
}
