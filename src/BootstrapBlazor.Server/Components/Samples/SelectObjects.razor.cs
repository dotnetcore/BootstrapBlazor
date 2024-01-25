// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
///
/// </summary>
public partial class SelectObjects
{
    [NotNull]
    private IEnumerable<ListViews.Product>? Products { get; set; }

    private ListViews.Product? _value;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Products = Enumerable.Range(1, 8).Select(i => new ListViews.Product()
        {
            ImageUrl = $"./images/Pic{i}.jpg",
            Description = $"Pic{i}.jpg",
            Category = $"Group{(i % 4) + 1}"
        });
    }

    private Task OnListViewItemClick(ListViews.Product product, ISelectObjectContext<ListViews.Product?> context)
    {
        context.Component.SetValue(product);

        return Task.CompletedTask;
    }

    private static string? GetTextCallback(ListViews.Product? product) => product?.ImageUrl;
}
