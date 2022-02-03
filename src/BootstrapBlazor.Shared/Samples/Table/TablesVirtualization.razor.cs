// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public partial class TablesVirtualization
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);
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
