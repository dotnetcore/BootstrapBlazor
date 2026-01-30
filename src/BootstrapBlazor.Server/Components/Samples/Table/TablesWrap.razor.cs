// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 折行示例代码
/// </summary>
public partial class TablesWrap
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private IEnumerable<Foo>? CellItems { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CellItems = Foo.GenerateFoo(FooLocalizer, 4);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        var items = Foo.GenerateFoo(FooLocalizer);
        // 设置记录总数
        var total = items.Count;
        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = true, IsFiltered = true, IsSearch = true });
    }

    private static async Task<string?> GetTooltipTextCallback(object? v)
    {
        await Task.Delay(5);

        var ret = string.Empty;
        if (v is Foo foo)
        {
            ret = $"{foo.Name}-{foo.Address}";
        }
        return ret;
    }
}
