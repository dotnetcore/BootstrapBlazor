// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 自动刷新示例代码
/// </summary>
public partial class TablesAutoRefresh
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    private List<Foo> Items { get; set; } = new List<Foo>();
    private bool IsAutoRefresh { get; set; }

    private static readonly Random random = new();
    private void ToggleAuto() => IsAutoRefresh = !IsAutoRefresh;
    private Task<QueryData<Foo>> OnAutoQueryAsync(QueryPageOptions options) => GenerateFoos(options, Items);
    private Task<QueryData<Foo>> OnManualQueryAsync(QueryPageOptions options) => GenerateFoos(options, Items);
    private Task<QueryData<Foo>> GenerateFoos(QueryPageOptions options, List<Foo> foos)
    {
        // 设置记录总数
        var total = foos.Count;
        var foo = Foo.Generate(LocalizerFoo);
        foo.Id = total++;
        foo.Name = LocalizerFoo["Foo.Name", total.ToString("D4")];
        foo.Address = LocalizerFoo["Foo.Address", $"{random.Next(1000, 2000)}"];
        foos.Insert(0, foo);
        if (foos.Count > 10)
        {
            foos.RemoveRange(10, 1);
            total = 10;
        }

        // 内存分页
        var items = foos.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total });
    }
}
