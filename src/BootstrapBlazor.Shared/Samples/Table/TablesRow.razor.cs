// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// Table 组件行示例代码
/// </summary>
public partial class TablesRow
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

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

    private static Task<Foo> OnAddAsync() => Task.FromResult(new Foo() { DateTime = DateTime.Now });

    private Task<bool> OnSaveAsync(Foo item, ItemChangedType changedType)
    {
        // 增加数据演示代码
        if (changedType == ItemChangedType.Add)
        {
            item.Id = Items.Max(i => i.Id) + 1;
            Items.Add(item);
        }
        else
        {
            var oldItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (oldItem != null)
            {
                oldItem.Name = item.Name;
                oldItem.Address = item.Address;
                oldItem.DateTime = item.DateTime;
                oldItem.Count = item.Count;
                oldItem.Complete = item.Complete;
                oldItem.Education = item.Education;
            }
        }
        return Task.FromResult(true);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;

        // 设置记录总数
        var total = items.Count();

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

    private async Task DoubleClickRowCallback(Foo item)
    {
        var cate = ToastCategory.Success;
        var title = "双击行回调委托示例";
        var content = $"选中行数据为名称 {item.Name} 的数据";
        await ToastService.Show(new ToastOption()
        {
            Category = cate,
            Title = title,
            Content = content
        });
    }

    private static string? SetRowClassFormatter(Foo item) => item.Count > 60 ? "highlight" : null;

    private Foo? CurrentItem { get; set; }

    private Task ClickRow(Foo item)
    {
        CurrentItem = item;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
