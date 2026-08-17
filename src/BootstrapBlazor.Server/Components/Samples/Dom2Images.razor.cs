// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Dom2Image 组件
/// </summary>
public partial class Dom2Images
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    [Inject]
    [NotNull]
    private IDom2ImageService? Dom2ImageService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Dom2Images>? Localizer { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    private string? _imageData;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private async Task OnGetUrlAsync()
    {
        var options = GetDom2ImageOptions();
        _imageData = await Dom2ImageService.GetUrlAsync("#table-9527", options);
    }

    private async Task OnDownloadAsync()
    {
        var fileName = $"table-9527-{DateTime.Now:HHmmss}";
        var options = GetDom2ImageOptions();
        await Dom2ImageService.DownloadAsync("#table-9527", fileName, options: options);
    }

    private async Task OnFullAsync()
    {
        var fileName = $"full-{DateTime.Now:HHmmss}";
        var options = GetDom2ImageOptions();
        await Dom2ImageService.DownloadAsync(".tabs-body-content:not(.d-none)", fileName, options: options);
    }

    private static Dom2ImageOptions GetDom2ImageOptions() => new()
    {
        // 排除表格 Header 中的筛选和排序图标，避免对齐问题
        Exclude = new[] { ".filter-icon", ".sort-icon" }
    };
}
