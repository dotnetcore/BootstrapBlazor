// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 弹窗示例代码
/// </summary>
public partial class TablesDialog
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Table<Foo>? ProductTable { get; set; }

    [NotNull]
    private List<Foo>? Products { get; set; }

    [NotNull]
    private List<Foo>? ProductSelectItems { get; set; }

    [NotNull]
    private Modal? Modal { get; set; }

    private bool _confirm;

    private static readonly Random random = new();

    private List<Foo> SelectedRows { get; set; } = new();

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Products = new List<Foo>();

        ProductSelectItems = Enumerable.Range(1, 5).Select(i => new Foo()
        {
            Id = i,
            Name = LocalizerFoo["Foo.Name", $"{i:d4}"],
            DateTime = DateTime.Now.AddDays(i - 1),
            Address = LocalizerFoo["Foo.Address", $"{random.Next(1000, 2000)}"],
            Count = random.Next(1, 100),
            Complete = random.Next(1, 100) > 50,
            Education = EnumEducation.Primary,
            Hobby = new string[] { "1" }
        }).ToList();
    }

    private Task ShowDialog(IEnumerable<Foo> items) => Modal.Toggle();

    private async Task OnConfirm()
    {
        _confirm = true;
        await Modal.Toggle();
        await ProductTable.QueryAsync();
    }

    private Task<bool> OnSaveAsync(Foo item, ItemChangedType changedType)
    {
        var oldItem = Products.FirstOrDefault(i => i.Id == item.Id);
        if (oldItem != null)
        {
            oldItem.Count = item.Count;
        }
        return Task.FromResult(true);
    }

    private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
    {
        Products.RemoveAll(p => items.Contains(p));
        return Task.FromResult(true);
    }

    private Task<QueryData<Foo>> OnQueryEditAsync(QueryPageOptions options)
    {
        ProductTable.SelectedRows.Clear();
        var items = Products;
        if (_confirm)
        {
            items.Clear();
            items.AddRange(SelectedRows);
        }
        _confirm = false;

        var total = items.Count;
        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsFiltered = true,
            IsSearch = true,
            IsSorted = true
        });
    }

    private Task<QueryData<Foo>> OnQueryProductAsync(QueryPageOptions options)
    {
        var items = ProductSelectItems;

        var total = items.Count;
        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total
        });
    }
}
