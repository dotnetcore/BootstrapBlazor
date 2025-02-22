// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Html2Image 组件
/// </summary>
public partial class Html2Images
{
    /// <summary>
    /// 获得 IconTheme 实例
    /// </summary>
    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [Inject]
    [NotNull]
    private IHtml2Image? Html2ImageService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Html2Images>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

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

    private async Task OnExportAsync()
    {
        _imageData = await Html2ImageService.GetDataAsync("#table-9527");
        StateHasChanged();
    }
}
