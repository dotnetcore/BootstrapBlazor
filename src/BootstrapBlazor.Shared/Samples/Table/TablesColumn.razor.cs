// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 列设置示例代码
/// </summary>
public partial class TablesColumn
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 5, 10, 20 };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private static bool ShowCheckbox(Foo foo) => foo.Complete;

    /// <summary>
    /// IntFormatter
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private static Task<string> IntFormatter(object? d)
    {
        var ret = "";
        if (d is TableColumnContext<Foo, object?> data && data.Value != null)
        {
            var val = (int)data.Value;
            ret = val.ToString("0.00");
        }
        return Task.FromResult(ret);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;

        // 先处理过滤再处理排序 提高性能
        var isFiltered = false;
        if (options.Filters.Any())
        {
            items = items.Where(options.Filters.GetFilterFunc<Foo>());
            isFiltered = true;
        }

        // 排序
        var isSorted = false;
        if (!string.IsNullOrEmpty(options.SortName))
        {
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
            IsFiltered = isFiltered,
            IsSearch = true
        });
    }

    private static Task<bool> OnSaveAsync(Foo foo, ItemChangedType changedType) => Task.FromResult(true);

    private static Task OnColumnCreating(List<ITableColumn> columns)
    {
        var item = columns.Find(i => i.GetFieldName() == nameof(Foo.Name));
        if (item != null)
        {
            item.Readonly = true;
        }
        return Task.CompletedTask;
    }
}
