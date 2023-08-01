// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;
using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 明细行示例代码
/// </summary>
public partial class TablesDetailRow
{
    private ConcurrentDictionary<string, List<DetailRow>> Cache { get; } = new();

    private static readonly Random random = new();

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    private DataTableDynamicContext? DataTableDynamicContext { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private string? DetailText { get; set; }

    [NotNull]
    private Table<Foo>? Table { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);

        DetailText = Localizer[$"{nameof(DetailText)}{IsDetails}"];

        DataTableDynamicContext = DataTableDynamicService.CreateContext();
    }

    private static List<DetailRow> GetDetailRowsByName(string name) => Enumerable.Range(1, 4).Select(i => new DetailRow()
    {
        Id = i,
        Name = name,
        DateTime = DateTime.Now.AddDays(i - 1),
        Complete = random.Next(1, 100) > 50
    }).ToList();


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
            TotalCount = total
        });
    }

    private bool IsDetails { get; set; } = true;

    private List<DetailRow> GetDetailDataSource(Foo foo)
    {
        var cacheKey = foo.Name ?? "";
        return Cache.GetOrAdd(cacheKey, key => GetDetailRowsByName(cacheKey));
    }

    private Func<Foo, Task>? OnDoubleClickRowCallback()
    {
        Func<Foo, Task>? ret = null;
        if (IsDetails)
        {
            ret = foo =>
            {
                Table.ExpandDetailRow(foo);
                return Task.CompletedTask;
            };
        }
        return ret;
    }

    private static bool ShowDetailRow(Foo item) => item.Complete;

    private void OnClickDetailRow()
    {
        DetailText = Localizer[$"{nameof(DetailText)}{IsDetails}"];
        IsDetails = !IsDetails;
    }

    private DynamicObjectContext GetDetailDataTableDynamicContext(DynamicObject context) => DataTableDynamicService.CreateDetailContext(context);

    private class DetailRow
    {
        [DisplayName("主键")]
        public int Id { get; set; }

        [DisplayName("培训课程")]
        [AutoGenerateColumn(Order = 10)]
        public string Name { get; set; } = "";

        [DisplayName("日期")]
        [AutoGenerateColumn(Order = 20, Width = 180)]
        public DateTime DateTime { get; set; }

        [DisplayName("是/否")]
        [AutoGenerateColumn(Order = 30, Width = 70)]
        public bool Complete { get; set; }
    }
}
