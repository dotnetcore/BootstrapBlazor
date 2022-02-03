// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public partial class ThemeChooser
{
    private ElementReference ThemeElement { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Themes { get; set; }

    [NotNull]
    private string? Title { get; set; }

    [NotNull]
    private string? HeaderText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ThemeChooser>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? BootstrapOptions { get; set; }

    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? SiteOptions { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];
        HeaderText ??= Localizer[nameof(HeaderText)];
        Themes = BootstrapOptions.Value.Themes.Select(kv => new SelectedItem(kv.Value, kv.Key));
        SiteOptions.Value.CurrentTheme = Themes.FirstOrDefault(i => i.Text == "Motronic")?.Value ?? "";
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("$.initTheme", ThemeElement);
        }
    }

    private async Task OnClickTheme(SelectedItem item)
    {
        SiteOptions.Value.CurrentTheme = item.Value;

        await JSRuntime.InvokeVoidAsync("$.setTheme", LinksCache[item.Value]);
    }

    private string? GetThemeItemClass(SelectedItem item) => CssBuilder.Default("theme-item")
        .AddClass("active", SiteOptions.Value.CurrentTheme == item.Value)
        .Build();

    private Dictionary<string, ICollection<string>> LinksCache { get; } = new(new KeyValuePair<string, ICollection<string>>[]
    {
            new("bootstrap.blazor.bundle.min.css", new List<string>()),
            new("motronic.min.css", new string[]
            {
                "_content/BootstrapBlazor/css/motronic.min.css",
                "_content/BootstrapBlazor.Shared/css/motronic.css"
            }),
            new("ant", new List<string>()),
            new("layui", new List<string>())
    });
}
