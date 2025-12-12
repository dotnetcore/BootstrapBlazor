// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 数据过滤示例代码
/// </summary>
public partial class TablesFilter
{
    /// <summary>
    /// Foo 类为 Demo 测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    private List<Foo> SelectedItems { get; set; } = [];

    private static IEnumerable<int> PageItemsSource => [4, 10, 20];

    private string SortString { get; set; } = "DateTime desc, Address";

    private string ComponentSourceCodeUrl => $"{WebsiteOption.Value.GiteeRepositoryUrl}/blob/main/src/BootstrapBlazor.Server/Components/Components/CustomerFilter.razor";

    [NotNull]
    private Table<Foo>? TableSetFilter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private async Task<List<SelectedItem>> OnGetAddressItemsAsync()
    {
        // 模拟数据库延时
        await Task.Delay(500);
        return [.. Items.Select(i => new SelectedItem(i.Address!, i.Address!)).DistinctBy(i => i.Value)];
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {

        //增加filter，序列化测试通过
        var json = JsonSerializer.Serialize(options);
        options = JsonSerializer.Deserialize<QueryPageOptions>(json);


        // 通过 options 获得用户组合的过滤条件
        var filters = options.ToFilter();

        // 使用内置扩展方法 ToFilter 获得过滤条件
        var items = Items.Where(filters.GetFilterFunc<Foo>());

        // 排序标记
        var isSorted = false;

        //处理高级排序
        if (options.AdvancedSortList.Count != 0)
        {
            items = items.Sort(options.AdvancedSortList);
            isSorted = true;
        }

        if (!string.IsNullOrEmpty(options.SortName))
        {
            // 外部未进行排序，内部自动进行排序处理
            items = items.Sort(options.SortName, options.SortOrder);
            isSorted = true;
        }

        // 设置记录总数
        var enumerable = items.ToList();
        var total = enumerable.Count;

        // 内存分页
        items = enumerable.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = isSorted,
            IsFiltered = filters.HasFilters()
        });
    }

    private static string OnSort(string sortName, SortOrder sortOrder)
    {
        string sortString = "";
        if (sortName == nameof(Foo.DateTime))
        {
            if (sortOrder == SortOrder.Asc)
            {
                sortString = "DateTime, Count";
            }
            else if (sortOrder == SortOrder.Desc)
            {
                sortString = "DateTime desc, Count desc";
            }
            else
            {
                sortString = "DateTime desc, Count";
            }
        }
        return sortString;
    }

    private async Task SetFilterInCode()
    {
        //Find Column
        var column = TableSetFilter.Columns.First(x => x.GetFieldName() == nameof(Foo.Name));

        //Build Filter
        var filter = new FilterKeyValueAction()
        {
            FieldValue = "01",
            FilterAction = FilterAction.Contains
        };

        //Set Filter
        var filterAction = column.Filter?.FilterAction;
        if (filterAction != null)
        {
            await filterAction.SetFilterConditionsAsync(filter);
        }
    }

    private Task ResetAllFilters() => TableSetFilter.ResetFilters();
}
