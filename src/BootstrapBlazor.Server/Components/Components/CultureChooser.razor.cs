// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class CultureChooser
{
    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? BootstrapOptions { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? WebsiteOption { get; set; }

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
    }

    private async Task SetCulture(SelectedItem item)
    {
        if (OperatingSystem.IsBrowser())
        {
            var cultureName = item.Value;
            if (cultureName != CultureInfo.CurrentCulture.Name)
            {
                await JSRuntime.SetCulture(cultureName);
                var culture = new CultureInfo(cultureName);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
        }
        else
        {
            // 使用 api 方式 适用于 Server-Side 模式
            if (SelectedCulture != item.Value)
            {
                var culture = item.Value;
                var uri = new Uri(NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
                var query = $"?culture={Uri.EscapeDataString(culture)}&redirectUri={Uri.EscapeDataString(uri)}";

                // use a path that matches your culture redirect controller from the previous steps
                NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);
            }
        }
    }

    private static string GetDisplayName(CultureInfo culture)
    {
        string? ret;
        if (OperatingSystem.IsBrowser())
        {
            ret = culture.Name switch
            {
                "zh-CN" => "中文（中国）",
                "en-US" => "English (United States)",
                _ => ""
            };
        }
        else
        {
            ret = culture.NativeName;
        }
        return ret;
    }
}
