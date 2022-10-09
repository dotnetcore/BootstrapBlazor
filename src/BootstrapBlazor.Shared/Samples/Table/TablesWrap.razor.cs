// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 折行演示示例代码
/// </summary>
public sealed partial class TablesWrap
{
    [NotNull]
    private IEnumerable<Foo>? CellItems { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CellItems = Foo.GenerateFoo(LocalizerFoo, 4);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        var items = Foo.GenerateFoo(LocalizerFoo);

        // 设置记录总数
        var total = items.Count;

        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        });
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("$.table_wrap");
        }
    }
}
