// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 分页示例代码
/// </summary>
public partial class TablesPages
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
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
        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;
        var total = items.Count();
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = true, IsFiltered = true, IsSearch = true });
    }
}
