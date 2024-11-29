﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Table;

/// <summary>
/// 行设置示例代码
/// </summary>
public partial class TablesRow
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);
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

    private Foo? CurrentItem { get; set; }

    private Task ClickRow(Foo item)
    {
        CurrentItem = item;
        StateHasChanged();
        return Task.CompletedTask;
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

    private static string? SetRowClassFormatter(Foo item) => item.Count > 60 ? "row-highlight" : null;

    private Task UpdateRowValue(TableRowContext<Foo> context)
    {
        // 触发内部数据变化通知
        // 本例中 context.Row 就是数据源内数据，此处不需要进行数据处理
        // 如果使用数据库此处可以根据数据变化项进行数据库更新即可
        // 此处不需要 StateHasChanged 更新 UI

        return Task.CompletedTask;
    }
}
