// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 弹窗示例代码
/// </summary>
public partial class TablesDialog
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
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

    private List<Foo> SelectedRows { get; set; } = [];

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Products = [];

        ProductSelectItems = Enumerable.Range(1, 5).Select(i => new Foo()
        {
            Id = i,
            Name = FooLocalizer["Foo.Name", $"{i:d4}"],
            DateTime = DateTime.Now.AddDays(i - 1),
            Address = FooLocalizer["Foo.Address", $"{random.Next(1000, 2000)}"],
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
