// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 虚拟滚动示例代码
/// </summary>
public partial class TablesVirtualization
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private async Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        await Task.Delay(200);
        var items = Items.Skip(options.StartIndex).Take(options.PageItems);
        return new QueryData<Foo>()
        {
            Items = items,
            TotalCount = Items.Count
        };
    }
}
