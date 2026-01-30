// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 自动刷新示例代码
/// </summary>
public partial class TablesAutoRefresh
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    private List<Foo> Items { get; set; } = [];

    private bool IsAutoRefresh { get; set; }

    private static readonly Random random = new();

    private void ToggleAuto() => IsAutoRefresh = !IsAutoRefresh;

    private Task<QueryData<Foo>> OnAutoQueryAsync(QueryPageOptions options) => GenerateFoos(options, Items);

    private Task<QueryData<Foo>> OnManualQueryAsync(QueryPageOptions options) => GenerateFoos(options, Items);

    private int _id = 0;

    private Task<QueryData<Foo>> GenerateFoos(QueryPageOptions options, List<Foo> foos)
    {
        // 设置记录总数
        var foo = Foo.Generate(FooLocalizer);
        foo.Id = _id++;
        foo.Name = FooLocalizer["Foo.Name", foo.Id.ToString("D4")];
        foo.Address = FooLocalizer["Foo.Address", $"{random.Next(1000, 2000)}"];
        foos.Insert(0, foo);
        if (foos.Count > 10)
        {
            foos.RemoveRange(10, 1);
        }

        // 内存分页
        var items = foos.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = foos.Count });
    }
}
