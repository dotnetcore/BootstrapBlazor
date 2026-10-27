// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;
using System.Globalization;

namespace BootstrapBlazor.Shared.Components.Layout;

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
        if (OperatingSystem.IsBrowser())
        {
            OS = "WebAssembly";
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
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

    private async Task SetLang(string cultureName)
    {
        if (SelectedCulture != cultureName)
        {
            if (OperatingSystem.IsBrowser())
            {
                try
                {
                    await JSRuntime.InvokeVoidAsync("bbCulture.set", cultureName);
                }
                catch (JSException ex)
                {
                    Logger.LogError(ex, "Error setting culture");
                    return;
                }

                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
                return;
            }

            var uri = new Uri(NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
            var query = $"?culture={Uri.EscapeDataString(cultureName)}&redirectUri={Uri.EscapeDataString(uri)}";

            // use a path that matches your culture redirect controller from the previous steps
            NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);
        }
    }
}
