// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 选择行示例代码
/// </summary>
[JSModuleAutoLoader("Samples/Table/TablesSelection.razor.js", AutoInvokeInit = false, AutoInvokeDispose = false)]
public partial class TablesSelection
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private List<Foo>? SelectedItems { get; set; }

    [NotNull]
    private List<Foo> _selectedScrollItems = [];

    private bool _isKeepSelectedRows;

    private List<Foo> _scrollItems = [];

    private static IEnumerable<int> PageItemsSource => new int[]
    {
        4,
        10,
        20
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Items = Foo.GenerateFoo(LocalizerFoo);
        SelectedItems = Items.Take(4).ToList();

        _scrollItems.AddRange(Foo.GenerateFoo(LocalizerFoo, 20));
    }

    private void OnClick()
    {
        SelectedItems.Clear();
    }

    private void OnTriggerScroll()
    {
        _selectedScrollItems.Clear();
        _selectedScrollItems.Add(_scrollItems.Last());
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        var total = Items.Count;
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

    private async Task OnAfterRenderCallback(Table<Foo> table, bool firstRender)
    {
        if (!firstRender)
        {
            await InvokeVoidAsync("scroll", table.Id);
        }
    }
}
