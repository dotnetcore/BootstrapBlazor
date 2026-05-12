// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class CultureChooser
{
    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? BootstrapOptions { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<CultureChooser>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    private string? ClassString => CssBuilder.Default("culture-selector")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string SelectedCulture { get; set; } = CultureInfo.CurrentUICulture.Name;

    [NotNull]
    private string? Label { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Label ??= Localizer[nameof(Label)];
        SelectedCulture = GetSelectedCultureName();
    }

    private async Task SetCulture(SelectedItem item)
    {
        var culture = item.Value;
        if (string.IsNullOrEmpty(culture) || string.Equals(SelectedCulture, culture, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        SelectedCulture = culture;
        if (RendererInfo.Name == "Server")
        {
            var uri = new Uri(NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
            var query = $"?culture={Uri.EscapeDataString(culture)}&redirectUri={Uri.EscapeDataString(uri)}";

            NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);
        }
        else
        {
            await JSRuntime.InvokeVoidAsync("bbCulture.set", culture);

            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        }
    }

    private string GetSelectedCultureName()
    {
        var culture = CultureInfo.CurrentUICulture;
        var supportedCultures = BootstrapOptions.Value.GetSupportedCultures();
        return supportedCultures.FirstOrDefault(i => string.Equals(i.Name, culture.Name, StringComparison.OrdinalIgnoreCase))?.Name
            ?? supportedCultures.FirstOrDefault(i => string.Equals(i.TwoLetterISOLanguageName, culture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))?.Name
            ?? supportedCultures.FirstOrDefault()?.Name
            ?? culture.Name;
    }

    private string GetDisplayName(CultureInfo culture)
    {
        string? ret;
        if (RendererInfo.Name == "Server")
        {
            ret = culture.NativeName;
        }
        else
        {
            ret = culture.Name switch
            {
                "zh-CN" => "中文（中国）",
                "en-US" => "English (United States)",
                _ => ""
            };
        }
        return ret;
    }
}
