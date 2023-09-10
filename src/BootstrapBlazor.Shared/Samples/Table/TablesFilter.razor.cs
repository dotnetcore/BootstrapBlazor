// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 数据过滤示例代码
/// </summary>
public partial class TablesFilter
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    private string SortString { get; set; } = "DateTime desc, Address";

    private string ComponentSourceCodeUrl => $"{WebsiteOption.CurrentValue.BootstrapBlazorLink}/blob/main/src/BootstrapBlazor.Shared/Components/CustomerFilter.razor";

    [NotNull]
    private Table<Foo>? TableSetFilter { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 通过 options 获得用户组合的过滤条件
        var filters = options.ToFilter();

        // 使用内置扩展方法 ToFilter 获得过滤条件
        var items = Items.Where(filters.GetFilterFunc<Foo>());

        // 排序标记
        var isSorted = false;

        // 此段代码可不写，组件内部自行处理
        if (options.SortName == nameof(Foo.DateTime) && options.SortList != null)
        {
            items = items.Sort(options.SortList);
            isSorted = true;
        }
        else if (!string.IsNullOrEmpty(options.SortName))
        {
            // 外部未进行排序，内部自动进行排序处理
            items = items.Sort(options.SortName, options.SortOrder);
            isSorted = true;
        }

        // 设置记录总数
        var total = items.Count();

        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

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
