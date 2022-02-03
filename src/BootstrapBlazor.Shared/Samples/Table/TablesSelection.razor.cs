// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 选中行示例代码
/// </summary>
public sealed partial class TablesSelection
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private List<Foo>? SelectedItems { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);
        SelectedItems = Items.Take(4).ToList();
    }

    private void OnClick()
    {
        SelectedItems.Clear();
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 设置记录总数
        var total = Items.Count;

        // 内存分页
        var items = Items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        });
    }
}
