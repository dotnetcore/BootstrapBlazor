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
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

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

        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private async Task OnGetUrlAsync()
    {
        _imageData = await Dom2ImageService.GetUrlAsync("#table-9527");
    }

    private async Task OnDownloadAsync()
    {
        var fileName = $"table-9527-{DateTime.Now:HHmmss}";
        await Dom2ImageService.DownloadAsync("#table-9527", fileName);
    }

    private async Task OnFullAsync()
    {
        var fileName = $"full-{DateTime.Now:HHmmss}";
        await Dom2ImageService.DownloadAsync(".tabs-body-content:not(.d-none)", fileName);
    }
}
