// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public sealed partial class TablesDetailRow
{
    private Dictionary<string, IEnumerable<DetailRow>> Cache { get; } = new();

    private static readonly Random random = new();

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    [NotNull]
    private List<Foo>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TablesDetailRow>? LocalizerDetailRow { get; set; }

    [NotNull]
    private string? DetailText { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DetailText ??= LocalizerDetailRow[$"{nameof(DetailText)}{!IsDetails}"];
        Items = Foo.GenerateFoo(Localizer);
    }

    private static IEnumerable<DetailRow> GetDetailRowsByName(string name) => Enumerable.Range(1, 4).Select(i => new DetailRow()
    {
        Id = i,
        Name = name,
        DateTime = DateTime.Now.AddDays(i - 1),
        Complete = random.Next(1, 100) > 50
    });

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

    private void OnClickDetailRow()
    {
        DetailText = LocalizerDetailRow[$"{nameof(DetailText)}{IsDetails}"];
        IsDetails = !IsDetails;
    }

    private class DetailRow
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("主键")]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("培训课程")]
        [AutoGenerateColumn(Order = 10)]
        public string Name { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("日期")]
        [AutoGenerateColumn(Order = 20, Width = 180)]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("是/否")]
        [AutoGenerateColumn(Order = 30, Width = 70)]
        public bool Complete { get; set; }
    }

    private static bool ShowDetailRow(Foo item) => item.Complete;
}
