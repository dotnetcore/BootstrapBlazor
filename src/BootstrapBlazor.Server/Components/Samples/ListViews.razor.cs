// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ListViews
/// </summary>
public sealed partial class ListViews
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private IEnumerable<Product>? Products { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Products = Enumerable.Range(1, 8).Select(i => new Product()
        {
            ImageUrl = $"{WebsiteOption.Value.AssetRootPath}images/Pic{i}.jpg",
            Description = $"Pic{i}.jpg",
            Category = $"Group{(i % 4) + 1}"
        });
    }

    private Task OnListViewItemClick(Product item)
    {
        Logger.Log($"ListViewItem: {item.Description} clicked");
        return Task.CompletedTask;
    }

    private Task<QueryData<Product>> OnQueryAsync(QueryPageOptions options)
    {
        var items = Products.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
        return Task.FromResult(new QueryData<Product>()
        {
            Items = items,
            TotalCount = Products.Count()
        });
    }

    private static bool CollapsedGroupCallback(object? groupKey) => groupKey?.ToString() != "Group1";

    private static IOrderedEnumerable<IGrouping<object?, Product>>? GroupOrderCallback(IEnumerable<IGrouping<object?, Product>> group) => group.OrderByDescending(i => i.Key);

    private static IOrderedEnumerable<Product>? GroupItemOrderCallback(IGrouping<object?, Product> group) => group.OrderByDescending(i => i.ImageUrl);

    internal class Product
    {
        public string ImageUrl { get; set; } = "";

        public string Description { get; set; } = "";

        public string Category { get; set; } = "";
    }

    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = "QueryAsync",
            Description = Localizer["QueryAsync"],
            Parameters = " — ",
            ReturnValue = "Task"
        }
    ];
}
