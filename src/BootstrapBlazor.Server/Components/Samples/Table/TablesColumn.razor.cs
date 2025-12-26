// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 列设置示例代码
/// </summary>
public partial class TablesColumn
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private List<ComplexFoo>? ComplexItems { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 5, 10, 20 };

    private bool IgnoreColumn { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);
        ComplexItems = ComplexFoo.GenerateComplexFoo(FooLocalizer);
    }

    private void OnClickIgnoreColumn() => IgnoreColumn = !IgnoreColumn;

    private static bool ShowCheckbox(Foo foo) => foo.Complete;

    /// <summary>
    /// IntFormatter
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private static Task<string?> IntFormatter(object d)
    {
        string? ret = null;
        if (d is TableColumnContext<Foo, object?> data && data.Value != null)
        {
            var val = (int)data.Value;
            ret = $"Sales: {val:0.00}";
        }
        return Task.FromResult(ret);
    }

    /// <summary>
    /// Foo 类型的异步查询
    /// The async query of Items
    /// </summary>
    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;

        // 先处理过滤再处理排序 提高性能
        var isFiltered = false;
        if (options.Filters.Count != 0)
        {
            items = items.Where(options.Filters.GetFilterFunc<Foo>());
            isFiltered = true;
        }

        // 排序
        var isSorted = false;
        if (!string.IsNullOrEmpty(options.SortName))
        {
            items = items.Sort(options.SortName, options.SortOrder);
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

    /// <summary>
    /// ComplexItems 的异步查询
    /// The async query of ComplexItems
    /// </summary>
    private Task<QueryData<ComplexFoo>> OnQueryComplexFooAsync(QueryPageOptions options)
    {
        IEnumerable<ComplexFoo> items = ComplexItems;

        // 先处理过滤再处理排序 提高性能
        var isFiltered = false;
        if (options.Filters.Count != 0)
        {
            items = items.Where(options.Filters.GetFilterFunc<ComplexFoo>());
            isFiltered = true;
        }

        // 排序
        var isSorted = false;
        if (!string.IsNullOrEmpty(options.SortName))
        {
            items = items.Sort(options.SortName, options.SortOrder);
            isSorted = true;
        }

        // 设置记录总数
        var total = items.Count();

        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<ComplexFoo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = isSorted,
            IsFiltered = isFiltered,
            IsSearch = true
        });
    }

    private static Task<bool> OnSaveAsync(Foo foo, ItemChangedType changedType) => Task.FromResult(true);

    private static Task OnColumnCreating(List<ITableColumn> columns)
    {
        var item = columns.Find(i => i.GetFieldName() == nameof(Foo.Name));
        if (item != null)
        {
            item.Readonly = true;
        }
        return Task.CompletedTask;
    }

    private Alignment _dateTimeAlign = Alignment.Left;
    private Alignment _nameAlign = Alignment.Left;

    private void SetAlign(ITableColumn column, Alignment alignment)
    {
        var name = column.GetFieldName();
        if (name == "Name")
        {
            _nameAlign = alignment;
        }
        else if (name == "DateTime")
        {
            _dateTimeAlign = alignment;
        }
    }

    /// <summary>
    /// 复杂类型 ComplexFoo 的构造回调
    /// Construction callback of complex type ComplexFoo
    /// </summary>
    private ComplexFoo CreateComplexFoo() => ComplexFoo.Generate(FooLocalizer);

    /// <summary>
    /// 设置 ComplexItems 的 Company 属性
    /// Set property Company of ComplexItems
    /// </summary>
    private Task OnClickCompanyButton()
    {
        foreach (var complexFoo in ComplexItems)
        {
            complexFoo.Company = new Company(((char)('A' + Random.Shared.Next(26))).ToString());
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 示例类 Company
    /// Class sample Company
    /// </summary>
    private class Company(string name)
    {
        public string Name { get; set; } = name;
    }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private Task OnAction(Foo foo, string actionName) => ToastService.Information(foo.Name, $"Trigger {actionName}");

    /// <summary>
    /// 示例复杂类型 ComplexFoo
    /// Complex class sample ComplexFoo
    /// </summary>
    private class ComplexFoo : Foo
    {
        public int Age { get; set; }

        /// <summary>
        /// 业务逻辑无法确保 Company 属性不为 null
        /// The business logic cannot ensure that the Company property is not null.
        /// </summary>
        public Company? Company { get; set; }

        /// <summary>
        /// ComplexFoo 类不提供无参构造函数
        /// Class ComplexFoo does not provide a parameterless constructor
        /// </summary>
        public ComplexFoo(int age)
        {
            Age = age;
        }

        /// <summary>
        /// 生成含有随机数据的 ComplexFoo 实例
        /// Generate an instance of ComplexFoo with random data
        /// </summary>
        public static new ComplexFoo Generate(IStringLocalizer<Foo> localizer) => new(Random.Shared.Next(20, 65))
        {
            Id = 1,
            Name = localizer["Foo.Name", "1000"],
            DateTime = System.DateTime.Now,
            Address = localizer["Foo.Address", $"{Random.Shared.Next(1000, 2000)}"],
            Count = Random.Shared.Next(1, 100),
            Complete = Random.Shared.Next(1, 100) > 50,
            Education = Random.Shared.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle,
            ReadonlyColumn = Random.Shared.Next(10, 50)
        };

        /// <summary>
        /// 生成含有随机数据的 ComplexFoo 实例集合
        /// Generate a list of ComplexFoo instances with random data
        /// </summary>
        /// <returns>
        /// 返回一个含有随机数据的 ComplexFoo 实例集合
        /// Return a List of ComplexFoo instances with random data
        /// </returns>
        public static List<ComplexFoo> GenerateComplexFoo(IStringLocalizer<Foo> localizer, int count = 80) => Enumerable.Range(1, count).Select(i => new ComplexFoo(Random.Shared.Next(20, 65))
        {
            Id = i,
            Name = localizer["Foo.Name", $"{i:d4}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{Random.Shared.Next(1000, 2000)}"],
            Count = Random.Shared.Next(1, 100),
            Complete = Random.Shared.Next(1, 100) > 50,
            Education = Random.Shared.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle,
            ReadonlyColumn = Random.Shared.Next(10, 50)
        }).ToList();
    }
}
