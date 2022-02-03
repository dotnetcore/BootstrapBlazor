// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class ListViews
{
    private BlockLogger? Trace { get; set; }
    private IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Products = Enumerable.Range(1, 100).Select(i => new Product()
        {
            ImageUrl = $"{WebsiteOption.Value.ImageLibUrl}/images/Pic{i}.jpg",
            Description = $"Pic{i}.jpg",
            Category = $"Group{(i % 4) + 1}"
        });
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
    private Task OnListViewItemClick(Product item)
    {
        Trace?.Log($"ListViewItem: {item.Description} clicked");
        return Task.CompletedTask;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem(){
                Name = "Items",
                Description = Localizer["Items"],
                Type = "IEnumerable<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "Pageable",
                Description = Localizer["Pageable"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem(){
                Name = "PageItemsSource",
                Description =Localizer["PageItemsSource"],
                Type = "IEnumerable<int>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "HeaderTemplate",
                Description = Localizer["HeaderTemplate"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "BodyTemplate",
                Description = Localizer["BodyTemplate"],
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "FooterTemplate",
                Description = Localizer["FooterTemplate"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnQueryAsync",
                Description = Localizer["OnQueryAsync"],
                Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
                ValueList = "—",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnListViewItemClick",
                Description = Localizer["OnListViewItemClick"],
                Type = "Func<TItem, Task>",
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
            },
    };
}

/// <summary>
/// 
/// </summary>
internal class Product
{
    /// <summary>
    /// 
    /// </summary>
    public string ImageUrl { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string Category { get; set; } = "";
}
