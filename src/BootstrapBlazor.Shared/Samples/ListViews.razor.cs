// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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
            ImageUrl = $"./images/Pic{i}.jpg",
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

    internal class Product
    {
        public string ImageUrl { get; set; } = "";

        public string Description { get; set; } = "";

        public string Category { get; set; } = "";
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
            Description = Localizer["Collapsable"],
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
        }
    };

    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new MethodItem()
        {
            Name = "QueryAsync",
            Description = Localizer["QueryAsync"],
            Parameters = " — ",
            ReturnValue = "Task"
        }
    };
}
