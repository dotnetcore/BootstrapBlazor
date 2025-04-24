// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// Home 模板
/// </summary>
public partial class HomeLayout
{
    private static string Version => Environment.Version.ToString();

    [NotNull]
    private string? OS { get; set; }

    private string SelectedCulture { get; set; } = CultureInfo.CurrentUICulture.Name;

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void OnInitialized()
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            OS = "Windows";
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
        {
            OS = "OSX";
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
        {
            OS = "Linux";
        }
        else
        {
            OS = "Unknown";
        }
    }

    private Task SetLang(string cultureName)
    {
        // 使用 api 方式 适用于 Server-Side 模式
        if (SelectedCulture != cultureName)
        {
            var uri = new Uri(NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
            var query = $"?culture={Uri.EscapeDataString(cultureName)}&redirectUri={Uri.EscapeDataString(uri)}";

            // use a path that matches your culture redirect controller from the previous steps
            NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);
        }

        return Task.CompletedTask;
    }
}
