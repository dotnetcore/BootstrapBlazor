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
            ImageUrl = $"{WebsiteOption.CurrentValue.AssetRootPath}images/Pic{i}.jpg",
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

    private AttributeItem[] GetAttributes() =>
    [
        new(){
            Name = "Items",
            Description = Localizer["Items"],
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new(){
            Name = "Pageable",
            Description = Localizer["Pageable"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new(){
            Name = "HeaderTemplate",
            Description = Localizer["HeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new(){
            Name = "BodyTemplate",
            Description = Localizer["BodyTemplate"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new(){
            Name = "FooterTemplate",
            Description = Localizer["FooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new(){
            Name = nameof(ListView<Foo>.Collapsible),
            Description = Localizer["Collapsible"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new(){
            Name = nameof(ListView<Foo>.IsAccordion),
            Description = Localizer["IsAccordion"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "OnQueryAsync",
            Description = Localizer["OnQueryAsync"],
            Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
            ValueList = "—",
            DefaultValue = " — "
        },
        new() {
            Name = "OnListViewItemClick",
            Description = Localizer["OnListViewItemClick"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ListView<Foo>.CollapsedGroupCallback),
            Description = Localizer["CollapsedGroupCallback"],
            Type = "Func<object?, bool>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ListView<Foo>.GroupOrderCallback),
            Description = Localizer["GroupOrderCallback"],
            Type = "Func<IEnumerable<IGrouping<object?, TItem>>, IOrderedEnumerable<IGrouping<object?, TItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ListView<Foo>.GroupItemOrderCallback),
            Description = Localizer["GroupItemOrderCallback"],
            Type = "Func<IGrouping<object?, TItem>, IOrderedEnumerable<TItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ListView<Foo>.GroupHeaderTextCallback),
            Description = Localizer["GroupHeaderTextCallback"],
            Type = "Func<object?, string?>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

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
