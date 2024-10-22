// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 主题选择器组件
/// </summary>
public partial class ThemeChooser
{
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
    private IOptionsMonitor<WebsiteOptions>? SiteOptions { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];
        HeaderText ??= Localizer[nameof(HeaderText)];
        Themes = SiteOptions.CurrentValue.Themes.Select(i => new SelectedItem { Text = i.Name, Value = i.Key });
        SiteOptions.CurrentValue.CurrentTheme = "bootstrap";
    }

    private async Task OnClickTheme(SelectedItem item)
    {
        SiteOptions.CurrentValue.CurrentTheme = item.Value;
        var theme = SiteOptions.CurrentValue.Themes.Find(i => i.Key == item.Value);
        if (theme != null)
        {
            await InvokeVoidAsync("updateTheme", [theme.Files]);
        }
    }

    private string? GetThemeItemClass(SelectedItem item) => CssBuilder.Default("theme-item")
        .AddClass("active", SiteOptions.CurrentValue.CurrentTheme == item.Value)
        .Build();
}
