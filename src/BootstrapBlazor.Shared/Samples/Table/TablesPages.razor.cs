// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 分页示例代码
/// </summary>
public partial class TablesPages
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }
    private static IEnumerable<int> PageItemsSource => new int[]
    {
        4,
        10,
        20,
        40,
        80,
        100
    };

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;
        var total = items.Count();
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = true, IsFiltered = true, IsSearch = true });
    }
}
