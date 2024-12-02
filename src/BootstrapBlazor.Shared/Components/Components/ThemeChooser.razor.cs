// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Components.Components;

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

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];
        HeaderText ??= Localizer[nameof(HeaderText)];
        Themes = WebsiteOption.CurrentValue.Themes.Select(i => new SelectedItem { Text = i.Name, Value = i.Key });
        WebsiteOption.CurrentValue.CurrentTheme = "bootstrap";
    }

    private async Task OnClickTheme(SelectedItem item)
    {
        WebsiteOption.CurrentValue.CurrentTheme = item.Value;
        var theme = WebsiteOption.CurrentValue.Themes.Find(i => i.Key == item.Value);
        if (theme != null)
        {
            await InvokeVoidAsync("updateTheme", [theme.Files]);
        }
    }

    private string? GetThemeItemClass(SelectedItem item) => CssBuilder.Default("theme-item")
        .AddClass("active", WebsiteOption.CurrentValue.CurrentTheme == item.Value)
        .Build();
}
