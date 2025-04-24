// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
///
/// </summary>
public partial class SelectObjects
{
    [NotNull]
    private IEnumerable<ListViews.Product>? Products { get; set; }

    private ListViews.Product? _value;

    private ListViews.Product? _widthValue;

    private ListViews.Product? _heightValue;

    private int _counter;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Products = Enumerable.Range(1, 8).Select(i => new ListViews.Product()
        {
            ImageUrl = $"{WebsiteOption.CurrentValue.AssetRootPath}images/Pic{i}.jpg",
            Description = $"Pic{i}.jpg",
            Category = $"Group{(i % 4) + 1}"
        });
    }

    private static async Task OnListViewItemClick(ListViews.Product product, ISelectObjectContext<ListViews.Product?> context)
    {
        // 设置组件值
        context.SetValue(product);

        // 当前模式为单选，主动关闭弹窗
        await context.CloseAsync();
    }

    private static string? GetTextCallback(ListViews.Product? product) => product?.ImageUrl;

    private static string? GetCounterTextCallback(int v) => v.ToString();

    private static async Task OnSubmit(int v, ISelectObjectContext<int> context)
    {
        context.SetValue(v);

        // 当前模式为单选，主动关闭弹窗
        await context.CloseAsync();
    }
}
