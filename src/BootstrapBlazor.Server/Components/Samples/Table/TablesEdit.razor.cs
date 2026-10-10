// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    [NotNull]
    private IEnumerable<Foo>? KeepAddingItems { get; set; }

    [NotNull]
    private IEnumerable<Foo>? KeepAddingDrawerItems { get; set; }

    private InsertRowMode InsertMode { get; set; } = InsertRowMode.Last;

    private string? PlaceHolderString { get; set; }

    private string DataServiceUrl => $"{WebsiteOption.Value.GiteeRepositoryUrl}/wikis/Table%20%E7%BB%84%E4%BB%B6%E6%95%B0%E6%8D%AE%E6%9C%8D%E5%8A%A1%E4%BB%8B%E7%BB%8D?sort_id=3207977";

    private bool _useGroup = false;

    private string? GetEducationDesc(EnumEducation? item) => item switch
    {
        EnumEducation.Primary => Localizer["TablesEditTemplateDisplayDetail1"],
        EnumEducation.Middle => Localizer["TablesEditTemplateDisplayDetail2"],
        _ => ""
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Items = Foo.GenerateFoo(FooLocalizer, 4);
        EditItems = Foo.GenerateFoo(FooLocalizer, 4);
        Hobbies = Foo.GenerateHobbies(FooLocalizer);
        CustomerDataService = new FooDataService<Foo>(FooLocalizer);
        BindItems = Foo.GenerateFoo(FooLocalizer).Take(5).ToList();
        PlaceHolderString ??= Localizer["TablesEditShowSearchPlaceHolderString"];

        var keepAddingItems = Foo.GenerateFoo(FooLocalizer, 4);

        // 数量列从 0 开始 便于观察连续新增时数量递增的效果
        keepAddingItems.ForEach(i => i.Count = 0);
        KeepAddingItems = keepAddingItems;

        var keepAddingDrawerItems = Foo.GenerateFoo(FooLocalizer, 4);
        keepAddingDrawerItems.ForEach(i => i.Count = 0);
        KeepAddingDrawerItems = keepAddingDrawerItems;
    }

    private Task<Foo> OnAddAsync() => Task.FromResult(new Foo() { Id = GenerateId(), DateTime = DateTime.Now, Address = $"Custom address  {DateTime.Now.Second}" });

    private Task<Foo> OnKeepAddAsync() => Task.FromResult(Foo.Generate(FooLocalizer));

    /// <summary>
    /// 点击「保存并新增」按钮时基于上一条已保存数据创建下一条模型 数量 +10
    /// </summary>
    /// <param name="lastModel">刚刚保存成功的模型实例</param>
    private static async Task<Foo> OnKeepAddingAsync(Foo lastModel)
    {
        // 模拟延时
        await Task.Delay(10);

        return new Foo()
        {
            Id = lastModel.Id + 1,
            DateTime = lastModel.DateTime,
            Name = lastModel.Name,
            Address = lastModel.Address,
            Complete = lastModel.Complete,
            Education = lastModel.Education,
            Hobby = lastModel.Hobby,
            Count = lastModel.Count + 10
        };
    }

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

    private class FooDataService<TModel>(IStringLocalizer<TModel> localizer) : TableDemoDataService<TModel>(localizer) where TModel : class
    {
    }

    private Task OnClick(Foo foo) => ToastService.Information("Custom button function", foo.Address!);
}
