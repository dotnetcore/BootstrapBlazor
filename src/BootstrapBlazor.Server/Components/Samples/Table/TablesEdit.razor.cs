// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 表格编辑示例代码
/// </summary>
public partial class TablesEdit
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private IEnumerable<Foo>? EditItems { get; set; }

    private static IEnumerable<int> PageItemsSource => new[] { 4, 10, 20 };

    [NotNull]
    private IDataService<Foo>? CustomerDataService { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbies { get; set; }

    [NotNull]
    private IEnumerable<Foo>? BindItems { get; set; }

    private InsertRowMode InsertMode { get; set; } = InsertRowMode.Last;

    private string? PlaceHolderString { get; set; }

    private string DataServiceUrl => $"{WebsiteOption.CurrentValue.GiteeRepositoryUrl}/wikis/Table%20%E7%BB%84%E4%BB%B6%E6%95%B0%E6%8D%AE%E6%9C%8D%E5%8A%A1%E4%BB%8B%E7%BB%8D?sort_id=3207977";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Items = Foo.GenerateFoo(LocalizerFoo, 4);
        EditItems = Foo.GenerateFoo(LocalizerFoo, 4);
        Hobbies = Foo.GenerateHobbies(LocalizerFoo);
        CustomerDataService = new FooDataService<Foo>(LocalizerFoo);
        BindItems = Foo.GenerateFoo(LocalizerFoo).Take(5).ToList();
        PlaceHolderString ??= Localizer["TablesEditShowSearchPlaceHolderString"];
    }

    private Task<Foo> OnAddAsync() => Task.FromResult(new Foo() { Id = GenerateId(), DateTime = DateTime.Now, Address = $"Custom address  {DateTime.Now.Second}" });

    private int GenerateId()
    {
        var id = Items.Count;
        while (Items.Any(i => i.Id == id))
        {
            id++;
        }
        return id;
    }

    private Task<bool> OnSaveAsync(Foo item, ItemChangedType changedType)
    {
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
                oldItem.Hobby = item.Hobby;
            }
        }
        return Task.FromResult(true);
    }

    private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
    {
        items.ToList().ForEach(i => Items.Remove(i));
        return Task.FromResult(true);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;

        // 过滤
        var isFiltered = false;
        if (options.Filters.Count > 0)
        {
            items = items.Where(options.Filters.GetFilterFunc<Foo>());
            isFiltered = true;
        }

        // 排序
        var isSorted = false;
        if (!string.IsNullOrEmpty(options.SortName))
        {
            var invoker = Foo.GetNameSortFunc();
            items = invoker(items, options.SortName, options.SortOrder);
            isSorted = true;
        }

        // 设置记录总数
        var total = items.Count();

        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = isSorted,
            IsFiltered = isFiltered,
            IsSearch = true
        });
    }

    private class FooDataService<TModel>(IStringLocalizer<TModel> localizer) : TableDemoDataService<TModel>(localizer) where TModel : class, new()
    {
    }

    private Task OnClick(Foo foo) => ToastService.Information("Custom button function", foo.Address!);
}
